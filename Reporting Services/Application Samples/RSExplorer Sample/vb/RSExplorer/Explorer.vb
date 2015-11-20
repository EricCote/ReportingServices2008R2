'------------------------------------------------------------------------------
'  This file is part of the Microsoft SQL Server Code Samples.
'
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
'============================================================================

Option Strict On
Option Explicit On

Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web.Services.Protocols
Imports System.Xml
Imports System.Xml.XPath
Imports System.IO
Imports System.Net
Imports Microsoft.Samples.ReportingServices.RSExplorer.Microsoft.SqlServer.ReportingServices2010


'<summary>
'Summary description for Explorer.
'</summary>

Public Class Explorer
    Inherits System.Windows.Forms.Form

#Region "Constants"

    Private Const wsdl As String = "/ReportService2010.asmx"
    Private Const Root As String = "/"
    Private Const _MaxFileUploadSize As Integer = 4096

#End Region

#Region "Private Member Variables"

    Private soapUrl As String
    Private rs As ReportingService2010
    Private m_path As String
    Private m_lastShortName As String
    Private m_copyList As ArrayList

    'Classes related to UI
    Private copyProgress As CopyProgress
    'Forms
    Private m_addFolder As New AddFolder()
    Private m_editItem As New EditItem()

#End Region

#Region "How To: Reporting Services"
#Region "How To: Connect to ReportingService2010s"

    Private Sub goButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles goButton.Click
        Me.ConnectToServer()
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> Private Sub ConnectToServer()
        ' Init UI
        EnableButtons(True)
        Me.Path = Root
        Dim serverPath As String = serverPathTextbox.Text
        Try
            ' Connect to Reporting Services
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            rs = New ReportingService2010()
            ' A production application would perform a complete check on the url path
            Me.Connect(serverPath)
            ' Display root items
            DisplayItems(Path)
        Catch ex As Exception
            EnableButtons(False)
            Me.HandleGeneralException(ex)
        Finally
            System.Windows.Forms.Cursor.Current = Cursors.Default
        End Try
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId:="0#")> Public Sub Connect(ByVal url As String)
        ' Create an instance of the Web service proxy and set the Url property
        rs = New ReportingService2010()
        rs.Credentials = System.Net.CredentialCache.DefaultCredentials
        soapUrl = url + wsdl

        rs.Url = soapUrl
    End Sub

#End Region

#Region "How To: Display CatalogItems in a Listview"

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> Private Sub DisplayItems(ByVal path As String)
        Dim catalogItems As CatalogItem() = Nothing

        ' Change UI state
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        explorerListView.Items.Clear()
        upButton.Enabled = True
        If Me.Path = "/" Then
            upButton.Enabled = False
        End If
        ' Call RS ListChildren
        catalogItems = rs.ListChildren(path, False)

        Try
            ' Main part of method 
            If Not (catalogItems Is Nothing) Then
                For Each ci As CatalogItem In catalogItems
                    ' Create a ListView item containing a CatalogItem
                    Dim newItem As New CatalogListViewItem(ci)
                    newItem.ImageIndex = GetTypeIndex(newItem.Item.TypeName)
                    explorerListView.Items.Add(newItem)
                Next
            End If
        Catch ex As Exception
            Me.HandleGeneralException(ex)
        Finally
            ' Update and restore UI status 
            SetFormText()
            statusBar1.Text = explorerListView.Items.Count.ToString( _
                System.Globalization.CultureInfo.InvariantCulture) & " objects"
            System.Windows.Forms.Cursor.Current = Cursors.Default
        End Try
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Exception.#ctor(System.String)")> Public Shared Function GetTypeIndex(ByVal TypeName As String) As Integer
        Dim typeIndex As Integer
        Select Case TypeName
            Case "Folder"
                typeIndex = 0
            Case "Report"
                typeIndex = 1
            Case "Resource"
                typeIndex = 2
            Case "LinkedReport"
                typeIndex = 3
            Case "DataSource"
                typeIndex = 4
            Case Else
                Dim exString As String = "No type information available for " + TypeName
                Throw New Exception(exString)
        End Select

        Return typeIndex
    End Function

