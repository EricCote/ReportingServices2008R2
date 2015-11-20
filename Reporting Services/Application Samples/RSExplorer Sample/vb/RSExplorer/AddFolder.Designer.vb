Public Partial Class AddFolder
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.TextBoxBase.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me._nameTextBox = New System.Windows.Forms.TextBox
        Me.cancelButton = New System.Windows.Forms.Button
        Me.label3 = New System.Windows.Forms.Label
        Me.descriptionTextBox = New System.Windows.Forms.TextBox
        Me.label2 = New System.Windows.Forms.Label
        Me.label1 = New System.Windows.Forms.Label
        Me.addButton = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        '_nameTextBox
        '
        Me._nameTextBox.Location = New System.Drawing.Point(77, 37)
        Me._nameTextBox.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me._nameTextBox.Name = "_nameTextBox"
        Me._nameTextBox.Size = New System.Drawing.Size(224, 20)
        Me._nameTextBox.TabIndex = 0
        Me._nameTextBox.Text = "New Folder"
        '
        'cancelButton
        '
        Me.cancelButton.Location = New System.Drawing.Point(229, 149)
        Me.cancelButton.Name = "cancelButton"
        Me.cancelButton.TabIndex = 3
        Me.cancelButton.Text = "Cancel"
        '
        'label3
        '
        Me.label3.Location = New System.Drawing.Point(13, 69)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(64, 23)
        Me.label3.TabIndex = 15
        Me.label3.Text = "Description"
        '
        'descriptionTextBox
        '
        Me.descriptionTextBox.Location = New System.Drawing.Point(77, 69)
        Me.descriptionTextBox.MaxLength = 512
        Me.descriptionTextBox.Multiline = True
        Me.descriptionTextBox.Name = "descriptionTextBox"
        Me.descriptionTextBox.Size = New System.Drawing.Size(224, 72)
        Me.descriptionTextBox.TabIndex = 1
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(13, 37)
        Me.label2.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(40, 23)
        Me.label2.TabIndex = 14
        Me.label2.Text = "Name"
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(13, 13)
        Me.label1.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(272, 23)
        Me.label1.TabIndex = 12
        Me.label1.Text = "Please enter a folder name and optional description."
        '
        'addButton
        '
        Me.addButton.Location = New System.Drawing.Point(141, 149)
        Me.addButton.Name = "addButton"
        Me.addButton.TabIndex = 2
        Me.addButton.Text = "Add"
        '
        'AddFolder
        '
        Me.ClientSize = New System.Drawing.Size(316, 185)
        Me.Controls.Add(Me._nameTextBox)
        Me.Controls.Add(Me.cancelButton)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.descriptionTextBox)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.addButton)
        Me.Name = "AddFolder"
        Me.Text = "Add Folder"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents _nameTextBox As System.Windows.Forms.TextBox
    Friend Shadows WithEvents cancelButton As System.Windows.Forms.Button
    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents descriptionTextBox As System.Windows.Forms.TextBox
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents addButton As System.Windows.Forms.Button
End Class
