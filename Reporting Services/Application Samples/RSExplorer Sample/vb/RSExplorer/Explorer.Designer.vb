Public Partial Class Explorer
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        '
        ' Required for Windows Form Designer support
        '
        InitializeComponent()

        ' Event handlers for cross form communication
        AddHandler m_addFolder.OnAdd, AddressOf Me.CreateFolder
        AddHandler m_editItem.OnEdit, AddressOf Me.EditItem
        Try
            ' Populate ComboBox text if there is a url on command-line
            serverPathTextbox.Text = Environment.GetCommandLineArgs()(1).ToString()
        Catch ex As Exception
        End Try
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
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.TextBoxBase.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.MenuItem.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Label.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId:="System.Windows.Forms.ColumnHeader.set_Text(System.String)")> <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Explorer))
        Me.deleteCntxtMenuItem = New System.Windows.Forms.MenuItem()
        Me.renameCntxtMenuItem = New System.Windows.Forms.MenuItem()
        Me.pasteCntxtMenuItem = New System.Windows.Forms.MenuItem()
        Me.sepMenuItem13 = New System.Windows.Forms.MenuItem()
        Me.menuItem5 = New System.Windows.Forms.MenuItem()
        Me.nameColumnHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.sizeColumnHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.propertiesCntxtMenuItem = New System.Windows.Forms.MenuItem()
        Me.explorerListView = New System.Windows.Forms.ListView()
        Me.typeColumnHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.modifiedColumnHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.largeImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.smallImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.iconsCntxtMenuItem = New System.Windows.Forms.MenuItem()
        Me.listCntxtMenuItem = New System.Windows.Forms.MenuItem()
        Me.defaultContextMenu = New System.Windows.Forms.ContextMenu()
        Me.viewCntxtMenuItem = New System.Windows.Forms.MenuItem()
        Me.detailsCntxtMenuItem = New System.Windows.Forms.MenuItem()
        Me.menuItem8 = New System.Windows.Forms.MenuItem()
        Me.refreshCntxtMenuItem = New System.Windows.Forms.MenuItem()
        Me.sepMenuItem12 = New System.Windows.Forms.MenuItem()
        Me.copyCntxtMenuItem = New System.Windows.Forms.MenuItem()
        Me.Value = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.columnValue = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.columnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.columnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.linkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.openReportFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.columnName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.propertiesListview = New System.Windows.Forms.ListView()
        Me.titleLabel = New System.Windows.Forms.Label()
        Me.menuItem2 = New System.Windows.Forms.MenuItem()
        Me.pasteMenuItem = New System.Windows.Forms.MenuItem()
        Me.menuItem10 = New System.Windows.Forms.MenuItem()
        Me.editMenuItem = New System.Windows.Forms.MenuItem()
        Me.copyMenuItem = New System.Windows.Forms.MenuItem()
        Me.deleteMenuItem = New System.Windows.Forms.MenuItem()
        Me.viewListMenuItem = New System.Windows.Forms.MenuItem()
        Me.viewDetailsMenuItem = New System.Windows.Forms.MenuItem()
        Me.viewMenuItem = New System.Windows.Forms.MenuItem()
        Me.viewIconsMenuItem = New System.Windows.Forms.MenuItem()
        Me.separatorMenuItem = New System.Windows.Forms.MenuItem()
        Me.viewRefreshMenuItem = New System.Windows.Forms.MenuItem()
        Me.menuItem1 = New System.Windows.Forms.MenuItem()
        Me.fileNewFolderMenuItem = New System.Windows.Forms.MenuItem()
        Me.statusBar1 = New System.Windows.Forms.StatusBar()
        Me.fileMenuItem = New System.Windows.Forms.MenuItem()
        Me.menuItemImport = New System.Windows.Forms.MenuItem()
        Me.menuItem4 = New System.Windows.Forms.MenuItem()
        Me.fileExitMenuItem = New System.Windows.Forms.MenuItem()
        Me.mainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.helpMenuItem = New System.Windows.Forms.MenuItem()
        Me.aboutMenuItem = New System.Windows.Forms.MenuItem()
        Me.toolbarImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.serverPathTextbox = New System.Windows.Forms.TextBox()
        Me.goButton = New System.Windows.Forms.Button()
        Me.label1 = New System.Windows.Forms.Label()
        Me.upButton = New System.Windows.Forms.ToolBarButton()
        Me.refreshButton = New System.Windows.Forms.ToolBarButton()
        Me.iconCntxtMenuItem = New System.Windows.Forms.MenuItem()
        Me.menuItem9 = New System.Windows.Forms.MenuItem()
        Me.menuItem3 = New System.Windows.Forms.MenuItem()
        Me.panel2 = New System.Windows.Forms.Panel()
        Me.toolBar1 = New System.Windows.Forms.ToolBar()
        Me.newFolderButton = New System.Windows.Forms.ToolBarButton()
        Me.editButton = New System.Windows.Forms.ToolBarButton()
        Me.deleteButton = New System.Windows.Forms.ToolBarButton()
        Me.separatorButton = New System.Windows.Forms.ToolBarButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.panel1.SuspendLayout()
        Me.panel2.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.SuspendLayout()
        '
        'deleteCntxtMenuItem
        '
        Me.deleteCntxtMenuItem.Enabled = False
        Me.deleteCntxtMenuItem.Index = 7
        Me.deleteCntxtMenuItem.Text = "Delete"
        '
        'renameCntxtMenuItem
        '
        Me.renameCntxtMenuItem.Enabled = False
        Me.renameCntxtMenuItem.Index = 8
        Me.renameCntxtMenuItem.Text = "Rename"
        '
        'pasteCntxtMenuItem
        '
        Me.pasteCntxtMenuItem.Enabled = False
        Me.pasteCntxtMenuItem.Index = 5
        Me.pasteCntxtMenuItem.Text = "Paste"
        '
        'sepMenuItem13
        '
        Me.sepMenuItem13.Index = 6
        Me.sepMenuItem13.Text = "-"
        Me.sepMenuItem13.Visible = False
        '
        'menuItem5
        '
        Me.menuItem5.Index = 9
        Me.menuItem5.Text = "-"
        '
        'nameColumnHeader
        '
        Me.nameColumnHeader.Name = "nameColumnHeader"
        Me.nameColumnHeader.Text = "Name"
        Me.nameColumnHeader.Width = 125
        '
        'sizeColumnHeader
        '
        Me.sizeColumnHeader.Name = "sizeColumnHeader"
        Me.sizeColumnHeader.Text = "Size"
        Me.sizeColumnHeader.Width = 100
        '
        'propertiesCntxtMenuItem
        '
        Me.propertiesCntxtMenuItem.Enabled = False
        Me.propertiesCntxtMenuItem.Index = 10
        Me.propertiesCntxtMenuItem.Text = "Properties"
        '
        'explorerListView
        '
        Me.explorerListView.AllowDrop = True
        Me.explorerListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.nameColumnHeader, Me.sizeColumnHeader, Me.typeColumnHeader, Me.modifiedColumnHeader})
        Me.explorerListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.explorerListView.HideSelection = False
        Me.explorerListView.LabelEdit = True
        Me.explorerListView.LargeImageList = Me.largeImageList
        Me.explorerListView.Location = New System.Drawing.Point(0, 0)
        Me.explorerListView.Name = "explorerListView"
        Me.explorerListView.Size = New System.Drawing.Size(526, 501)
        Me.explorerListView.SmallImageList = Me.smallImageList
        Me.explorerListView.TabIndex = 4
        Me.explorerListView.UseCompatibleStateImageBehavior = False
        Me.explorerListView.View = System.Windows.Forms.View.Details
        '
        'typeColumnHeader
        '
        Me.typeColumnHeader.Name = "typeColumnHeader"
        Me.typeColumnHeader.Text = "Type"
        Me.typeColumnHeader.Width = 100
        '
        'modifiedColumnHeader
        '
        Me.modifiedColumnHeader.Name = "modifiedColumnHeader"
        Me.modifiedColumnHeader.Text = "Modified Date"
        Me.modifiedColumnHeader.Width = 125
        '
        'largeImageList
        '
        Me.largeImageList.ImageStream = CType(resources.GetObject("largeImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.largeImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.largeImageList.Images.SetKeyName(0, "")
        Me.largeImageList.Images.SetKeyName(1, "")
        Me.largeImageList.Images.SetKeyName(2, "")
        Me.largeImageList.Images.SetKeyName(3, "")
        Me.largeImageList.Images.SetKeyName(4, "")
        '
        'smallImageList
        '
        Me.smallImageList.ImageStream = CType(resources.GetObject("smallImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.smallImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.smallImageList.Images.SetKeyName(0, "")
        Me.smallImageList.Images.SetKeyName(1, "")
        Me.smallImageList.Images.SetKeyName(2, "")
        Me.smallImageList.Images.SetKeyName(3, "")
        Me.smallImageList.Images.SetKeyName(4, "")
        '
        'iconsCntxtMenuItem
        '
        Me.iconsCntxtMenuItem.Index = 0
        Me.iconsCntxtMenuItem.RadioCheck = True
        Me.iconsCntxtMenuItem.Text = "Icons"
        '
        'listCntxtMenuItem
        '
        Me.listCntxtMenuItem.Index = 1
        Me.listCntxtMenuItem.RadioCheck = True
        Me.listCntxtMenuItem.Text = "List"
        '
        'defaultContextMenu
        '
        Me.defaultContextMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.viewCntxtMenuItem, Me.menuItem8, Me.refreshCntxtMenuItem, Me.sepMenuItem12, Me.copyCntxtMenuItem, Me.pasteCntxtMenuItem, Me.sepMenuItem13, Me.deleteCntxtMenuItem, Me.renameCntxtMenuItem, Me.menuItem5, Me.propertiesCntxtMenuItem})
        '
        'viewCntxtMenuItem
        '
        Me.viewCntxtMenuItem.Index = 0
        Me.viewCntxtMenuItem.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.iconsCntxtMenuItem, Me.listCntxtMenuItem, Me.detailsCntxtMenuItem})
        Me.viewCntxtMenuItem.Text = "View"
        '
        'detailsCntxtMenuItem
        '
        Me.detailsCntxtMenuItem.Checked = True
        Me.detailsCntxtMenuItem.Index = 2
        Me.detailsCntxtMenuItem.RadioCheck = True
        Me.detailsCntxtMenuItem.Text = "Detail"
        '
        'menuItem8
        '
        Me.menuItem8.Index = 1
        Me.menuItem8.Text = "-"
        '
        'refreshCntxtMenuItem
        '
        Me.refreshCntxtMenuItem.Enabled = False
        Me.refreshCntxtMenuItem.Index = 2
        Me.refreshCntxtMenuItem.Text = "Refresh"
        '
        'sepMenuItem12
        '
        Me.sepMenuItem12.Index = 3
        Me.sepMenuItem12.Text = "-"
        '
        'copyCntxtMenuItem
        '
        Me.copyCntxtMenuItem.Enabled = False
        Me.copyCntxtMenuItem.Index = 4
        Me.copyCntxtMenuItem.Text = "Copy"
        '
        'Value
        '
        Me.Value.Name = "Value"
        Me.Value.Text = "Value"
        Me.Value.Width = 100
        '
        'columnValue
        '
        Me.columnValue.Name = "columnValue"
        Me.columnValue.Text = "Value"
        Me.columnValue.Width = 200
        '
        'columnHeader1
        '
        Me.columnHeader1.Name = "columnHeader1"
        Me.columnHeader1.Text = "Property Name"
        Me.columnHeader1.Width = 100
        '
        'columnHeader2
        '
        Me.columnHeader2.Name = "columnHeader2"
        Me.columnHeader2.Text = "Value"
        Me.columnHeader2.Width = 300
        '
        'linkLabel1
        '
        Me.linkLabel1.BackColor = System.Drawing.Color.Transparent
        Me.linkLabel1.LinkArea = New System.Windows.Forms.LinkArea(0, 15)
        Me.linkLabel1.Location = New System.Drawing.Point(3, 0)
        Me.linkLabel1.Name = "linkLabel1"
        Me.linkLabel1.Size = New System.Drawing.Size(106, 15)
        Me.linkLabel1.TabIndex = 5
        Me.linkLabel1.TabStop = True
        Me.linkLabel1.Text = "Show Properties"
        '
        'openReportFileDialog
        '
        Me.openReportFileDialog.DefaultExt = "rdl"
        Me.openReportFileDialog.Filter = "Report Files | *.rdl"
        Me.openReportFileDialog.Title = "Import Report File"
        '
        'columnName
        '
        Me.columnName.Name = "columnName"
        Me.columnName.Text = "Name"
        Me.columnName.Width = 100
        '
        'propertiesListview
        '
        Me.propertiesListview.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnHeader1, Me.columnHeader2})
        Me.propertiesListview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.propertiesListview.FullRowSelect = True
        Me.propertiesListview.Location = New System.Drawing.Point(0, 0)
        Me.propertiesListview.Name = "propertiesListview"
        Me.propertiesListview.Size = New System.Drawing.Size(334, 497)
        Me.propertiesListview.TabIndex = 11
        Me.propertiesListview.UseCompatibleStateImageBehavior = False
        Me.propertiesListview.View = System.Windows.Forms.View.Details
        '
        'titleLabel
        '
        Me.titleLabel.BackColor = System.Drawing.Color.Transparent
        Me.titleLabel.Location = New System.Drawing.Point(3, 0)
        Me.titleLabel.Name = "titleLabel"
        Me.titleLabel.Size = New System.Drawing.Size(92, 18)
        Me.titleLabel.TabIndex = 3
        Me.titleLabel.Text = "Catalog Explorer"
        '
        'menuItem2
        '
        Me.menuItem2.Index = -1
        Me.menuItem2.Text = ""
        '
        'pasteMenuItem
        '
        Me.pasteMenuItem.Enabled = False
        Me.pasteMenuItem.Index = 1
        Me.pasteMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlV
        Me.pasteMenuItem.Text = "Paste"
        '
        'menuItem10
        '
        Me.menuItem10.Index = 2
        Me.menuItem10.Text = "-"
        '
        'editMenuItem
        '
        Me.editMenuItem.Index = 1
        Me.editMenuItem.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.copyMenuItem, Me.pasteMenuItem, Me.menuItem10, Me.deleteMenuItem})
        Me.editMenuItem.Text = "&Edit"
        '
        'copyMenuItem
        '
        Me.copyMenuItem.Index = 0
        Me.copyMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlC
        Me.copyMenuItem.Text = "Copy"
        '
        'deleteMenuItem
        '
        Me.deleteMenuItem.Enabled = False
        Me.deleteMenuItem.Index = 3
        Me.deleteMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlD
        Me.deleteMenuItem.Text = "&Delete"
        '
        'viewListMenuItem
        '
        Me.viewListMenuItem.Index = 1
        Me.viewListMenuItem.RadioCheck = True
        Me.viewListMenuItem.Text = "&List"
        '
        'viewDetailsMenuItem
        '
        Me.viewDetailsMenuItem.Checked = True
        Me.viewDetailsMenuItem.Index = 2
        Me.viewDetailsMenuItem.RadioCheck = True
        Me.viewDetailsMenuItem.Text = "&Details"
        '
        'viewMenuItem
        '
        Me.viewMenuItem.Index = 2
        Me.viewMenuItem.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.viewIconsMenuItem, Me.viewListMenuItem, Me.viewDetailsMenuItem, Me.separatorMenuItem, Me.viewRefreshMenuItem})
        Me.viewMenuItem.Text = "&View"
        '
        'viewIconsMenuItem
        '
        Me.viewIconsMenuItem.Index = 0
        Me.viewIconsMenuItem.RadioCheck = True
        Me.viewIconsMenuItem.Text = "Ico&ns"
        '
        'separatorMenuItem
        '
        Me.separatorMenuItem.Index = 3
        Me.separatorMenuItem.Text = "-"
        '
        'viewRefreshMenuItem
        '
        Me.viewRefreshMenuItem.Enabled = False
        Me.viewRefreshMenuItem.Index = 4
        Me.viewRefreshMenuItem.Text = "&Refresh"
        '
        'menuItem1
        '
        Me.menuItem1.Index = 0
        Me.menuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.fileNewFolderMenuItem})
        Me.menuItem1.Text = "Ne&w"
        '
        'fileNewFolderMenuItem
        '
        Me.fileNewFolderMenuItem.Enabled = False
        Me.fileNewFolderMenuItem.Index = 0
        Me.fileNewFolderMenuItem.Text = "&Folder"
        '
        'statusBar1
        '
        Me.statusBar1.Location = New System.Drawing.Point(0, 570)
        Me.statusBar1.Name = "statusBar1"
        Me.statusBar1.Size = New System.Drawing.Size(864, 27)
        Me.statusBar1.TabIndex = 17
        Me.statusBar1.Text = "Ready"
        '
        'fileMenuItem
        '
        Me.fileMenuItem.Index = 0
        Me.fileMenuItem.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuItem1, Me.menuItemImport, Me.menuItem4, Me.fileExitMenuItem})
        Me.fileMenuItem.Text = "&File"
        '
        'menuItemImport
        '
        Me.menuItemImport.Enabled = False
        Me.menuItemImport.Index = 1
        Me.menuItemImport.Text = "&Import Report..."
        '
        'menuItem4
        '
        Me.menuItem4.Index = 2
        Me.menuItem4.Text = "-"
        '
        'fileExitMenuItem
        '
        Me.fileExitMenuItem.Index = 3
        Me.fileExitMenuItem.Text = "E&xit"
        '
        'mainMenu1
        '
        Me.mainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.fileMenuItem, Me.editMenuItem, Me.viewMenuItem, Me.helpMenuItem})
        '
        'helpMenuItem
        '
        Me.helpMenuItem.Index = 3
        Me.helpMenuItem.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.aboutMenuItem})
        Me.helpMenuItem.Text = "&Help"
        '
        'aboutMenuItem
        '
        Me.aboutMenuItem.Index = 0
        Me.aboutMenuItem.Text = "&About RSExplorer..."
        '
        'toolbarImageList
        '
        Me.toolbarImageList.ImageStream = CType(resources.GetObject("toolbarImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.toolbarImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.toolbarImageList.Images.SetKeyName(0, "")
        Me.toolbarImageList.Images.SetKeyName(1, "")
        Me.toolbarImageList.Images.SetKeyName(2, "")
        Me.toolbarImageList.Images.SetKeyName(3, "")
        Me.toolbarImageList.Images.SetKeyName(4, "")
        Me.toolbarImageList.Images.SetKeyName(5, "")
        '
        'panel1
        '
        Me.panel1.Controls.Add(Me.Panel4)
        Me.panel1.Controls.Add(Me.serverPathTextbox)
        Me.panel1.Controls.Add(Me.goButton)
        Me.panel1.Controls.Add(Me.label1)
        Me.panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.panel1.Location = New System.Drawing.Point(0, 32)
        Me.panel1.Name = "panel1"
        Me.panel1.Size = New System.Drawing.Size(864, 23)
        Me.panel1.TabIndex = 13
        '
        'Panel4
        '
        Me.Panel4.Location = New System.Drawing.Point(550, 65)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(146, 46)
        Me.Panel4.TabIndex = 6
        '
        'serverPathTextbox
        '
        Me.serverPathTextbox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.serverPathTextbox.Location = New System.Drawing.Point(88, 0)
        Me.serverPathTextbox.Name = "serverPathTextbox"
        Me.serverPathTextbox.Size = New System.Drawing.Size(712, 20)
        Me.serverPathTextbox.TabIndex = 3
        Me.serverPathTextbox.Text = "http://{your server name}/reportserver"
        '
        'goButton
        '
        Me.goButton.Dock = System.Windows.Forms.DockStyle.Right
        Me.goButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.goButton.Image = Global.Microsoft.Samples.ReportingServices.RSExplorer.My.Resources.Resources.goButton_Image
        Me.goButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.goButton.Location = New System.Drawing.Point(800, 0)
        Me.goButton.Name = "goButton"
        Me.goButton.Size = New System.Drawing.Size(64, 23)
        Me.goButton.TabIndex = 2
        Me.goButton.Text = "Go"
        '
        'label1
        '
        Me.label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.label1.Location = New System.Drawing.Point(0, 0)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(88, 23)
        Me.label1.TabIndex = 0
        Me.label1.Text = "Server Address"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'upButton
        '
        Me.upButton.Enabled = False
        Me.upButton.ImageIndex = 0
        Me.upButton.Name = "upButton"
        Me.upButton.ToolTipText = "Up"
        '
        'refreshButton
        '
        Me.refreshButton.Enabled = False
        Me.refreshButton.ImageIndex = 3
        Me.refreshButton.Name = "refreshButton"
        Me.refreshButton.ToolTipText = "Refresh"
        '
        'iconCntxtMenuItem
        '
        Me.iconCntxtMenuItem.Index = -1
        Me.iconCntxtMenuItem.Text = ""
        '
        'menuItem9
        '
        Me.menuItem9.Index = -1
        Me.menuItem9.Text = "-"
        '
        'menuItem3
        '
        Me.menuItem3.Index = -1
        Me.menuItem3.Text = "-"
        '
        'panel2
        '
        Me.panel2.Controls.Add(Me.toolBar1)
        Me.panel2.Controls.Add(Me.panel1)
        Me.panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.panel2.Location = New System.Drawing.Point(0, 0)
        Me.panel2.Name = "panel2"
        Me.panel2.Size = New System.Drawing.Size(864, 55)
        Me.panel2.TabIndex = 18
        '
        'toolBar1
        '
        Me.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.toolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.newFolderButton, Me.editButton, Me.deleteButton, Me.separatorButton, Me.upButton, Me.refreshButton})
        Me.toolBar1.ButtonSize = New System.Drawing.Size(23, 23)
        Me.toolBar1.DropDownArrows = True
        Me.toolBar1.ImageList = Me.toolbarImageList
        Me.toolBar1.Location = New System.Drawing.Point(0, 0)
        Me.toolBar1.Name = "toolBar1"
        Me.toolBar1.ShowToolTips = True
        Me.toolBar1.Size = New System.Drawing.Size(864, 28)
        Me.toolBar1.TabIndex = 14
        '
        'newFolderButton
        '
        Me.newFolderButton.Enabled = False
        Me.newFolderButton.ImageIndex = 1
        Me.newFolderButton.Name = "newFolderButton"
        Me.newFolderButton.ToolTipText = "New Folder"
        '
        'editButton
        '
        Me.editButton.Enabled = False
        Me.editButton.ImageIndex = 4
        Me.editButton.Name = "editButton"
        Me.editButton.ToolTipText = "Edit"
        '
        'deleteButton
        '
        Me.deleteButton.Enabled = False
        Me.deleteButton.ImageIndex = 2
        Me.deleteButton.Name = "deleteButton"
        Me.deleteButton.ToolTipText = "Delete"
        '
        'separatorButton
        '
        Me.separatorButton.Name = "separatorButton"
        Me.separatorButton.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 55)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel7)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel6)
        Me.SplitContainer1.Size = New System.Drawing.Size(864, 515)
        Me.SplitContainer1.SplitterDistance = 526
        Me.SplitContainer1.TabIndex = 19
        Me.SplitContainer1.Text = "SplitContainer1"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.explorerListView)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 14)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(526, 501)
        Me.Panel5.TabIndex = 6
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.titleLabel)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(526, 14)
        Me.Panel3.TabIndex = 5
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.propertiesListview)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Location = New System.Drawing.Point(0, 18)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(334, 497)
        Me.Panel7.TabIndex = 7
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.linkLabel1)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(334, 18)
        Me.Panel6.TabIndex = 7
        '
        'Explorer
        '
        Me.ClientSize = New System.Drawing.Size(864, 597)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.statusBar1)
        Me.Controls.Add(Me.panel2)
        Me.Menu = Me.mainMenu1
        Me.Name = "Explorer"
        Me.Text = "Explorer"
        Me.panel1.ResumeLayout(False)
        Me.panel1.PerformLayout()
        Me.panel2.ResumeLayout(False)
        Me.panel2.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents largeImageList As System.Windows.Forms.ImageList
    Friend WithEvents smallImageList As System.Windows.Forms.ImageList
    Friend WithEvents toolbarImageList As System.Windows.Forms.ImageList
    Friend WithEvents deleteCntxtMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents renameCntxtMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents pasteCntxtMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents sepMenuItem13 As System.Windows.Forms.MenuItem
    Friend WithEvents menuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents nameColumnHeader As System.Windows.Forms.ColumnHeader
    Friend WithEvents sizeColumnHeader As System.Windows.Forms.ColumnHeader
    Friend WithEvents propertiesCntxtMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents explorerListView As System.Windows.Forms.ListView
    Friend WithEvents typeColumnHeader As System.Windows.Forms.ColumnHeader
    Friend WithEvents modifiedColumnHeader As System.Windows.Forms.ColumnHeader
    Friend WithEvents iconsCntxtMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents listCntxtMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents defaultContextMenu As System.Windows.Forms.ContextMenu
    Friend WithEvents viewCntxtMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents detailsCntxtMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents menuItem8 As System.Windows.Forms.MenuItem
    Friend WithEvents refreshCntxtMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents sepMenuItem12 As System.Windows.Forms.MenuItem
    Friend WithEvents copyCntxtMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents Value As System.Windows.Forms.ColumnHeader
    Friend WithEvents columnValue As System.Windows.Forms.ColumnHeader
    Friend WithEvents columnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents columnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents linkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents openReportFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents columnName As System.Windows.Forms.ColumnHeader
    Friend WithEvents propertiesListview As System.Windows.Forms.ListView
    Friend WithEvents titleLabel As System.Windows.Forms.Label
    Friend WithEvents menuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents pasteMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents menuItem10 As System.Windows.Forms.MenuItem
    Friend WithEvents editMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents copyMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents deleteMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents viewListMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents viewDetailsMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents viewMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents viewIconsMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents separatorMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents viewRefreshMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents menuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents fileNewFolderMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents statusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents fileMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents menuItemImport As System.Windows.Forms.MenuItem
    Friend WithEvents menuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents fileExitMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents mainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents helpMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents aboutMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents panel1 As System.Windows.Forms.Panel
    Friend WithEvents serverPathTextbox As System.Windows.Forms.TextBox
    Friend WithEvents goButton As System.Windows.Forms.Button
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents upButton As System.Windows.Forms.ToolBarButton
    Friend WithEvents refreshButton As System.Windows.Forms.ToolBarButton
    Friend WithEvents iconCntxtMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents menuItem9 As System.Windows.Forms.MenuItem
    Friend WithEvents menuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents panel2 As System.Windows.Forms.Panel
    Friend WithEvents toolBar1 As System.Windows.Forms.ToolBar
    Friend WithEvents newFolderButton As System.Windows.Forms.ToolBarButton
    Friend WithEvents editButton As System.Windows.Forms.ToolBarButton
    Friend WithEvents deleteButton As System.Windows.Forms.ToolBarButton
    Friend WithEvents separatorButton As System.Windows.Forms.ToolBarButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
End Class