#End Region

#Region "How To: Get CatalogItem Properties from a path"

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> Private Sub FillPropertiesListview(ByVal selItem As CatalogItem)
        Dim [property] As [Property]() = Nothing
        ' Clear Listview
        propertiesListview.Items.Clear()

        Try
            ' Properties parameter is null so all properties for the specified item are returned.
            [property] = rs.GetProperties(selItem.Path, Nothing)

            ' Display properties in a Listview 
            For Each prop As [Property] In [property]
                Dim lstItem As ListViewItem = propertiesListview.Items.Add(prop.Name)
                lstItem.SubItems.Add(prop.Value)
            Next
        Catch ex As Exception
            ' Note: In a production application you would want to use a specific exception type 
            Me.HandleGeneralException(ex)
        End Try
    End Sub
#End Region

#Region "How To: Create a Folder with a description property"


    ' <summary>
    ' Create a Folder in CatalogManager
    ' <seealso cref=""/>
    ' </summary>
    ' <param name="name"></param>
    ' <param name="description"></param>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.TextBoxBase.set_Text(System.String)")> Private Sub CreateFolder(ByVal name As String, ByVal description As String)
        Try
            Dim props As [Property]() = CreateDescriptionProperty(description)
            ' See Create a Folder in CatalogManager
            rs.CreateFolder(name, Me.Path, props)
        Catch ex As Exception
            Me.HandleGeneralException(ex)
        Finally
            ' Restore UI
            DisplayItems(Me.Path)
            m_addFolder.NameTextBox.Text = "New Folder"
            m_addFolder.Hide()
        End Try
    End Sub

    ' <summary>
    ' Helper method for sample purposes only. A more robust application would create a general
    ' purpose property method.
    ' </summary>
    ' <returns></returns>
    Private Shared Function CreateDescriptionProperty(ByVal description As String) As [Property]()
        Dim rsProperties(0) As [Property]
        Dim rsProperty As New [Property]()
        rsProperty.Name = "Description"
        rsProperty.Value = description
        rsProperties(0) = rsProperty

        Return rsProperties
    End Function

#End Region

#Region "How To: Delete multiple items from a Listview"

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.MessageBox.Show(System.String,System.String,System.Windows.Forms.MessageBoxButtons,System.Windows.Forms.MessageBoxIcon,System.Windows.Forms.MessageBoxDefaultButton,System.Windows.Forms.MessageBoxOptions)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> Private Sub DeleteItems()
        ' Only execute method if there are selected items
        If explorerListView.SelectedItems.Count > 0 Then
            Dim answer As DialogResult = MessageBox.Show( _
            "Are you sure you want to delete the selected item(s)?", _
            "Confirm Item Delete", MessageBoxButtons.YesNo, _
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, 0)

            If answer = Windows.Forms.DialogResult.Yes Then
                Dim selectedItems As New ArrayList()
                statusBar1.Text = "Deleting..."
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

                ' Create ArrayList to pass to DeleteItems
                For Each item As ListViewItem In explorerListView.SelectedItems
                    selectedItems.Add(item.Text)
                Next
                ' Call DeleteItems
                Try
                    Dim itemPath As String

                    

                    ' Call DeleteItem methods
                    For Each itemName As String In selectedItems
                        itemPath = GetCleanPath(Me.Path, itemName)
                        rs.DeleteItem(itemPath)
                    Next

                    
                Catch e As Exception
                    HandleGeneralException(e)
                Finally
                    System.Windows.Forms.Cursor.Current = Cursors.Default
                End Try
                ' Restore UI
                statusBar1.Text = "Delete completed."
                DisplayItems(Path)
            End If

        Else
            MessageBox.Show("Please select an item to delete.", _
                "Item Not Selected", MessageBoxButtons.OK, _
                MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0)
        End If
    End Sub

    Public Shared Function GetCleanPath(ByVal path As String, ByVal item As String) As String
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

