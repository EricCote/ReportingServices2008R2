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

Imports System
Imports System.Net
Imports System.Collections
Imports System.Xml
Imports System.Xml.XPath
Imports System.Text.RegularExpressions
Imports System.Web.Services.Protocols
Imports System.Globalization
Imports Microsoft.SqlServer.ReportingServices2010

Namespace Microsoft.Samples.SqlServer
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")> _
    Public Class CatalogManager
#Region "Constants"
        Private Const wsdl As String = "/ReportService2010.asmx"
        Private Const Root As String = "/"
        Private Const m_MaxFileUploadSize As Integer = 4096
#End Region

#Region "Private Member Variables	"

        Private m_url As String
        Private m_soapUrl As String
        Private m_service As ReportingService2010
        Private m_credentials As ICredentials

#End Region

#Region "Constructors"


        Public Sub New()

        End Sub


        Public Sub New(ByVal url As String)

            Me.m_url = url
            Me.m_soapUrl = Me.m_url + wsdl

            Me.m_service.Url = Me.m_soapUrl

        End Sub

#End Region

#Region "RSWebService"

        Public ReadOnly Property RSWebService() As ReportingService2010
            Get
                Return m_service
            End Get
        End Property

#Region "How To: Connect to Reporting Services"

        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId:="0#")> _
        Public Sub Connect(ByVal url As String)
            Me.m_service = New ReportingService2010()
            Me.m_service.Credentials = Me.Credentials
            Me.m_url = url
            Me.m_soapUrl = Me.m_url + wsdl

            Me.m_service.Url = Me.m_soapUrl

        End Sub
#End Region
#End Region

#Region "How To: RS Methods"


#Region "How To: Create a Folder with description property"

        Public Overloads Sub CreateFolder(ByVal path As String, ByVal name As String, ByVal description As String)
            If String.IsNullOrEmpty(path) Or String.IsNullOrEmpty(name) Then
                Throw New ArgumentException(String.Format(CultureInfo.InvariantCulture, Resources.Resources.InvalidParameter))
            Else
                'Create Property array for description
                Dim rsProperties As [Property]() = Me.CreateDescriptionProperty(description)

                'Call RS CreateFolder() base method
                Me.CreateFolder(name, path, rsProperties)
            End If

        End Sub



        '/ <summary>
        '/ RSWebService Wrapper Method called by CreateFolder(path, name, description )
        '/ </summary>
        '/ <param name="folderName"></param>
        '/ <param name="parentPath"></param>
        '/ <param name="properties"></param>
        Public Overloads Sub CreateFolder(ByVal folderName As String, ByVal parentPath As String, ByVal properties() As [Property])
            Me.RSWebService.CreateFolder(folderName, parentPath, properties)

        End Sub


#End Region

#Region "How To: Get a list of child CatalogItems"

        '/ <summary>
        '/ Returns CatalogItem
        '/ </summary>
        '/ <param name="path"></param>
        '/ <returns></returns>
        Public Function ListChildren(ByVal path As String) As CatalogItem()
            Return Me.RSWebService.ListChildren(path, False)

        End Function


        Public Function AllFolders() As CatalogItem()
            Return Me.RSWebService.ListChildren("/", True)

        End Function
#End Region

#Region "How To: Delete multiple CatalogItems within a Batch"

        Public Sub DeleteItems(ByVal path As String, ByVal catalogItems As ArrayList)
            Dim itemPath As String

            'Create batch header - BathchFunction are decremented in 2010
            'Dim singleBatchHeader As New BatchHeader()
            'Set EditItem batch id and top BatchHeaderValue
            'singleBatchHeader.BatchID = Me.RSWebService.CreateBatch()
            'Me.RSWebService.BatchHeaderValue = singleBatchHeader

            'Call DeleteItem methods
            Dim item As String
            For Each item In catalogItems
                itemPath = Me.GetCleanPath(path, item)
                Me.DeleteItem(itemPath)
            Next item
            'Execute DeleteItem methods in a batch
            'Me.RSWebService.ExecuteBatch()

            'Me.RSWebService.BatchHeaderValue = Nothing

        End Sub


        Public Function GetCleanPath(ByVal path As String, ByVal item As String) As String
            ' If the item is in the root don't include the path.
            ' This avoids double forward slash "//Pathname" problems
            Dim itemPath As String
            If path <> Root Then
                itemPath = path + Root + item
            Else
                itemPath = Root + item
            End If

            Return itemPath

        End Function

#End Region

