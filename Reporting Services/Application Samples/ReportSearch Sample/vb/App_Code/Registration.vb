'-----------------------------------------------------------------------
' This file is part of the Microsoft Code Samples.
'
' Copyright (C) Microsoft Corporation.  All rights reserved.
' 
' This source code is intended only as a supplement to Microsoft
' Development Tools and/or on-line documentation.  See these other
' materials for detailed information regarding Microsoft code samples.
'
' THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
' KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
' IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
' PARTICULAR PURPOSE.
'
' Summary:  Demonstrates a Microsoft Office 2003 Research Pane for Microsoft 
' SQL Server Reporting Service. The Registration web service is required for
' a Research Pane. The Registration web service is used by Microsoft Office 2003 to
' create a reference to the Query web service.
'---------------------------------------------------------------------

Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web
Imports System.Web.Configuration
Imports System.Web.Services
Imports System.Xml
Imports System.IO
Imports System.Globalization

Namespace Microsoft.Samples.SqlServer
    '/ <summary>
    '/  The web service must have the urn:Microsoft.Search namespace attribute.
    '/ </summary>
    <WebService([Namespace]:="urn:Microsoft.Search")> _
    Public Class ReportServerPane
        Inherits System.Web.Services.WebService

        Public Sub New()

        End Sub


        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId:="System.IO.StringWriter.#ctor")> <WebMethod()> _
        Public Function Registration() As String
            Dim output As New StringWriter()
            Dim regString As String = String.Empty
            Dim settings As New XmlWriterSettings()
            settings.ConformanceLevel = ConformanceLevel.Auto

            Dim writer As XmlWriter = XmlWriter.Create(output, settings)

            ' Write XML data.
            writer.WriteRaw("<?xml version=""1.0"" encoding=""utf-8""?>")
            writer.WriteRaw("<ProviderUpdate xmlns=""urn:Microsoft.Search.Registration.Response"">")
            writer.WriteStartElement("Status")
            writer.WriteValue(Resources.Resources.Status)
            writer.WriteEndElement()
            writer.WriteStartElement("Providers")
            writer.WriteStartElement("Provider")
            writer.WriteStartElement("Message")
            writer.WriteValue(Resources.Research.Message)
            writer.WriteEndElement()
            writer.WriteStartElement("Id")
            writer.WriteValue(Resources.Research.RegistrationId)
            writer.WriteEndElement()
            writer.WriteStartElement("Name")
            writer.WriteValue(Resources.Research.RegistrationName)
            writer.WriteEndElement()

            Dim HTTPPath As String = System.Configuration.ConfigurationManager.AppSettings("SearchServerPath")
            writer.WriteStartElement("QueryPath")
            writer.WriteValue(HTTPPath + "/" + Resources.Research.QueryPath)
            writer.WriteEndElement()
            writer.WriteStartElement("RegistrationPath")
            writer.WriteValue(Resources.Research.RegistrationPath)
            writer.WriteEndElement()
            writer.WriteRaw("<Type>SOAP</Type>")

            writer.WriteStartElement("Services")
            writer.WriteStartElement("Service")
            writer.WriteStartElement("Id")
            writer.WriteValue(Resources.Research.ServiceId)
            writer.WriteEndElement()
            writer.WriteStartElement("Name")
            writer.WriteValue(Resources.Research.ServiceName)
            writer.WriteEndElement()
            writer.WriteStartElement("Description")
            writer.WriteValue(Resources.Research.ServiceDescription)
            writer.WriteEndElement()
            writer.WriteStartElement("Copyright")
            writer.WriteValue(Resources.Research.Copyright)
            writer.WriteEndElement()
            writer.WriteRaw("<Display>On</Display>")
            writer.WriteRaw("<Category>INTRANET_GENERAL</Category>")
            writer.WriteEndElement()
            writer.WriteEndElement()
            writer.WriteEndElement()
            writer.WriteEndElement()
            writer.WriteRaw("</ProviderUpdate>")

            writer.Flush()
            Using output
                regString = output.ToString()
            End Using
            Return regString

        End Function


        <WebMethod()> _
        Public Function Status() As String
            Return String.Format(CultureInfo.InvariantCulture, Resources.Resources.Status)

        End Function
    End Class
End Namespace