'-----------------------------------------------------------------------
'  This file is part of the Microsoft Code Samples.
' 
'  Copyright (C) Microsoft Corporation.  All rights reserved.
' 
'  This source code is intended only as a supplement to Microsoft
'  Development Tools and/or on-line documentation.  See these other
'  materials for detailed information regarding Microsoft code samples.
' 
'  THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
'  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
'  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
'  PARTICULAR PURPOSE.
'-----------------------------------------------------------------------

Public Class AddFolder

    ' OnAdd Event for window integration
    ' Note: In a production application use sender/EventArgs signature format
    Delegate Sub AddEventHandler(ByVal name As String, ByVal desc As String)
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")> Public Event OnAdd As AddEventHandler

    Public Property NameTextBox() As TextBox
        Get
            Return _nameTextBox
        End Get
        Set(ByVal Value As TextBox)
            Me._nameTextBox = Value
        End Set
    End Property

    '/ <summary>
    '/ Method to handle when a user clicks the Add button in dialog
    '/ </summary>
    '/ <param name="sender"></param>
    '/ <param name="e"></param>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.TextBoxBase.set_Text(System.String)")> Private Sub addButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles addButton.Click
        RaiseEvent OnAdd(_nameTextBox.Text, descriptionTextBox.Text)
        'Restore
        Me._nameTextBox.Text = "New Folder"
        Me.descriptionTextBox.Text = String.Empty
        Me.Hide()
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.TextBoxBase.set_Text(System.String)")> Private Sub AddFolder_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        ' When the dialog box is closing set the default values.
        ' The dialog remains in memory for the next use
        e.Cancel = True
        Me._nameTextBox.Text = "New Folder"
        Me.descriptionTextBox.Text = String.Empty
        Me.Hide()
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.TextBoxBase.set_Text(System.String)")> Private Sub cancelButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cancelButton.Click
        ' When the dialog box is closed via the cancel button, set the default values.
        ' The dialog remains in memory for the next use
        Me._nameTextBox.Text = "New Folder"
        Me.descriptionTextBox.Text = String.Empty
        Me.Hide()
    End Sub
End Class