#Region "How To: Delete an item"

        Public Sub DeleteItem(ByVal path As String)
            Me.RSWebService.DeleteItem(path)

        End Sub
#End Region

#Region "How To: Edit a CatalogItem using a CreateBatch/ExecuteBatch"

        'How To: Excecute multiple methods with CreateBatch(), CancelBatch() and ExecuteBatch()
        'How To: Set CatalogItem Properties
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")> _
        Public Sub EditItem(ByVal oldPath As String, ByVal newPath As String, ByVal description As String)
            Dim rsProperties As [Property]() = Me.CreateDescriptionProperty(description)

            Try
                'Rename Item and Create new Properties based on old properties
                'Create batch header - Batch Header is Deprecated in RS 2010 endpoint
                'Dim singleBatchHeader As New BatchHeader()
                'Set EditItem batch id and top BatchHeaderValue
                'singleBatchHeader.BatchID = Me.RSWebService.CreateBatch()
                'Me.RSWebService.BatchHeaderValue = singleBatchHeader

                'Call methods to batch
                Me.RSWebService.MoveItem(oldPath, newPath)
                Me.RSWebService.SetProperties(newPath, rsProperties)
                'Test code. 
                ' this.SetProperties( newPath, null ); causes a failed method call which results in
                ' a rollback of the MoveItem changes
                'Rollback transaction if either method fails
                'Me.RSWebService.ExecuteBatch()

                'Me.RSWebService.BatchHeaderValue = Nothing
            Catch
                'BatchOperationFailed
                Throw New Exception(String.Format(CultureInfo.InvariantCulture, Resources.Resources.BatchOperationFailed))
            End Try

        End Sub

#End Region

#Region "How To: Copy a Report, Resource or DataSource to a new path	"

        Public Function CopyReport(ByVal name As String, ByVal oldPath As String, ByVal newPath As String) As Warning()
            'GetReportDefinition()
            'CreateReport
            Dim warning As Warning() = Nothing
            Dim reportDefinition As Byte() = Nothing

            reportDefinition = Me.RSWebService.GetItemDefinition(oldPath)
            Me.RSWebService.CreateCatalogItem("Report", name, newPath, True, reportDefinition, Nothing, warning)

            Return warning

        End Function


        'Public Sub CopyResource(ByVal name As String, ByVal oldPath As String, ByVal newPath As String)
        '    Dim resourceContents As Byte() = Nothing
        '    Dim mimeType As String = String.Empty

        '    resourceContents = Me.RSWebService.GetResourceContents(oldPath, mimeType)
        '    Me.RSWebService.CreateResource(name, newPath, True, resourceContents, mimeType, Nothing)

        'End Sub


        'Public Sub CopyDataSource(ByVal dataSource As String, ByVal oldPath As String, ByVal newPath As String)
        '    Dim dsDefinition As DataSourceDefinition = Nothing

        '    dsDefinition = Me.RSWebService.GetDataSourceContents(oldPath)
        '    Me.RSWebService.CreateDataSource(dataSource, newPath, True, dsDefinition, Nothing)

        'End Sub


#End Region

#Region "How To: Get CatalogItem properties from a path"

        '/ <summary>
        '/ 
        '/ </summary>
        '/ <param name="path"></param>
        '/ <param name="properties"></param>
        '/ <returns></returns>
        Public Function GetProperties(ByVal path As String, ByVal properties() As [Property]) As [Property]()
            Return Me.RSWebService.GetProperties(path, properties)

        End Function

#End Region

#Region "How To: Import a Report"

        '/ <summary>
        '/ The sample will not replace existing reports.
        '/ </summary>
        '/ <param name="reportName"></param>
        '/ <param name="path"></param>
        '/ <param name="reportDefinition"></param>
        '/ <returns></returns>
        Public Function CreateReport(ByVal reportName As String, ByVal path As String, ByVal reportDefinition() As [Byte]) As Warning()
            Dim warnings As Warning()
            RSWebService.CreateCatalogItem("Report", reportName, path, False, reportDefinition, Nothing, warnings)

            Return warnings

        End Function

#End Region