#Region "How To: Edit a CatalogItem"

    Public Sub EditItem(ByVal name As String, ByVal description As String)
        Dim item, target As String
        Try
            ' Get a clean path to pass to EditItem
            item = GetCleanPath(Me.Path, m_lastShortName)
            target = GetCleanPath(Me.Path, name)

            Dim rsProperties As [Property]() = CreateDescriptionProperty(description)

            ' Rename Item and Create new Properties based on old properties
            

            ' Call methods to batch
            rs.MoveItem(item, target)
            rs.SetProperties(target, rsProperties)
            ' Test code. 
            ' this.SetProperties( newPath, null ); causes a failed method call which results in
            ' a rollback of the MoveItem changes
            ' Rollback transaction if either method fails
            

        Catch ex As Exception
            HandleGeneralException(ex)

        Finally
            ' Restore UI
            DisplayItems(Path)
        End Try
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> Public Sub EditItem(ByVal name As String)
        Dim item, target As String
        Try
            ' Get a clean path to pass to EditItem
            item = GetCleanPath(Me.Path, m_lastShortName)
            target = GetCleanPath(Me.Path, name)

            ' Rename Item and Create new Properties based on old properties
            

            ' Call methods to batch
            rs.MoveItem(item, target)
            

        Catch ex As Exception
            HandleGeneralException(ex)

        Finally
            ' Restore UI
            DisplayItems(Path)
        End Try
    End Sub
#End Region

#Region "How To: Copy and paste a report or resource"

#Region "Copy selected CatalogItems to an array"

    Private Sub copyMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles copyMenuItem.Click, copyCntxtMenuItem.Click
        m_copyList = New ArrayList()

        If explorerListView.SelectedItems.Count > 0 Then
            ' Add CatalogItems to ArrayList
            For Each clvi As CatalogListViewItem In explorerListView.SelectedItems
                m_copyList.Add(clvi)
            Next

            pasteMenuItem.Enabled = True
            pasteCntxtMenuItem.Enabled = True
        End If
    End Sub
#End Region

