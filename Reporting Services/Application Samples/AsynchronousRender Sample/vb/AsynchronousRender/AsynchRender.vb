
'=============================================================================
'  Summary:  Demonstrates an implementation of a Form that can
'            be used to render a report via the Reporting Services
'            Web service using asychronous Web service calls.
'      
'---------------------------------------------------------------------
'  This file is part of Microsoft SQL Server Code Samples.
'  
' This source code is intended only as a supplement to Microsoft
' Development Tools and/or on-line documentation.  See these other
' materials for detailed information regarding Microsoft code samples.
'
' THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
' KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
' IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
' PARTICULAR PURPOSE.
'==============================================================================
#Region "Using directives"

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports System.Globalization
Imports RS = Microsoft.Samples.ReportingServices.AsynchronousRender.Microsoft.SqlServer.ReportingServices2010
Imports RSExec = Microsoft.Samples.ReportingServices.AsynchronousRender.Microsoft.SqlServer.ReportingServices.ReportExecutionService

#End Region


Class AsynchRender
    Inherits Form

    Private rs As RS.ReportingService2010
    Private rsExec As RSExec.ReportExecutionService

    Private start As DateTime
    Private rootTreeNode As TreeNode
    Private reportName As String
    Private renderFileName As String = String.Empty

    Private Const MHTML As String = "Web Page, single file (*.mhtml)|*.mhtml"

    Private Sub CreateProxy()
        rs = New RS.ReportingService2010()

        rs.Url = serverTextBox.Text + "ReportService2010.asmx"
        rs.Credentials = System.Net.CredentialCache.DefaultCredentials

        rsExec = New RSExec.ReportExecutionService()
        rsExec.Url = serverTextBox.Text + "ReportExecution2005.asmx"
        rsExec.Credentials = System.Net.CredentialCache.DefaultCredentials
        AddHandler rsExec.RenderCompleted, AddressOf Me.RenderCompletedHandler
    End Sub

    Private Sub exitButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles exitButton.Click
        Application.Exit()
    End Sub


    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.MessageBox.Show(System.String,System.String,System.Windows.Forms.MessageBoxButtons,System.Windows.Forms.MessageBoxIcon)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.MessageBox.Show(System.String)")> _
    Private Sub renderButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles renderButton.Click
        If catalogTreeView.SelectedNode Is Nothing OrElse catalogTreeView.SelectedNode.ImageIndex = 0 OrElse catalogTreeView.SelectedNode.ImageIndex = 3 Then
            MessageBox.Show("Select Report")
        Else
            If catalogTreeView.SelectedNode.ImageIndex <> 0 Then
                ' If the report uses parameters for which there is no default
                ' value, then the report cannot be rendered and saved by this
                ' application
                Dim hasDefaultValue As Boolean = ParametersHaveDefaultValue(CStr(catalogTreeView.SelectedNode.Tag))

                If Not hasDefaultValue Then
                    MessageBox.Show( _
                    "Sample does not support rendering a report with a ReportParameter." _
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                Else
                    RenderReport()
                End If
            End If
        End If
    End Sub

    Private Function ParametersHaveDefaultValue(ByVal path As String) As Boolean
        Dim haveDefaultValue As Boolean = True
        Dim forRendering As Boolean = False
        Dim historyID As String = Nothing
        Dim values As RS.ParameterValue() = Nothing
        Dim credentials As RS.DataSourceCredentials() = Nothing
        Dim parameters As RS.ItemParameter() = Nothing

        parameters = rs.GetItemParameters(path, historyID, forRendering, values, credentials)

        Dim parameter As RS.ItemParameter
        For Each parameter In parameters
            If parameter.DefaultValues Is Nothing Then
                haveDefaultValue = False
                Exit For
            End If
        Next parameter

        Return haveDefaultValue

    End Function

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId:="SessionId")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> Private Sub RenderReport()
        ' Render arguments
        Dim reportPath As String = CStr(catalogTreeView.SelectedNode.Tag)
        reportName = catalogTreeView.SelectedNode.Text
        Dim format As String = "MHTML"
        Dim devInfo As String = Nothing
        Dim historyID As String = Nothing
        Dim execHeader As New RSExec.ExecutionHeader()
        rsExec.ExecutionHeaderValue = execHeader

        Try
            rsExec.LoadReport(reportPath, historyID)

            Dim SessionId As String = rsExec.ExecutionHeaderValue.ExecutionID

            saveReportDialog = New SaveFileDialog()
            saveReportDialog.Filter = MHTML
            saveReportDialog.FileName = reportName

            Dim dr As DialogResult = saveReportDialog.ShowDialog()

            If dr = Windows.Forms.DialogResult.OK Then
                renderFileName = saveReportDialog.FileName

                start = DateTime.Now
                statusTimer.Start()

                rsExec.RenderAsync(format, devInfo)

                rsExec.GetExecutionInfo()

            Else
                statusTimer.Stop()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.MessageBox.Show(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> _
    Private Sub RenderCompletedHandler(ByVal sender As Object, ByVal arg As RSExec.RenderCompletedEventArgs)
        statusTimer.Stop()

        Try
            SaveReport(arg.Result, renderFileName)
        Catch
            MessageBox.Show("A report rendering is currently executing.")
        End Try

    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")> Private Sub SaveReport(ByVal results() As Byte, ByVal location As String)
        ' Create a file stream and write the report to it
        Dim stream As FileStream = File.OpenWrite(location)
        stream.Write(results, 0, results.Length)
        stream.Close()

    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId:="System.String.Format(System.String,System.Object)")> Private Sub statusTimer_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles statusTimer.Tick
        Dim elapsedTime As TimeSpan = DateTime.Now.Subtract(start)
        Dim formattedShortTime As New TimeSpan(elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds)

        statusTextBox.Text = String.Format(" ({0})", formattedShortTime)

    End Sub


    ' Method to add child nodes to a selected node
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.TreeNode.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> Private Sub AddChildNodes(ByVal currentNode As TreeNode)
        ' If the node is not a folder, don't call ListChildren
        If currentNode.ImageIndex <> 0 Then
            Return
        End If

        Try
            ' List the children of the current report server folder item
            Dim items As RS.CatalogItem() = rs.ListChildren(currentNode.Tag.ToString(), False)
            ' For each item, add node to the current node
            Dim ci As RS.CatalogItem
            For Each ci In items

                Dim newNode As New TreeNode()
                newNode.Text = ci.Name
                newNode.Tag = CType(ci.Path, Object)
                newNode.ImageIndex = GetItemTypeIndex(ci.TypeName)
                newNode.SelectedImageIndex = newNode.ImageIndex

                currentNode.Nodes.Add(newNode)
                AddChildNodes(newNode)
            Next ci
        Catch
            rootTreeNode.Text = "Connection Error"
        End Try

    End Sub

    ' Method to retrive images for the different node types
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")> Private Function GetItemTypeIndex(ByVal TypeName As String) As Integer
        Dim index As Integer = 2
        Select Case TypeName
            Case "Folder"
                index = 0
            Case "DataSource"
                index = 3
            Case "LinkedReport"
                index = 1
            Case "Report"
                index = 1
            Case "Resource"
                index = 2
            Case Else
        End Select

        Return index

    End Function


    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.TreeNode.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> Private Sub connectButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles connectButton.Click
        catalogTreeView.Nodes.Clear()
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        CreateProxy()
        rootTreeNode = New TreeNode()
        rootTreeNode.Text = "Home"
        rootTreeNode.Tag = CType("/", Object)
        rootTreeNode.ImageIndex = 0
        rootTreeNode.SelectedImageIndex = 0
        catalogTreeView.Nodes.Add(rootTreeNode)
        AddChildNodes(rootTreeNode)
        renderButton.Enabled = True
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        connectButton.Text = "Refresh"

    End Sub


    Private Sub statusLabel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles statusLabel.Click

    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.TextBoxBase.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> Private Sub AsynchRender_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        serverTextBox.Text = "http://localhost/ReportServer/"
    End Sub
End Class
