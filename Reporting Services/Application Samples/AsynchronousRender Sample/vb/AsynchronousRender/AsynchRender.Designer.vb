Partial Public Class AsynchRender
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
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Mobility", "CA1601:DoNotUseTimersThatPreventPowerStateChanges")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.TextBoxBase.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AsynchRender))
        Me.connectButton = New System.Windows.Forms.Button
        Me.statusTimer = New System.Windows.Forms.Timer(Me.components)
        Me.catalogTreeView = New System.Windows.Forms.TreeView
        Me.treeNodeImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.statusTextBox = New System.Windows.Forms.TextBox
        Me.statusLabel = New System.Windows.Forms.Label
        Me.infoLabel = New System.Windows.Forms.Label
        Me.exitButton = New System.Windows.Forms.Button
        Me.saveReportDialog = New System.Windows.Forms.SaveFileDialog
        Me.renderButton = New System.Windows.Forms.Button
        Me.label1 = New System.Windows.Forms.Label
        Me.serverTextBox = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'connectButton
        '
        Me.connectButton.Location = New System.Drawing.Point(305, 59)
        Me.connectButton.Name = "connectButton"
        Me.connectButton.Size = New System.Drawing.Size(75, 23)
        Me.connectButton.TabIndex = 26
        Me.connectButton.Text = "Connect"
        '
        'statusTimer
        '
        Me.statusTimer.Interval = 500
        '
        'catalogTreeView
        '
        Me.catalogTreeView.ImageIndex = 0
        Me.catalogTreeView.ImageList = Me.treeNodeImageList
        Me.catalogTreeView.Location = New System.Drawing.Point(12, 88)
        Me.catalogTreeView.Margin = New System.Windows.Forms.Padding(3, 3, 1, 3)
        Me.catalogTreeView.Name = "catalogTreeView"
        Me.catalogTreeView.SelectedImageIndex = 0
        Me.catalogTreeView.Size = New System.Drawing.Size(288, 134)
        Me.catalogTreeView.TabIndex = 25
        '
        'treeNodeImageList
        '
        Me.treeNodeImageList.ImageStream = CType(resources.GetObject("treeNodeImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.treeNodeImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.treeNodeImageList.Images.SetKeyName(0, "")
        Me.treeNodeImageList.Images.SetKeyName(1, "")
        Me.treeNodeImageList.Images.SetKeyName(2, "")
        Me.treeNodeImageList.Images.SetKeyName(3, "")
        '
        'statusTextBox
        '
        Me.statusTextBox.BackColor = System.Drawing.SystemColors.Control
        Me.statusTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.statusTextBox.Location = New System.Drawing.Point(15, 249)
        Me.statusTextBox.Margin = New System.Windows.Forms.Padding(3, 3, 1, 3)
        Me.statusTextBox.Multiline = True
        Me.statusTextBox.Name = "statusTextBox"
        Me.statusTextBox.Size = New System.Drawing.Size(209, 20)
        Me.statusTextBox.TabIndex = 24
        Me.statusTextBox.Text = "Ready."
        '
        'statusLabel
        '
        Me.statusLabel.Location = New System.Drawing.Point(12, 230)
        Me.statusLabel.Name = "statusLabel"
        Me.statusLabel.Size = New System.Drawing.Size(100, 16)
        Me.statusLabel.TabIndex = 23
        Me.statusLabel.Text = "Status:"
        '
        'infoLabel
        '
        Me.infoLabel.Location = New System.Drawing.Point(13, 9)
        Me.infoLabel.Name = "infoLabel"
        Me.infoLabel.Size = New System.Drawing.Size(287, 32)
        Me.infoLabel.TabIndex = 22
        Me.infoLabel.Text = "To render a report asynchronously, click Connect, choose a report, and then click" & _
            " Render."
        '
        'exitButton
        '
        Me.exitButton.Location = New System.Drawing.Point(305, 244)
        Me.exitButton.Margin = New System.Windows.Forms.Padding(2, 3, 3, 3)
        Me.exitButton.Name = "exitButton"
        Me.exitButton.Size = New System.Drawing.Size(75, 23)
        Me.exitButton.TabIndex = 21
        Me.exitButton.Text = "Exit"
        '
        'renderButton
        '
        Me.renderButton.Enabled = False
        Me.renderButton.Location = New System.Drawing.Point(305, 88)
        Me.renderButton.Margin = New System.Windows.Forms.Padding(2, 3, 3, 3)
        Me.renderButton.Name = "renderButton"
        Me.renderButton.Size = New System.Drawing.Size(75, 24)
        Me.renderButton.TabIndex = 20
        Me.renderButton.Text = "Render"
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.label1.Location = New System.Drawing.Point(12, 43)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(38, 13)
        Me.label1.TabIndex = 28
        Me.label1.Text = "Server"
        '
        'serverTextBox
        '
        Me.serverTextBox.Location = New System.Drawing.Point(16, 62)
        Me.serverTextBox.Name = "serverTextBox"
        Me.serverTextBox.Size = New System.Drawing.Size(284, 20)
        Me.serverTextBox.TabIndex = 29
        '
        'AsynchRender
        '
        Me.ClientSize = New System.Drawing.Size(388, 277)
        Me.Controls.Add(Me.serverTextBox)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.statusLabel)
        Me.Controls.Add(Me.infoLabel)
        Me.Controls.Add(Me.exitButton)
        Me.Controls.Add(Me.renderButton)
        Me.Controls.Add(Me.connectButton)
        Me.Controls.Add(Me.catalogTreeView)
        Me.Controls.Add(Me.statusTextBox)
        Me.Name = "AsynchRender"
        Me.Text = "AsynchronousRender Sample"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents connectButton As System.Windows.Forms.Button
    Friend WithEvents statusTimer As System.Windows.Forms.Timer
    Friend WithEvents catalogTreeView As System.Windows.Forms.TreeView
    Friend WithEvents treeNodeImageList As System.Windows.Forms.ImageList
    Friend WithEvents statusTextBox As System.Windows.Forms.TextBox
    Friend WithEvents statusLabel As System.Windows.Forms.Label
    Friend WithEvents infoLabel As System.Windows.Forms.Label
    Friend WithEvents exitButton As System.Windows.Forms.Button
    Friend WithEvents saveReportDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents renderButton As System.Windows.Forms.Button
    Private WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents serverTextBox As System.Windows.Forms.TextBox

End Class