#Region "Paste CatalogItems within m_copyList array to new path"

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.MessageBox.Show(System.String,System.String,System.Windows.Forms.MessageBoxButtons,System.Windows.Forms.MessageBoxIcon,System.Windows.Forms.MessageBoxDefaultButton,System.Windows.Forms.MessageBoxOptions)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> Private Sub pasteMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles pasteMenuItem.Click, pasteCntxtMenuItem.Click
        Dim message As String
        Dim doSkip As Boolean = False
        ' Do not execute any further if m_copyList array is zero length
        If m_copyList.Count > 0 Then
            Me.ShowProgressDialog(True)
            Try
                ' Loop copy ArrayList
                For Each clvi As CatalogListViewItem In m_copyList
                    ' Test if Item is in Listview.Lists
                    doSkip = Me.IsInList(clvi)
                    If Not doSkip Then
                        ' Copy datasource first
                        If clvi.Item.TypeName = "DataSource" Then
                            Me.CopyDataSource(clvi.Item.Name, clvi.Item.Path, Path)

                            ' Test if we need to copy a report or resource
                        ElseIf clvi.Item.TypeName = "Report" Then
                            ' oldPath = clvi.Item.Path
                            ' Handling warning in CopyReport() method below
                            Me.CopyReport(clvi.Item.Name, clvi.Item.Path, Path)
                        ElseIf clvi.Item.TypeName = "Resource" Then
                            Me.CopyResource(clvi.Item.Name, clvi.Item.Path, Path)
                        Else
                            ' Show not supported message
                            message = "The " + clvi.Item.Name + " " + clvi.Item.TypeName + " could not be copied because Copy and Paste for this item is unsupported." + " " + ControlChars.Lf + "Copying of any remaining items will continue."
                            MessageBox.Show(message, _
                                "Copy and Paste For Item Not Supported", _
                                MessageBoxButtons.OK, _
                                MessageBoxIcon.Information, _
                                MessageBoxDefaultButton.Button1, 0)
                        End If

                        ' Refresh CopyProgress Form
                        copyProgress.Refresh()
                        copyProgress.ItemNameLabel.Text = "Item: " + clvi.Item.Name
                        copyProgress.ProgressBar.PerformStep()

                    Else
                        doSkip = False
                    End If
                Next

            Catch ex As Exception
                Me.HandleGeneralException(ex)

            Finally
                Me.ShowProgressDialog(False)
            End Try
        End If
    End Sub

    Public Sub CopyResource(ByVal name As String, ByVal oldPath As String, ByVal newPath As String)
        Dim resourceContents As Byte() = Nothing

        resourceContents = Me.rs.GetItemDefinition(oldPath)
        Me.rs.CreateCatalogItem("Resource", name, newPath, True, resourceContents, Nothing, Nothing)
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.MessageBox.Show(System.String,System.String,System.Windows.Forms.MessageBoxButtons,System.Windows.Forms.MessageBoxIcon)")> Private Function CopyReport(ByVal name As String, ByVal oldPath As String, ByVal newPath As String) As Warning()
        Dim warning As Warning()
        ' GetReportDefinition()
        ' CreateReport
        Dim reportDefinition As Byte() = Nothing

        reportDefinition = rs.GetItemDefinition(oldPath)
        rs.CreateCatalogItem("Report", name, newPath, True, reportDefinition, Nothing, warning)

        If Not (warning Is Nothing) Then
            MessageBox.Show(warning(0).Message, "DataSource Warning", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Dim itemPath As String = GetCleanPath(Me.Path, name)
            rs.DeleteItem(itemPath)
        End If

        Return warning
    End Function

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> Private Sub CopyDataSource(ByVal name As String, ByVal oldPath As String, ByVal newPath As String)
        Try
            Dim dsDefinition As DataSourceDefinition = Nothing

            dsDefinition = Me.rs.GetDataSourceContents(oldPath)
            rs.CreateDataSource(name, newPath, False, dsDefinition, Nothing)

        Catch ex As Exception
            Me.HandleGeneralException(ex)
        End Try
    End Sub

#End Region

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1818:DoNotConcatenateStringsInsideLoops")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.MessageBox.Show(System.String,System.String,System.Windows.Forms.MessageBoxButtons,System.Windows.Forms.MessageBoxIcon,System.Windows.Forms.MessageBoxDefaultButton,System.Windows.Forms.MessageBoxOptions)")> Private Function IsInList(ByVal clvi As CatalogListViewItem) As Boolean
        Dim skip As Boolean = False
        Dim message As String
        Dim answer As DialogResult

        For Each lvi As ListViewItem In explorerListView.Items
            If clvi.Item.Name = lvi.Text And clvi.Item.TypeName <> "Folder" Then
                message = clvi.Item.Name + " already exists."
                message += " Would you like to create a copy?"
                answer = MessageBox.Show(message, "Item Already Exists", _
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
                    MessageBoxDefaultButton.Button2, 0)

                If answer = Windows.Forms.DialogResult.Yes Then
                    'Note: This sample code does not account for existing "Copy of" CatalogItems
                    ' Test for "Copy of ..." CatalogItems within a production application
                    clvi.Item.Name = "Copy of " + clvi.Item.Name
                Else
                    skip = True
                End If
            End If
        Next

        Return skip
    End Function

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> Private Sub ShowProgressDialog(ByVal visible As Boolean)
        If visible Then
            statusBar1.Text = "Copying..."

            copyProgress = New CopyProgress()
            copyProgress.ProgressBar.Minimum = 1
            copyProgress.ProgressBar.Maximum = m_copyList.Count
            copyProgress.ProgressBar.Value = 1
            copyProgress.ProgressBar.Step = 1

            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

            copyProgress.Show()
            copyProgress.Refresh()

            System.Windows.Forms.Cursor.Current = Cursors.Default
        Else
            copyProgress.Hide()
            statusBar1.Text = "Copy completed."
            DisplayItems(Path)
        End If
    End Sub

