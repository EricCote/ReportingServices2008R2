'-----------------------------------------------------------------------
' * This file is part of the Microsoft Code Samples.
'
' * Copyright (C) Microsoft Corporation.  All rights reserved.
' 
' * This source code is intended only as a supplement to Microsoft
' * Development Tools and/or on-line documentation.  See these other
' * materials for detailed information regarding Microsoft code samples.
'
' * THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
' * KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
' * IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
' * PARTICULAR PURPOSE.
'
' * Summary:  Demonstrates a Microsoft Office 2003 Research Pane for Microsoft 
' * SQL Server Reporting Service. The Query web service is required for
' * a Research Pane. The Query web service process the queries 
' * you receive from your users and returns an XML stream comforming to the
' * urn:Microsoft.Search namespace. Please see the Microsoft Research Library
' * SDK for more details.
'---------------------------------------------------------------------

Imports System
Imports System.Collections
Imports System.Web
Imports System.Web.Services
Imports System.Net
Imports System.Web.Services.Protocols
Imports System.Xml
Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Globalization
Imports Microsoft.SqlServer.ReportingServices2010


Namespace Microsoft.Samples.SqlServer
    '/ <summary>
    '/ The web service must have the urn:Microsoft.Search namespace attribute.
    '/ </summary>
    <WebService([Namespace]:="urn:Microsoft.Search")> _
    Public Class QueryService
        Inherits System.Web.Services.WebService
        Private xmlQueryResponse As XmlWriter
        Private ioMemoryStream As MemoryStream
        Private catalogItemArray() As CatalogItem
        Private catalogManager As CatalogManager
        Private HTTPPath As String = String.Empty
        Private reportItemPath As String = String.Empty
        Private queryText As String = String.Empty
        Private queryValues As Hashtable


        Public Sub New()

        End Sub 'New



        '/ <summary>
        '/ Called by Microsoft Office research pane API to process user query string and return appropriate
        '/ xml back to Microsoft Office.
        '/ </summary>
        '/ <param name="queryXml"></param>
        '/ <returns></returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes"), WebMethod()> _
        Public Function Query(ByVal queryXml As String) As String
            Try
                reportItemPath = System.Configuration.ConfigurationManager.AppSettings("ReportItemPath")
                Dim reportServer As String = System.Configuration.ConfigurationManager.AppSettings("ReportServer")
                HTTPPath = System.Configuration.ConfigurationManager.AppSettings("SearchServerPath")
                If Not HTTPPath.EndsWith("/") Then
                    HTTPPath += "/"
                End If
                'The response requires a research pane specific header. See the method for details.
                'Create starting elements for urn:Microsoft.Search.Response header. If there is an exception, the 
                'required response header stream is created outside of the try/catch.
                WriteStartResponseHeader()

                queryValues = QueryValuesFromXml(queryXml)

                Connect(reportServer, CredentialCache.DefaultCredentials)

                Dim startChar As String = queryText.Substring(0, 1)
                Dim isSearchAction As Boolean = queryValues.ContainsKey("FormAction")
                Dim searchType As Integer = 0
                If startChar <> "?" OrElse isSearchAction Then
                    WriteContentHeader()

                    If isSearchAction Then
                        searchType = 2
                    ElseIf startChar = "/" Then
                        searchType = 1
                    End If 'Set the CatalogItems. A "/" will call ListChildren. Any other query will call FindItems.
                    CatalogItems(searchType)

                    'Create elements for Folders with a folder icon
                    Folders()
                    'Create elements for Reports
                    Reports()

                Else
                    SearchForm()
                End If

            Catch ex As Exception
                NoResult(ex)
            Finally
                WriteGoTo()

                WriteEndResponseHeader()
            End Try

            'Return a response representing Reporting Services catalogs or the exception message.
            Return WriteResponse()

        End Function


        '/ <summary>
        '/ Set the CatalogItems to render based on the user query.
        '/ </summary>
        Private Sub CatalogItems(ByVal searchType As Integer)
            Dim boolOperator As BooleanOperatorEnum = BooleanOperatorEnum.Or
            Dim searchFolder As String
            'Default searchType = 0
            If searchType = 1 Then
                catalogItemArray = catalogManager.ListChildren(queryText)
            ElseIf searchType = 2 Then

                If queryValues("AndOr").ToString() = "And" Then
                    boolOperator = BooleanOperatorEnum.And
                ElseIf queryValues("AndOr").ToString() = "Or" Then
                    boolOperator = BooleanOperatorEnum.Or
                End If
                searchFolder = queryValues("SearchFolder").ToString()
                If searchFolder = "RootFolder" Then
                    searchFolder = "/"
                End If
                catalogItemArray = catalogManager.AdvancedSearch(searchFolder, boolOperator, SearchConditions())
            Else
                catalogItemArray = catalogManager.FindItems(queryText)
            End If
            If catalogItemArray.Length = 0 Then
                NoResult(Nothing)
            End If

        End Sub

        Private Function SearchConditions() As SearchCondition()

            'Create single condition sample based on "?" in queryText
            'Supports Folder to Search and Name Contains Or Description contains
            Dim searchCondition As SearchCondition = Nothing
            Dim searchValues As New Hashtable()

            Dim item As DictionaryEntry
            For Each item In queryValues
                Dim key As String = item.Key.ToString()

                If key.StartsWith("SearchCondition.") Then
                    searchValues.Add(key, item.Value.ToString())
                End If
            Next item

            Dim conditions(searchValues.Count - 1) As SearchCondition
            Dim i As Integer = -1
            Dim searchItem As DictionaryEntry
            For Each searchItem In searchValues
                i += 1
                Dim delimStr As String = "."
                Dim delimiter As Char() = delimStr.ToCharArray()
                Dim searchName As String = searchItem.Key.ToString().Split(delimiter)(1)

                searchCondition = CreateSearchCondition(searchName, searchItem.Value.ToString())
                conditions(i) = searchCondition
            Next searchItem

            Return conditions

        End Function


        Private Function CreateSearchCondition(ByVal name As String, ByVal value As String) As SearchCondition

            
            Dim searchCondition As New SearchCondition()
            searchCondition.Condition = ConditionEnum.Contains
            searchCondition.ConditionSpecified = True
            searchCondition.Name = name
            Dim val() As String = {value}
            searchCondition.Values = val

            Return searchCondition

        End Function


        '/ <summary>
        '/ Write the final response to Office research pane
        '/ </summary>
        '/ <returns></returns>
        Private Function WriteResponse() As String
            xmlQueryResponse.Flush()
            ioMemoryStream.Flush()
            ioMemoryStream.Position = 0
            Dim response As String = String.Empty

            Using iostReader As StreamReader = New StreamReader(ioMemoryStream)
                Using (xmlQueryResponse)
                    response = iostReader.ReadToEnd().ToString()
                End Using
                ioMemoryStream.Close()
                Return response
            End Using

        End Function


        Private Function QueryValuesFromXml(ByVal queryXml As String) As Hashtable
            Dim values As New Hashtable()
            Dim requestXml As New XmlDocument()
            Dim nsmRequest As XmlNamespaceManager = Nothing
            'Extact Query Text
            If queryXml.Length > 0 Then
                requestXml.LoadXml(queryXml)
                nsmRequest = New XmlNamespaceManager(requestXml.NameTable)
                nsmRequest.AddNamespace("ns", "urn:Microsoft.Search.Query")
                nsmRequest.AddNamespace("sp", "urn:Microsoft.Search.Office.ServiceParameters")
                nsmRequest.AddNamespace("oc", "urn:Microsoft.Search.Query.Office.Context")
            End If

            queryText = requestXml.SelectSingleNode("//ns:QueryText", nsmRequest).InnerText

            'Get ServiceParameters
            Dim serviceParameterNode As XmlNode = requestXml.SelectSingleNode("//sp:ServiceParameters/sp:Parameters", nsmRequest)
            If Not (serviceParameterNode Is Nothing) Then
                values.Add("FormAction", requestXml.SelectSingleNode("//sp:ServiceParameters/sp:Action", nsmRequest).InnerText)

                Dim param As XmlNode
                For Each param In serviceParameterNode.ChildNodes
                    values.Add(param.Name, param.InnerText)
                Next param
            End If

            Return values

        End Function


        '/ <summary>
        '/ Response header for registration domain
        '/ </summary>
        Private Sub WriteStartResponseHeader()

            'Create response MemoryStream and xmlQueryResponse for response string
            ioMemoryStream = New MemoryStream()
            xmlQueryResponse = XmlWriter.Create(ioMemoryStream)

            'Start of XML document
            xmlQueryResponse.WriteStartDocument()
            xmlQueryResponse.WriteStartElement("ResponsePacket", "urn:Microsoft.Search.Response")
            xmlQueryResponse.WriteAttributeString("revision", "1")
            xmlQueryResponse.WriteStartElement("Response")

            'The domain GUID must match the Registration Service ID
            xmlQueryResponse.WriteAttributeString("domain", Resources.Research.ServiceId)
            xmlQueryResponse.WriteElementString("QueryID", Resources.Research.QueryId)
            xmlQueryResponse.WriteStartElement("Range")
            xmlQueryResponse.WriteStartElement("Results")

        End Sub

        Private Sub WriteContentHeader()
            xmlQueryResponse.WriteStartElement("Content", "urn:Microsoft.Search.Response.Content")

        End Sub


        Private Sub WriteFormHeader()
            xmlQueryResponse.WriteStartElement("Form", "urn:Microsoft.Search.Response.Form")

        End Sub



        '/ <summary>
        '/ Close response start elements.
        '/ </summary>
        Private Sub WriteEndResponseHeader()
            xmlQueryResponse.WriteEndElement() 'Content
            xmlQueryResponse.WriteEndElement() 'Results
            xmlQueryResponse.WriteEndElement() 'Range
            xmlQueryResponse.WriteElementString("Status", String.Format(CultureInfo.InvariantCulture, Resources.Resources.Status))

            xmlQueryResponse.WriteEndElement() 'Response
            xmlQueryResponse.WriteEndElement() 'ResponsePacket
            xmlQueryResponse.WriteEndDocument()

        End Sub


        Private Sub NoResult(ByVal ex As Exception)
            'Close any open resources since response creates a new XmlWriter
            ioMemoryStream.Close()
            xmlQueryResponse.Close()

            WriteStartResponseHeader()
            WriteContentHeader()

            xmlQueryResponse.WriteStartElement("Line")

            xmlQueryResponse.WriteStartElement("Image")

            xmlQueryResponse.WriteAttributeString("source", HTTPPath + System.Configuration.ConfigurationManager.AppSettings("InfoIcon"))
            xmlQueryResponse.WriteEndElement()
            xmlQueryResponse.WriteElementString("Char", String.Format(CultureInfo.InvariantCulture, Resources.Resources.NoResults))
            xmlQueryResponse.WriteEndElement()

            xmlQueryResponse.WriteRaw("<HorizontalRule/>")

            If Not (ex Is Nothing) Then
                'For testing - Create P elements to return the exception message to the research pane. 
                xmlQueryResponse.WriteElementString("P", "Exception:")
                xmlQueryResponse.WriteElementString("P", ex.Message)
                xmlQueryResponse.WriteRaw("<HorizontalRule/>")
            End If

        End Sub


        Private Sub WriteGoTo()
            xmlQueryResponse.WriteStartElement("Line")
            xmlQueryResponse.WriteElementString("Char", String.Format(CultureInfo.InvariantCulture, Resources.Resources.GoTo))
            'NewQuery
            xmlQueryResponse.WriteStartElement("NewQuery")
            xmlQueryResponse.WriteAttributeString("query", "/")
            xmlQueryResponse.WriteAttributeString("tooltip", String.Format(CultureInfo.InvariantCulture, Resources.Resources.GoToRootFolder))
            xmlQueryResponse.WriteElementString("Text", String.Format(CultureInfo.InvariantCulture, Resources.Resources.ShowRootFolder))
            xmlQueryResponse.WriteEndElement()

            xmlQueryResponse.WriteElementString("Char", "|")
            'NewQuery
            xmlQueryResponse.WriteStartElement("NewQuery")
            xmlQueryResponse.WriteAttributeString("query", "?")
            xmlQueryResponse.WriteAttributeString("tooltip", String.Format(CultureInfo.InvariantCulture, Resources.Resources.ShowSearch))
            xmlQueryResponse.WriteElementString("Text", String.Format(CultureInfo.InvariantCulture, Resources.Resources.ShowSearch))

            xmlQueryResponse.WriteEndElement()
            xmlQueryResponse.WriteEndElement()

        End Sub


        '/ <summary>
        '/ Create line elements with folder image and subfolder hyperlinks. The hyperlink contains
        '/ a new query for a subfolder such as "/Products"
        '/ </summary>
        '/ <param name="folderImage"></param>
        Private Sub Folders()
            Dim catalogItem As CatalogItem
            For Each catalogItem In catalogItemArray
                If catalogItem.TypeName = "Folder" AndAlso catalogItem.Name <> "Data Sources" Then
                    xmlQueryResponse.WriteStartElement("Line")
                    'Folder Image
                    xmlQueryResponse.WriteStartElement("Image")

                    xmlQueryResponse.WriteAttributeString("source", HTTPPath + System.Configuration.ConfigurationManager.AppSettings("FolderIcon"))
                    xmlQueryResponse.WriteEndElement()
                    'NewQuery
                    xmlQueryResponse.WriteStartElement("NewQuery")
                    xmlQueryResponse.WriteAttributeString("query", catalogItem.Path)
                    xmlQueryResponse.WriteElementString("Text", catalogItem.Name)
                    xmlQueryResponse.WriteEndElement()
                    xmlQueryResponse.WriteEndElement()
                End If
            Next catalogItem

        End Sub


        Private Sub SearchForm()
            'Get all folders
            catalogItemArray = catalogManager.AllFolders()

            WriteFormHeader()

            xmlQueryResponse.WriteElementString("Label", String.Format(CultureInfo.InvariantCulture, Resources.Resources.SearchPage))

            'Search Folders List
            xmlQueryResponse.WriteElementString("Label", String.Format(CultureInfo.InvariantCulture, Resources.Resources.SearchWithinFolder))
            xmlQueryResponse.WriteStartElement("Listbox")
            xmlQueryResponse.WriteAttributeString("id", "SearchFolder")

            xmlQueryResponse.WriteStartElement("Option")
            xmlQueryResponse.WriteAttributeString("id", "RootFolder")
            xmlQueryResponse.WriteAttributeString("selected", "true")
            xmlQueryResponse.WriteElementString("Text", String.Format(CultureInfo.InvariantCulture, Resources.Resources.RootFolder))
            xmlQueryResponse.WriteEndElement()

            Dim catalogItem As CatalogItem
            For Each catalogItem In catalogItemArray
                If catalogItem.TypeName = "Folder" AndAlso catalogItem.Name <> "Data Sources" Then

                    xmlQueryResponse.WriteStartElement("Option")
                    xmlQueryResponse.WriteAttributeString("id", catalogItem.Path)
                    xmlQueryResponse.WriteElementString("Text", String.Format(CultureInfo.InvariantCulture, catalogItem.Name))
                    xmlQueryResponse.WriteEndElement()
                End If
            Next catalogItem
            xmlQueryResponse.WriteEndElement()

            'Name Contains Option            
            xmlQueryResponse.WriteElementString("Label", String.Format(CultureInfo.InvariantCulture, Resources.Resources.NameContains))

            xmlQueryResponse.WriteStartElement("Edit")
            xmlQueryResponse.WriteAttributeString("id", "SearchCondition.Name")
            xmlQueryResponse.WriteEndElement()


            'AndOr Combobox
            xmlQueryResponse.WriteStartElement("Listbox")
            xmlQueryResponse.WriteAttributeString("id", "AndOr")
            xmlQueryResponse.WriteAttributeString("width", "4")
            'xmlQueryResponse.WriteElementString("Text", "Boolean Operator");
            xmlQueryResponse.WriteStartElement("Option")
            xmlQueryResponse.WriteAttributeString("id", "And")
            xmlQueryResponse.WriteElementString("Text", "And")
            xmlQueryResponse.WriteEndElement()

            xmlQueryResponse.WriteStartElement("Option")
            xmlQueryResponse.WriteAttributeString("id", "Or")
            xmlQueryResponse.WriteAttributeString("selected", "true")
            xmlQueryResponse.WriteElementString("Text", "Or")
            xmlQueryResponse.WriteEndElement()

            xmlQueryResponse.WriteEndElement()

            'Description Contains Option            
            xmlQueryResponse.WriteElementString("Label", String.Format(CultureInfo.InvariantCulture, Resources.Resources.DescriptionContains))

            xmlQueryResponse.WriteStartElement("Edit")
            xmlQueryResponse.WriteAttributeString("id", "SearchCondition.Description")
            xmlQueryResponse.WriteEndElement()

            'Requery Button
            xmlQueryResponse.WriteStartElement("Button")
            xmlQueryResponse.WriteAttributeString("id", "SearchForm")
            xmlQueryResponse.WriteAttributeString("action", "requery")
            xmlQueryResponse.WriteElementString("Text", String.Format(CultureInfo.InvariantCulture, Resources.Resources.Search))
            xmlQueryResponse.WriteEndElement()

        End Sub

        '/ <summary>
        '/ Create report line items. Earch report is a heading. The heading contains a hyperlink to
        '/ the report (report will be display in IE), a description, and another heading which contains
        '/ a report summary. If the report contains a table, then the table field names will be listed.
        '/ </summary>
        Private Sub Reports()
            Dim catalogItemPath As String = String.Empty

            Dim catalogItem As CatalogItem
            For Each catalogItem In catalogItemArray
                If catalogItem.TypeName = "Report" Then
                    'Collapsed Heading
                    xmlQueryResponse.WriteStartElement("Heading")
                    xmlQueryResponse.WriteAttributeString("collapsed", "true")
                    xmlQueryResponse.WriteElementString("Text", catalogItem.Name)

                    xmlQueryResponse.WriteStartElement("Line")
                    xmlQueryResponse.WriteStartElement("Image")
                    xmlQueryResponse.WriteAttributeString("source", HTTPPath + System.Configuration.ConfigurationManager.AppSettings("NavLinkIcon"))
                    xmlQueryResponse.WriteEndElement()
                    xmlQueryResponse.WriteStartElement("Hyperlink")
                    xmlQueryResponse.WriteAttributeString("url", reportItemPath + catalogItem.Path)
                    xmlQueryResponse.WriteAttributeString("tooltip", String.Format(CultureInfo.InvariantCulture, Resources.Resources.GoToReport + catalogItem.Name))
                    xmlQueryResponse.WriteElementString("Text", String.Format(CultureInfo.InvariantCulture, catalogItem.Name))
                    xmlQueryResponse.WriteEndElement()
                    xmlQueryResponse.WriteEndElement()

                    'Description
                    If Not (catalogItem.Description Is Nothing) Then
                        xmlQueryResponse.WriteElementString("P", String.Format(CultureInfo.InvariantCulture, catalogItem.Description))
                    End If

                    'More details Heading
                    xmlQueryResponse.WriteStartElement("Heading")
                    xmlQueryResponse.WriteAttributeString("collapsed", "true")
                    xmlQueryResponse.WriteElementString("Text", String.Format(CultureInfo.InvariantCulture, Resources.Resources.ReportSummary))
                    'Report Preview Table
                    TabularPreview(catalogItem)
                    xmlQueryResponse.WriteEndElement()
                    xmlQueryResponse.WriteEndElement()
                End If
            Next catalogItem

        End Sub


        '/ <summary>
        '/ Render a Tabular element containing Name/Value pair for CreatedBy and CreationDate properties.
        '/ </summary>
        '/ <param name="catalogItem"></param>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId:="System.DateTime.ToString")> _
        Private Sub TabularPreview(ByVal catalogItem As CatalogItem)
            'Start Tabular
            xmlQueryResponse.WriteStartElement("Tabular")
            'CreatedBy Record
            xmlQueryResponse.WriteStartElement("Record")
            xmlQueryResponse.WriteElementString("Name", String.Format(CultureInfo.InvariantCulture, Resources.Resources.CreatedBy))
            xmlQueryResponse.WriteElementString("Value", String.Format(CultureInfo.InvariantCulture, catalogItem.CreatedBy))
            xmlQueryResponse.WriteEndElement()
            'CreationDate Record
            xmlQueryResponse.WriteStartElement("Record")
            xmlQueryResponse.WriteElementString("Name", String.Format(CultureInfo.InvariantCulture, Resources.Resources.CreationDate))
            xmlQueryResponse.WriteElementString("Value", String.Format(CultureInfo.InvariantCulture, catalogItem.CreationDate.ToString()))
            xmlQueryResponse.WriteEndElement()

            xmlQueryResponse.WriteEndElement()

        End Sub



        '/ <summary>
        '/ Connect to Reporting Services web service.
        '/ </summary>
        '/ <param name="url"></param>
        '/ <param name="credentials"></param>
        Private Sub Connect(ByVal url As String, ByVal credentials As ICredentials)
            catalogManager = New CatalogManager()
            catalogManager.Credentials = credentials

            catalogManager.Connect(url)

        End Sub
    End Class
End Namespace