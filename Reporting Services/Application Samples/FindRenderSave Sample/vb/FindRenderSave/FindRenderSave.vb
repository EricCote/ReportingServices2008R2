#Region "Copyright © 2004 Microsoft Corporation. All rights reserved."
'==============================================================================
'  File:      FindRenderSave.vb
'
'  Summary:  Demonstrates an implementation of a Form that can
'            be used to render a report via the Reporting Services
'            Web service using asychronous Web service calls.
'
'---------------------------------------------------------------------
'  This file is part of the Microsoft SQL Server Code Samples.
'  Copyright (C) Microsoft Corporation.  All rights reserved.
'
'  This source code is intended only as a supplement to Microsoft
'  Development Tools and/or on-line documentation.  See these other
'  materials for detailed information regarding Microsoft code samples.
'
'  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
'  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
'  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
'  PARTICULAR PURPOSE.
'==============================================================================
#End Region

Imports System
Imports System.ComponentModel
Imports System.IO
Imports System.Text
Imports System.Web.Services.Protocols
Imports rs2010 = Microsoft.Samples.ReportingServices.FindRenderSave.Microsoft.SqlServer.ReportingServices2010
Imports Microsoft.Samples.ReportingServices.FindRenderSave.ReportExecution2005
Imports Microsoft.Samples.ReportingServices.FindRenderSave.My.Resources
Imports System.Configuration
Imports Microsoft.Samples.ReportingServices.FindRenderSave.Microsoft.SqlServer.ReportingServices2010