#End Region

#Region "How To: Render a report in the ReportViewer WinForms control"

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> Public Sub RenderReport(ByVal report As CatalogItem)
        Dim currentPath As String

        ' If the path of the item is the root of the catalog
        ' set path equal to /,
        If Me.Path = "/" Then
            currentPath = Me.Path
        Else
            currentPath = Me.Path + "/"
        End If
        ' Build string for url access
        Dim url As String = serverPathTextbox.Text + "?" + currentPath + report.Name

        Try
            Dim viewer As New ReportViewer()
            viewer.Url = url
            viewer.Show()

        Catch ex As Exception
            Me.HandleGeneralException(ex)
        End Try
    End Sub
#End Region
#End Region

#Region "General Event Handlers"

    Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles toolBar1.ButtonClick
        Select Case toolBar1.Buttons.IndexOf(e.Button)
            Case 0 ' New Folder button
                m_addFolder.Show()
            Case 1 ' Edit button   
                Me.ShowEditForm()
            Case 2 ' Delete button
                Me.DeleteItems()
            Case 3 ' Separator
            Case 4 ' Up button
                Path = GetParentPath(Me.Path)
                DisplayItems(Path)
            Case 5 ' Refresh button
                DisplayItems(Path)
            Case Else
        End Select
    End Sub

    ' <summary>
    ' Fill Properties Listview
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Private Sub explorerListView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles explorerListView.Click
        Me.RefreshProperties(True)
    End Sub

    Private Sub renameCntxtMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles renameCntxtMenuItem.Click
        Me.ShowEditForm()
    End Sub

    Private Sub fileExitMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles fileExitMenuItem.Click
        Me.Close()
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.MessageBox.Show(System.String,System.String,System.Windows.Forms.MessageBoxButtons,System.Windows.Forms.MessageBoxIcon,System.Windows.Forms.MessageBoxDefaultButton,System.Windows.Forms.MessageBoxOptions)")> Private Sub explorerListView_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles explorerListView.DoubleClick
        Dim selItem As CatalogListViewItem = CType(explorerListView.SelectedItems(0), CatalogListViewItem)

        Select Case selItem.Item.TypeName
            ' If the item is a folder, display children
            Case "Folder"
                Path = GetFolderPath(selItem.Item)
                DisplayItems(Path)

                ' If the item is a report, launch the Win32 viewer form
            Case "Report"
                RenderReport(selItem.Item)

                ' If the item is a resource, do nothing
            Case "Resource"

                ' If the item is a linked report, render it to the viewer
            Case "LinkedReport"
                RenderReport(selItem.Item)

                ' If it is a data source, do nothing
            Case "DataSource"

            Case Else
                MessageBox.Show("No type information available for " _
                    & selItem.Text, "Unexplained Error", _
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation, _
                    MessageBoxDefaultButton.Button1, 0)
        End Select
    End Sub

    ' Used to support vitual paths of folders.
    Private Shared Function GetFolderPath(ByVal item As CatalogItem) As String
        Dim delimiter As String = "/"

        ' Split the path string into the folder names as parts
        Dim rx As New Regex(delimiter)
        Dim pathParts As String() = rx.Split(item.Path)

        ' Check to see if the item has a virtual path and return the
        ' virtual path for items that support it, like My Reports
        If Not (item.VirtualPath Is Nothing) AndAlso pathParts(1) <> "Users Folders" Then
            Return item.VirtualPath
        Else
            Return item.Path
        End If
    End Function

    Private Sub viewMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles viewIconsMenuItem.Click, viewListMenuItem.Click, viewDetailsMenuItem.Click, viewMenuItem.Click
        If sender Is viewIconsMenuItem Then
            SetView("ICONS")

        ElseIf sender Is viewListMenuItem Then
            SetView("LIST")

        ElseIf sender Is viewDetailsMenuItem Then
            SetView("DETAILS")
        End If
    End Sub

    Private Sub viewCntxtMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles iconsCntxtMenuItem.Click, listCntxtMenuItem.Click, detailsCntxtMenuItem.Click, viewCntxtMenuItem.Click
        If sender Is iconsCntxtMenuItem Then
            SetView("ICONS")
        ElseIf sender Is listCntxtMenuItem Then
            SetView("LIST")
        ElseIf sender Is detailsCntxtMenuItem Then
            SetView("DETAILS")
        End If
    End Sub

    Private Sub viewRefreshMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles refreshCntxtMenuItem.Click
        DisplayItems(Path)
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.MessageBox.Show(System.String,System.String,System.Windows.Forms.MessageBoxButtons,System.Windows.Forms.MessageBoxIcon,System.Windows.Forms.MessageBoxDefaultButton,System.Windows.Forms.MessageBoxOptions)")> Private Sub aboutMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles aboutMenuItem.Click
        MessageBox.Show("Version: " + Application.ProductVersion, _
            "About RSExplorer", MessageBoxButtons.OK, _
            MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, 0)
    End Sub

    Private Sub fileNewFolderMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles fileNewFolderMenuItem.Click
        m_addFolder.Show()
    End Sub

    Private Sub deleteMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles deleteMenuItem.Click, deleteCntxtMenuItem.Click
        If explorerListView.Focused Then
            Me.DeleteItems()
        End If
    End Sub

    Private Sub serverPathTextbox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles serverPathTextbox.KeyPress
        If e.KeyChar = "13" Then
            Me.ConnectToServer()
        End If
    End Sub

    Private Sub explorerListView_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyValue = 46 Then
            Me.DeleteItems()
        End If
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> Private Sub explorerListView_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Try
                defaultContextMenu.Show(explorerListView, New Point(e.X, e.Y))
            Catch
            End Try
        End If
    End Sub

    Private Sub propertiesntxtMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles propertiesCntxtMenuItem.Click
        Me.RefreshProperties(False)
    End Sub

    Private Sub linkLabel1_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles linkLabel1.LinkClicked
        Me.RefreshProperties(False)
    End Sub

    ' Method supports single click editing of item names in the list view
    Private Sub explorerListView_BeforeLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.LabelEditEventArgs) Handles explorerListView.BeforeLabelEdit
        Me.viewRefreshMenuItem.Enabled = False
        Me.refreshButton.Enabled = False
        Me.refreshCntxtMenuItem.Enabled = False

        ' Need to store the last known name of an item as a member variable
        m_lastShortName = explorerListView.Items(e.Item).Text
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> Private Sub explorerListView_AfterLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.LabelEditEventArgs) Handles explorerListView.AfterLabelEdit

        Me.viewRefreshMenuItem.Enabled = True
        Me.refreshButton.Enabled = True
        Me.refreshCntxtMenuItem.Enabled = True

        If Not (e.Label Is Nothing) AndAlso e.Label <> m_lastShortName Then
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

            Try
                ' Use the EditItem method, which ultimately calls the SOAP API
                Me.EditItem(e.Label)

            Catch ex As Exception
                e.CancelEdit = True
                HandleGeneralException(ex)

            Finally

                DisplayItems(Path)
                System.Windows.Forms.Cursor.Current = Cursors.Default
            End Try
        End If
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.MessageBox.Show(System.String,System.String,System.Windows.Forms.MessageBoxButtons,System.Windows.Forms.MessageBoxIcon,System.Windows.Forms.MessageBoxDefaultButton,System.Windows.Forms.MessageBoxOptions)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> _
    Private Sub menuItemImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuItemImport.Click
        Dim reportStream As Stream
        Dim reportDefinition() As System.Byte
        Dim parsedPath As String() = Nothing

        If openReportFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            ' Get Report Name and Report Extension
            Dim delimStr As String = "\"
            Dim delimiter As Char() = delimStr.ToCharArray()
            parsedPath = openReportFileDialog.FileName.Split(delimiter)
            Dim reportName As String = parsedPath((parsedPath.Length - 1))
            Dim reportExt As String = reportName.Substring(reportName.Length - 3, 3)
            reportName = reportName.Substring(0, reportName.Length - 4)

            ' Don't go any further if the stream is null or ext is not rdl.
            ' In a production application you would want to test for an rdl file type
            reportStream = openReportFileDialog.OpenFile()
            If Not (reportStream Is Nothing) And reportExt = "rdl" Then
                'Code to read the stream here.
                Try
                    System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
                    reportDefinition = New [Byte](CInt(reportStream.Length)) {}
                    reportStream.Read(reportDefinition, 0, CInt(Fix(reportStream.Length)))
                    reportStream.Close()

                    rs.CreateCatalogItem("Report", reportName, Me.Path, False, reportDefinition, Nothing, Nothing)
                Catch ex As Exception
                    MessageBox.Show("This report is associated with a shared data source. " _
                        + "Please use Report Manager to add the shared data source, before importing this report.", _
                        "Exception", MessageBoxButtons.OK, _
                        MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, 0)
                    statusBar1.Text = "Error."
                    System.Windows.Forms.Cursor.Current = Cursors.Default
                Finally
                    DisplayItems(Path)
                    System.Windows.Forms.Cursor.Current = Cursors.Default
                End Try
            Else
                MessageBox.Show("Not a report format or report file is empty", _
                "General Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, _
                MessageBoxDefaultButton.Button1, 0)
            End If
        End If
    End Sub