#Region "How To: Find reports "

        Public Function FindItems(ByVal queryText As String) As CatalogItem()
            'Create conditions
            Dim condition As New SearchCondition()
            condition.Condition = ConditionEnum.Contains
            condition.ConditionSpecified = True
            condition.Name = "Name"
            Dim val() As String = {queryText}
            condition.Values = val

            Dim conditions(0) As SearchCondition
            conditions(0) = condition


            Dim SearchOptions(1) As Microsoft.SqlServer.ReportingServices2010.Property
            Dim SearchOption As New Microsoft.SqlServer.ReportingServices2010.Property
            SearchOption.Name = "Recursive"
            SearchOption.Value = "True"
            SearchOptions(0) = SearchOption

            Return Me.RSWebService.FindItems("/", BooleanOperatorEnum.Or, SearchOptions, conditions)

        End Function


        Public Function AdvancedSearch(ByVal folder As String, ByVal booleanOperator As BooleanOperatorEnum, ByVal searchConditions() As SearchCondition) As CatalogItem()

            Dim SearchOptions(1) As Microsoft.SqlServer.ReportingServices2010.Property
            Dim SearchOption As New Microsoft.SqlServer.ReportingServices2010.Property
            SearchOption.Name = "Recursive"
            SearchOption.Value = "True"
            SearchOptions(0) = SearchOption

            Return Me.RSWebService.FindItems(folder, booleanOperator, SearchOptions, searchConditions)

        End Function

#End Region

#End Region

#Region "Properties"


        Public Property Credentials() As ICredentials
            Get
                Return Me.m_credentials
            End Get

            Set(ByVal value As ICredentials)
                Me.m_credentials = Value
            End Set
        End Property
#End Region

#Region "Utility Members"

        Public Function GetParentPath(ByVal currentPath As String) As String
            Dim delimiter As String = "/"
            Dim rx As New Regex(delimiter)
            Dim childPath As String() = rx.Split(currentPath)

            Dim parentLength As Integer = childPath.Length - 1
            Dim parentPath(parentLength) As String

            Dim i As Integer
            For i = 0 To parentLength
                parentPath(i) = childPath(i)
            Next i
            If parentPath.Length = 1 Then
                Return "/"
            Else
                Return String.Join("/", parentPath)
            End If

        End Function


        '/ <summary>
        '/ 
        '/ </summary>
        '/ <returns></returns>
        Public Function ServerName() As String
            Dim rx As New Regex("//")
            Dim tempArray As String() = rx.Split(Me.m_url)
            Dim path As String = tempArray(1)
            rx = New Regex("/")
            tempArray = rx.Split(path)
            Dim server As String = tempArray(0)

            Return server

        End Function


        '/ <summary>
        '/ Create a Property Array 
        '/ </summary>
        '/ <param name="Name"></param>
        '/ <returns></returns>
        Public Function CreatePropertesArray(ByVal name As String) As [Property]()
            Dim rsProperty As New [Property]()
            rsProperty.Name = name
            Dim rsProperties(0) As [Property]
            rsProperties(0) = rsProperty

            Return rsProperties

        End Function


        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")> _
        Public Function GetProperty(ByVal path As String, ByVal name As String) As String
            Dim propValue As String = String.Empty
            Try
                Dim propArray As [Property]() = Me.CreatePropertesArray(name)
                Dim properties As [Property]() = Me.m_service.GetProperties(path, propArray)
                Dim prop As [Property]
                For Each prop In properties
                    If prop.Name = name Then
                        propValue = prop.Value
                        Exit For
                    End If
                Next prop
            Catch
                Throw New Exception(String.Format(CultureInfo.InvariantCulture, Resources.Resources.GetPropertyError))
            End Try
            Return propValue

        End Function


        '<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")> _
        'Public Function GetTypeIndex(ByVal type As ItemTypeEnum) As Integer
        '    Dim typeIndex As Integer
        '    Select Case type
        '        Case ItemTypeEnum.Folder
        '            typeIndex = 0
        '        Case ItemTypeEnum.Report
        '            typeIndex = 1
        '        Case ItemTypeEnum.Resource
        '            typeIndex = 2
        '        Case ItemTypeEnum.LinkedReport
        '            typeIndex = 3
        '        Case ItemTypeEnum.DataSource
        '            typeIndex = 4
        '        Case Else
        '            Throw New Exception(String.Format(CultureInfo.InvariantCulture, Resources.Resources.NoTypeInformation + type))
        '    End Select
        '    Return typeIndex

        'End Function


        '/ <summary>
        '/ Helper method for sample purposes only. A more robust application would create a general
        '/ purpose property method.
        '/ </summary>
        '/ <returns></returns>
        Private Function CreateDescriptionProperty(ByVal description As String) As [Property]()
            Dim rsProperties(0) As [Property]
            Dim rsProperty As New [Property]()
            rsProperty.Name = "Description"
            rsProperty.Value = description
            rsProperties(0) = rsProperty

            Return rsProperties

        End Function
#End Region
    End Class
End Namespace