Public Class FindRenderSave
    ' User defined variables.
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public selItem As CatalogItem

    Private rs As ReportingService2010
    Private rsExec As ReportExecutionService

    Private returnedItems() As CatalogItem
    Private Const REPORT_NAME As Integer = 0
    Private Const DESC As Integer = 1
    Private Const BOTH As Integer = 2

    ' Variables used for save dialog filters
    Private Const PDF As String = "PDF file (*.pdf)|*.pdf"
    Private Const IMAGE As String = "Tiff file (*.tif)|*.tif"
    Private Const MHTML As String = "Web Page, single file (*.mhtml)|*.mhtml"
    Private Const EXCEL As String = "Microsoft Excel Workbook (*.xls)|*.xls"

    Private Sub reportListView_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles reportListView.SelectedIndexChanged
        ' Once a report is selected, enable the save button.
        saveReportButton.Enabled = True

        descriptionTextBox.Clear()
        pathTextBox.Clear()
        If reportListView.SelectedItems.Count > 0 Then
            Dim catalogItem As CatalogItem = CType(reportListView.SelectedItems(0), CatalogListViewItem).Item

            'Show the description
            If Not (catalogItem.Description Is Nothing) Then
                descriptionTextBox.Text = catalogItem.Description
            End If

            ' Show the path
            pathTextBox.Text = catalogItem.Path
        End If
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> Private Sub saveReportButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles saveReportButton.Click
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

        ' Define variables needed for the Render() method.
        Dim historyID As String = Nothing
        Dim credentials As rs2010.DataSourceCredentials() = Nothing
        Dim reportHistoryParameters As rs2010.ParameterValue() = Nothing

        ' Define variables needed for GetParameters() method
        Dim forRendering As Boolean = False
        Dim parameters As ItemParameter() = Nothing
        Dim noDefault As Boolean = False

        ' Create a variable containing the selected item
        Me.selItem = CType(reportListView.SelectedItems(0), CatalogListViewItem).Item

        Try
            ' If the report uses parameters for which there is no default
            ' value, then the report cannot be rendered and saved by this
            ' application
            parameters = rs.GetItemParameters(Me.selItem.Path, historyID, forRendering, reportHistoryParameters, credentials)

            Dim parameter As ItemParameter
            For Each parameter In parameters
                If parameter.DefaultValues Is Nothing Then
                    noDefault = True
                    Exit For
                End If
            Next parameter

            If noDefault Then
                MessageBox.Show(My.Resources.missingDefaultParametersErrorMessage, _
                    My.Resources.missingDefaultParametersMessageBoxTitle, _
                    MessageBoxButtons.OK, MessageBoxIcon.Error, _
                    MessageBoxDefaultButton.Button1, 0)
            Else
                SaveAs()
            End If

        Catch exception As Exception
            HandleException(exception)

        Finally
            System.Windows.Forms.Cursor.Current = Cursors.Default
        End Try
    End Sub

    ' Utility method that is used to simplify populating the Save Dialog 
    ' with the appropriate file filters.
    Private Function GetFilterString() As String
        Select Case formatComboBox.Text
            Case "MHTML"
                Return MHTML

            Case "PDF"
                Return PDF

            Case "IMAGE"
                Return IMAGE

            Case "EXCEL"
                Return EXCEL

            Case Else
                Return String.Empty
        End Select
    End Function

    '/ <summary>
    '/ When the search button is selected, connect to the Web service
    '/ and return a list of reports that meets the search conditions
    '/ defined by the user.
    '/ </summary>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> Private Sub searchButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles searchButton.Click
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

        ' Clear out the current descripton and path fields
        descriptionTextBox.Clear()
        pathTextBox.Clear()

        ' Disable save button on new search
        saveReportButton.Enabled = False

        ' Check to see if the 'Search By' string is valid.
        If conditionComboBox.SelectedIndex = -1 Then
            MessageBox.Show( _
                    "Please select a valid 'Search By' string by clicking the drop down arrow!", _
                    "Invalid 'Search By' String", _
                    MessageBoxButtons.OK, _
                    MessageBoxIcon.Error)
            Return
        End If

        ' Check to see if a search string is entered
        If String.IsNullOrEmpty(searchTextBox.Text) = True Then
            MessageBox.Show(My.Resources.invalidSearchStringErrorMessage, _
                My.Resources.invalidSearchStringMessageBoxTitle, _
                MessageBoxButtons.OK, MessageBoxIcon.Error, _
                MessageBoxDefaultButton.Button1, 0)
        Else
            reportListView.Items.Clear()

            ' Create a new proxy to the web service
            rs = New ReportingService2010()
            rsExec = New ReportExecutionService()

            ' Authenticate to the Web service using Windows credentials
            rs.Credentials = System.Net.CredentialCache.DefaultCredentials
            rsExec.Credentials = System.Net.CredentialCache.DefaultCredentials

            ' Assign the URL of the Web service
            rs.Url = ConfigurationManager.AppSettings("ReportingService2010")
            rsExec.Url = ConfigurationManager.AppSettings("ReportExecutionService")

            Dim conditions() As SearchCondition

            If conditionComboBox.SelectedIndex = REPORT_NAME Then
                ' Create Name search condition
                Dim condition As New SearchCondition()
                condition.Condition = ConditionEnum.Contains
                condition.ConditionSpecified = True
                condition.Name = "Name"
                Dim val() As String = {searchTextBox.Text}
                condition.Values = val


                conditions = New SearchCondition(0) {}
                conditions(0) = condition
            ElseIf conditionComboBox.SelectedIndex = DESC Then
                ' Create Description search condition
                Dim condition As New SearchCondition()
                condition.Condition = ConditionEnum.Contains
                condition.ConditionSpecified = True
                condition.Name = "Description"
                Dim val() As String = {searchTextBox.Text}
                condition.Values = val

                ' Add conditions to the conditions argument to be used for 
                ' FindItems
                conditions = New SearchCondition(0) {}
                conditions(0) = condition
            Else
                ' Create Name
                Dim nameCondition As New SearchCondition()
                nameCondition.Condition = ConditionEnum.Contains
                nameCondition.ConditionSpecified = True
                nameCondition.Name = "Name"
                Dim val() As String = {searchTextBox.Text}
                nameCondition.Values = val

                ' Create Desription
                Dim descCondition As New SearchCondition()
                descCondition.Condition = ConditionEnum.Contains
                descCondition.ConditionSpecified = True
                descCondition.Name = "Description"                
                descCondition.Values = val

                ' Add conditions to the conditions argument to be used for 
                ' FindItems
                conditions = New SearchCondition(1) {}
                conditions(0) = nameCondition
                conditions(1) = descCondition
            End If

            Try
                ' Return a list of items based on the search conditions that 
                ' apply

                Dim SearchOptions(1) As Microsoft.SqlServer.ReportingServices2010.Property
                Dim SearchOption As New Microsoft.SqlServer.ReportingServices2010.Property
                SearchOption.Name = "Recursive"
                SearchOption.Value = "True"
                SearchOptions(0) = SearchOption

                returnedItems = rs.FindItems("/", BooleanOperatorEnum.Or, SearchOptions, conditions)

                If Not (returnedItems Is Nothing) AndAlso returnedItems.Length <> 0 Then
                    Dim ci As CatalogItem
                    For Each ci In returnedItems
                        'Create a ListView item containing a report catalog item
                        If ci.TypeName = "Report" Then
                            ' Add the items to the list view
                            Dim newItem As New CatalogListViewItem(ci)
                            reportListView.Items.Add(newItem)
                        End If
                    Next ci
                Else
                    MessageBox.Show(My.Resources.noItemsFoundInfoMessage, _
                        My.Resources.noItemsFoundMessageBoxTitle, _
                        MessageBoxButtons.OK, MessageBoxIcon.Information, _
                        MessageBoxDefaultButton.Button1, 0)
                End If

            Catch exception As Exception
                HandleException(exception)

            Finally
                System.Windows.Forms.Cursor.Current = Cursors.Default
            End Try
        End If
    End Sub

    Private Sub closeButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles closeButton.Click
        Application.Exit()
    End Sub

    ''' <summary>
    ''' Method to save the selected item
    ''' </summary>
    ''' <remarks></remarks>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId:="showHide")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId:="ei")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> Private Sub SaveAs()
        ' Create instance of save dialog and set default
        ' filename and filter
        saveReportDialog = New SaveFileDialog()
        saveReportDialog.Filter = GetFilterString()
        saveReportDialog.FileName = Me.selItem.Name

        ' Open the save file dialog
        Dim dr As DialogResult = saveReportDialog.ShowDialog()

        ' If the user selects a valid item and clicks OK
        If dr = Windows.Forms.DialogResult.OK Then
            ' Store the filename of the report
            Dim fileName As String = saveReportDialog.FileName

            ' Prepare Render arguments
            Dim historyID As String = Nothing
            Dim deviceInfo As String = Nothing
            Dim format As String = formatComboBox.Text
            Dim showHide As String = Nothing
            Dim results() As [Byte]
            Dim warnings As ReportExecution2005.Warning() = Nothing
            Dim streamIDs As String() = Nothing
            Dim encoding As String = String.Empty
            Dim mimeType As String = String.Empty

            Dim ei As ExecutionInfo = rsExec.LoadReport(selItem.Path, historyID)

            'Exectute the report and save it into a file.
            Try
                results = rsExec.Render(format, deviceInfo, encoding, mimeType, warnings, streamIDs)

                ' Create a file stream and write the report to it
                Using stream As FileStream = File.OpenWrite(fileName)
                    stream.Write(results, 0, results.Length)
                End Using

            Catch exception As Exception
                HandleException(exception)
            End Try
        End If
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId:="exception")> Private Shared Sub HandleException(ByVal exception As Exception)
        'Dim exceptionText As String
        ' Find out if the exception is a SOAP exception and make use of the
        ' SOAP exception Detail property.
        'If TypeOf exception Is SoapException Then
        '    exceptionText = CType(exception, SoapException).Detail("Message").InnerXml
        'Else
        '    exceptionText = exception.Message
        'End If

        MessageBox.Show(My.Resources.genericErrorMessage, _
            My.Resources.genericErrorMessageBoxTitle, MessageBoxButtons.OK, _
            MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 0)
    End Sub
End Class
