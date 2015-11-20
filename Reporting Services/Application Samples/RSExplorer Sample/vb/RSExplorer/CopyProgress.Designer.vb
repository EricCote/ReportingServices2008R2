Public Partial Class CopyProgress
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
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me._itemNameLabel = New System.Windows.Forms.Label
        Me.progressBar1 = New System.Windows.Forms.ProgressBar
        Me.SuspendLayout()
        '
        '_itemNameLabel
        '
        Me._itemNameLabel.Location = New System.Drawing.Point(13, 13)
        Me._itemNameLabel.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me._itemNameLabel.Name = "_itemNameLabel"
        Me._itemNameLabel.Size = New System.Drawing.Size(272, 23)
        Me._itemNameLabel.TabIndex = 3
        Me._itemNameLabel.Text = "Item:"
        '
        'progressBar1
        '
        Me.progressBar1.Location = New System.Drawing.Point(13, 37)
        Me.progressBar1.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.progressBar1.Name = "progressBar1"
        Me.progressBar1.Size = New System.Drawing.Size(272, 23)
        Me.progressBar1.TabIndex = 2
        '
        'CopyProgress
        '
        Me.ClientSize = New System.Drawing.Size(298, 74)
        Me.Controls.Add(Me._itemNameLabel)
        Me.Controls.Add(Me.progressBar1)
        Me.Name = "CopyProgress"
        Me.Text = "CopyProgress"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents _itemNameLabel As System.Windows.Forms.Label
    Friend WithEvents progressBar1 As System.Windows.Forms.ProgressBar
End Class