#End Region

#Region "General Methods"


    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.MessageBox.Show(System.String,System.String,System.Windows.Forms.MessageBoxButtons,System.Windows.Forms.MessageBoxIcon,System.Windows.Forms.MessageBoxDefaultButton,System.Windows.Forms.MessageBoxOptions)")> Private Sub ShowEditForm()
        If explorerListView.SelectedItems.Count > 0 Then
            ' For the Edit dialog, capture the current name of the item
            Dim description As String = String.Empty
            Dim item As CatalogItem = CType(explorerListView.SelectedItems(0), CatalogListViewItem).Item
            m_editItem.NameTextBox.Text = item.Name
            m_lastShortName = item.Name

            Try
                ' Retrieve the description of the item for the Edit dialog
                description = Me.GetProperty(item.Path, "Description")
                m_editItem.DescriptionTextBox.Text = description
                m_editItem.ShowDialog()
            Catch ex As Exception
                HandleGeneralException(ex)

            Finally
                DisplayItems(Path)
            End Try

        Else
            MessageBox.Show("Please select an item to edit.", _
                "Item Not Selected", MessageBoxButtons.OK, _
                MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0)
        End If
    End Sub

    Private Sub CntxtMenuItemsVisible(ByVal expr As Boolean)
        copyCntxtMenuItem.Visible = expr
        sepMenuItem13.Visible = expr
        deleteCntxtMenuItem.Visible = expr
        renameCntxtMenuItem.Visible = expr
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId:="clvi")> Public Sub SetContextMenu(ByVal clvi As CatalogListViewItem)
        ' This method can be used to expand
        ' the functionality of item specific context
        ' menus
        CntxtMenuItemsVisible(True)
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.MessageBox.Show(System.String,System.String,System.Windows.Forms.MessageBoxButtons,System.Windows.Forms.MessageBoxIcon,System.Windows.Forms.MessageBoxDefaultButton,System.Windows.Forms.MessageBoxOptions)")> Public Sub HandleGeneralException(ByVal ex As Exception)
        ' Note: This error handling is for sample purposes only
        '            *  A production application would require more robust exception handling
        '            
        MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, _
            MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 0)
        statusBar1.Text = "Error."
        System.Windows.Forms.Cursor.Current = Cursors.Default
    End Sub

    Public Sub EnableButtons(ByVal enable As Boolean)
        upButton.Enabled = enable
        newFolderButton.Enabled = enable
        deleteButton.Enabled = enable
        deleteMenuItem.Enabled = enable
        refreshButton.Enabled = enable
        viewRefreshMenuItem.Enabled = enable
        refreshCntxtMenuItem.Enabled = enable
        editButton.Enabled = enable
        fileNewFolderMenuItem.Enabled = enable

        'Context Menus
        refreshCntxtMenuItem.Enabled = enable
        copyCntxtMenuItem.Enabled = enable
        pasteCntxtMenuItem.Enabled = enable
        deleteCntxtMenuItem.Enabled = enable
        renameCntxtMenuItem.Enabled = enable
        propertiesCntxtMenuItem.Enabled = enable
        menuItemImport.Enabled = enable
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")> Public Sub SetView(ByVal viewType As String)

        viewIconsMenuItem.Checked = False
        viewListMenuItem.Checked = False
        viewDetailsMenuItem.Checked = False

        Select Case viewType
            Case "ICONS"
                viewIconsMenuItem.Checked = True
                explorerListView.View = View.LargeIcon

            Case "LIST"
                viewListMenuItem.Checked = True
                explorerListView.View = View.List

            Case "DETAILS"
                viewDetailsMenuItem.Checked = True
                explorerListView.View = View.Details

            Case Else
        End Select
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> Public Sub SetFormText()
        titleLabel.Text = "Catalog Explorer: "
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> Private Sub RefreshProperties(ByVal clearList As Boolean)
        If clearList Then
            propertiesListview.Items.Clear()
        Else
            Try
                Dim selItem As CatalogItem = CType(explorerListView.SelectedItems(0), CatalogListViewItem).Item
                Me.FillPropertiesListview(selItem)
            Catch
            End Try
        End If 'Do nothing
    End Sub

