Public Partial Class EditItem
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
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.label3 = New System.Windows.Forms.Label
        Me.DescriptionTextBox = New System.Windows.Forms.TextBox
        Me.NameTextBox = New System.Windows.Forms.TextBox
        Me.label2 = New System.Windows.Forms.Label
        Me.cancelButton = New System.Windows.Forms.Button
        Me.label1 = New System.Windows.Forms.Label
        Me.updateButton = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'label3
        '
        Me.label3.Location = New System.Drawing.Point(13, 69)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(64, 23)
        Me.label3.TabIndex = 21
        Me.label3.Text = "Description"
        '
        'DescriptionTextBox
        '
        Me.DescriptionTextBox.Location = New System.Drawing.Point(77, 69)
        Me.DescriptionTextBox.MaxLength = 512
        Me.DescriptionTextBox.Multiline = True
        Me.DescriptionTextBox.Name = "DescriptionTextBox"
        Me.DescriptionTextBox.Size = New System.Drawing.Size(224, 72)
        Me.DescriptionTextBox.TabIndex = 16
        '
        'NameTextBox
        '
        Me.NameTextBox.Location = New System.Drawing.Point(77, 37)
        Me.NameTextBox.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.NameTextBox.MaxLength = 260
        Me.NameTextBox.Name = "NameTextBox"
        Me.NameTextBox.Size = New System.Drawing.Size(224, 20)
        Me.NameTextBox.TabIndex = 15
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(13, 37)
        Me.label2.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(40, 23)
        Me.label2.TabIndex = 20
        Me.label2.Text = "Name"
        '
        'cancelButton
        '
        Me.cancelButton.Location = New System.Drawing.Point(221, 149)
        Me.cancelButton.Name = "cancelButton"
        Me.cancelButton.Size = New System.Drawing.Size(75, 23)
        Me.cancelButton.TabIndex = 17
        Me.cancelButton.Text = "Cancel"
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(13, 13)
        Me.label1.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(272, 23)
        Me.label1.TabIndex = 19
        Me.label1.Text = "Please enter a folder name and optional description."
        '
        'updateButton
        '
        Me.updateButton.Location = New System.Drawing.Point(141, 149)
        Me.updateButton.Name = "updateButton"
        Me.updateButton.Size = New System.Drawing.Size(75, 23)
        Me.updateButton.TabIndex = 18
        Me.updateButton.Text = "Update"
        '
        'EditItem
        '
        Me.ClientSize = New System.Drawing.Size(314, 185)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.DescriptionTextBox)
        Me.Controls.Add(Me.NameTextBox)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.cancelButton)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.updateButton)
        Me.Name = "EditItem"
        Me.Text = "EditItem"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents DescriptionTextBox As System.Windows.Forms.TextBox
    Public WithEvents NameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend Shadows WithEvents cancelButton As System.Windows.Forms.Button
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents updateButton As System.Windows.Forms.Button
End Class
