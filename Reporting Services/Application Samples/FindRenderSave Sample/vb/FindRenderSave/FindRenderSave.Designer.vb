<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces")> Partial Public Class FindRenderSave
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
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.closeButton = New System.Windows.Forms.Button
        Me.renderAsLabel = New System.Windows.Forms.Label
        Me.itemsFoundLabel = New System.Windows.Forms.Label
        Me.introLabel = New System.Windows.Forms.Label
        Me.searchStringLabel = New System.Windows.Forms.Label
        Me.pathTextBox = New System.Windows.Forms.TextBox
        Me.searchTextBox = New System.Windows.Forms.TextBox
        Me.descriptionTextBox = New System.Windows.Forms.TextBox
        Me.pathLabel = New System.Windows.Forms.Label
        Me.formatComboBox = New System.Windows.Forms.ComboBox
        Me.searchButton = New System.Windows.Forms.Button
        Me.conditionComboBox = New System.Windows.Forms.ComboBox
        Me.searchByLabel = New System.Windows.Forms.Label
        Me.saveReportButton = New System.Windows.Forms.Button
        Me.reportListView = New System.Windows.Forms.ListBox
        Me.descriptionLabel = New System.Windows.Forms.Label
        Me.saveReportDialog = New System.Windows.Forms.SaveFileDialog
        Me.SuspendLayout()
        '
        'closeButton
        '
        Me.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.closeButton.Location = New System.Drawing.Point(389, 309)
        Me.closeButton.Name = "closeButton"
        Me.closeButton.Size = New System.Drawing.Size(72, 24)
        Me.closeButton.TabIndex = 50
        Me.closeButton.Text = "Close"
        '
        'renderAsLabel
        '
        Me.renderAsLabel.Location = New System.Drawing.Point(13, 293)
        Me.renderAsLabel.Name = "renderAsLabel"
        Me.renderAsLabel.Size = New System.Drawing.Size(64, 23)
        Me.renderAsLabel.TabIndex = 49
        Me.renderAsLabel.Text = "Render as:"
        '
        'itemsFoundLabel
        '
        Me.itemsFoundLabel.Location = New System.Drawing.Point(13, 117)
        Me.itemsFoundLabel.Name = "itemsFoundLabel"
        Me.itemsFoundLabel.Size = New System.Drawing.Size(80, 16)
        Me.itemsFoundLabel.TabIndex = 48
        Me.itemsFoundLabel.Text = "Items found:"
        '
        'introLabel
        '
        Me.introLabel.Location = New System.Drawing.Point(13, 13)
        Me.introLabel.Name = "introLabel"
        Me.introLabel.Size = New System.Drawing.Size(424, 23)
        Me.introLabel.TabIndex = 47
        Me.introLabel.Text = "Specify a search string for a report that you would like to render and click Sear" & _
            "ch."
        '
        'searchStringLabel
        '
        Me.searchStringLabel.Location = New System.Drawing.Point(13, 77)
        Me.searchStringLabel.Name = "searchStringLabel"
        Me.searchStringLabel.Size = New System.Drawing.Size(80, 23)
        Me.searchStringLabel.TabIndex = 46
        Me.searchStringLabel.Text = "Search string:"
        '
        'pathTextBox
        '
        Me.pathTextBox.BackColor = System.Drawing.SystemColors.Control
        Me.pathTextBox.Location = New System.Drawing.Point(237, 261)
        Me.pathTextBox.Name = "pathTextBox"
        Me.pathTextBox.ReadOnly = True
        Me.pathTextBox.Size = New System.Drawing.Size(224, 20)
        Me.pathTextBox.TabIndex = 45
        '
        'searchTextBox
        '
        Me.searchTextBox.Location = New System.Drawing.Point(101, 77)
        Me.searchTextBox.Name = "searchTextBox"
        Me.searchTextBox.Size = New System.Drawing.Size(200, 20)
        Me.searchTextBox.TabIndex = 38
        '
        'descriptionTextBox
        '
        Me.descriptionTextBox.AcceptsReturn = True
        Me.descriptionTextBox.BackColor = System.Drawing.SystemColors.Control
        Me.descriptionTextBox.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.descriptionTextBox.Location = New System.Drawing.Point(237, 133)
        Me.descriptionTextBox.Multiline = True
        Me.descriptionTextBox.Name = "descriptionTextBox"
        Me.descriptionTextBox.ReadOnly = True
        Me.descriptionTextBox.Size = New System.Drawing.Size(224, 104)
        Me.descriptionTextBox.TabIndex = 43
        '
        'pathLabel
        '
        Me.pathLabel.Location = New System.Drawing.Point(237, 245)
        Me.pathLabel.Name = "pathLabel"
        Me.pathLabel.Size = New System.Drawing.Size(72, 16)
        Me.pathLabel.TabIndex = 44
        Me.pathLabel.Text = "Path:"
        '
        'formatComboBox
        '
        Me.formatComboBox.FormattingEnabled = True
        Me.formatComboBox.Items.AddRange(New Object() {"MHTML", "EXCEL", "IMAGE", "PDF"})
        Me.formatComboBox.Location = New System.Drawing.Point(85, 293)
        Me.formatComboBox.Name = "formatComboBox"
        Me.formatComboBox.Size = New System.Drawing.Size(88, 21)
        Me.formatComboBox.TabIndex = 42
        Me.formatComboBox.Text = "MHTML"
        '
        'searchButton
        '
        Me.searchButton.Location = New System.Drawing.Point(309, 77)
        Me.searchButton.Name = "searchButton"
        Me.searchButton.Size = New System.Drawing.Size(75, 23)
        Me.searchButton.TabIndex = 41
        Me.searchButton.Text = "Search"
        '
        'conditionComboBox
        '
        Me.conditionComboBox.FormattingEnabled = True
        Me.conditionComboBox.Items.AddRange(New Object() {"Name", "Description", "Name or Description"})
        Me.conditionComboBox.Location = New System.Drawing.Point(101, 45)
        Me.conditionComboBox.Name = "conditionComboBox"
        Me.conditionComboBox.Size = New System.Drawing.Size(136, 21)
        Me.conditionComboBox.TabIndex = 40
        Me.conditionComboBox.Text = "Name"
        '
        'searchByLabel
        '
        Me.searchByLabel.Location = New System.Drawing.Point(13, 45)
        Me.searchByLabel.Name = "searchByLabel"
        Me.searchByLabel.Size = New System.Drawing.Size(80, 23)
        Me.searchByLabel.TabIndex = 39
        Me.searchByLabel.Text = "Search by:"
        '
        'saveReportButton
        '
        Me.saveReportButton.Enabled = False
        Me.saveReportButton.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.saveReportButton.Location = New System.Drawing.Point(278, 309)
        Me.saveReportButton.Name = "saveReportButton"
        Me.saveReportButton.Size = New System.Drawing.Size(103, 24)
        Me.saveReportButton.TabIndex = 37
        Me.saveReportButton.Text = "Save Report"
        '
        'reportListView
        '
        Me.reportListView.DisplayMember = "Text"
        Me.reportListView.FormattingEnabled = True
        Me.reportListView.Location = New System.Drawing.Point(13, 133)
        Me.reportListView.Name = "reportListView"
        Me.reportListView.Size = New System.Drawing.Size(216, 147)
        Me.reportListView.TabIndex = 36
        '
        'descriptionLabel
        '
        Me.descriptionLabel.Location = New System.Drawing.Point(237, 117)
        Me.descriptionLabel.Name = "descriptionLabel"
        Me.descriptionLabel.Size = New System.Drawing.Size(72, 16)
        Me.descriptionLabel.TabIndex = 35
        Me.descriptionLabel.Text = "Description:"
        '
        'FindRenderSave
        '
        Me.ClientSize = New System.Drawing.Size(468, 340)
        Me.Controls.Add(Me.closeButton)
        Me.Controls.Add(Me.renderAsLabel)
        Me.Controls.Add(Me.itemsFoundLabel)
        Me.Controls.Add(Me.introLabel)
        Me.Controls.Add(Me.searchStringLabel)
        Me.Controls.Add(Me.pathTextBox)
        Me.Controls.Add(Me.searchTextBox)
        Me.Controls.Add(Me.descriptionTextBox)
        Me.Controls.Add(Me.pathLabel)
        Me.Controls.Add(Me.formatComboBox)
        Me.Controls.Add(Me.searchButton)
        Me.Controls.Add(Me.conditionComboBox)
        Me.Controls.Add(Me.searchByLabel)
        Me.Controls.Add(Me.saveReportButton)
        Me.Controls.Add(Me.reportListView)
        Me.Controls.Add(Me.descriptionLabel)
        Me.Name = "FindRenderSave"
        Me.Text = "FindRenderSave Sample Application"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents closeButton As System.Windows.Forms.Button
    Friend WithEvents renderAsLabel As System.Windows.Forms.Label
    Friend WithEvents itemsFoundLabel As System.Windows.Forms.Label
    Friend WithEvents introLabel As System.Windows.Forms.Label
    Friend WithEvents searchStringLabel As System.Windows.Forms.Label
    Friend WithEvents pathTextBox As System.Windows.Forms.TextBox
    Friend WithEvents searchTextBox As System.Windows.Forms.TextBox
    Friend WithEvents descriptionTextBox As System.Windows.Forms.TextBox
    Friend WithEvents pathLabel As System.Windows.Forms.Label
    Friend WithEvents formatComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents searchButton As System.Windows.Forms.Button
    Friend WithEvents conditionComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents searchByLabel As System.Windows.Forms.Label
    Friend WithEvents saveReportButton As System.Windows.Forms.Button
    Friend WithEvents reportListView As System.Windows.Forms.ListBox
    Friend WithEvents descriptionLabel As System.Windows.Forms.Label
    Friend WithEvents saveReportDialog As System.Windows.Forms.SaveFileDialog

End Class