#End Region

#Region "Properties"

    Public Property Path() As String
        Get
            Return m_path
        End Get

        Set(ByVal value As String)
            m_path = value
        End Set
    End Property

#End Region

#Region "Utility Members"
    Public Shared Function GetParentPath(ByVal currentPath As String) As String
        Dim delimiter As String = "/"
        Dim rx As New Regex(delimiter)
        Dim childPath As String() = rx.Split(currentPath)

        Dim parentLength As Integer = childPath.Length - 2
        Dim parentPath(parentLength) As String

        For i As Integer = 0 To parentLength
            parentPath(i) = childPath(i)
        Next

        If parentPath.Length = 1 Then
            Return "/"
        Else
            Return String.Join("/", parentPath)
        End If
    End Function

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.ApplicationException.#ctor(System.String)")> Public Function GetProperty(ByVal path As String, ByVal name As String) As String
        Dim propValue As String = String.Empty
        Try
            Dim propArray As [Property]() = CreatePropertesArray(name)
            Dim properties As [Property]() = rs.GetProperties(path, propArray)
            For Each prop As [Property] In properties
                If prop.Name = name Then
                    propValue = prop.Value
                    Exit For
                End If
            Next
        Catch
            Throw New ApplicationException("Could not get property")
        End Try
        Return propValue
    End Function

    ' <summary>
    ' Create a Property Array 
    ' </summary>
    ' <param name="Name"></param>
    ' <returns></returns>
    Public Shared Function CreatePropertesArray(ByVal name As String) As [Property]()
        Dim rsProperty As New [Property]()
        rsProperty.Name = name
        Dim rsProperties(0) As [Property]
        rsProperties(0) = rsProperty

        Return rsProperties
    End Function
#End Region
End Class