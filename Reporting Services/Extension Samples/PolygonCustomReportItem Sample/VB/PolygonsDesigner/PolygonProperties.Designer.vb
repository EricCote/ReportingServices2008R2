Partial Class PolygonProperties 'The Partial modifier is only required on one class definition per project.
    ''' <summary>
    ''' Required designer variable.
    ''' </summary>
    Private components As System.ComponentModel.IContainer = Nothing

    ''' <summary>
    ''' Clean up any resources being used.
    ''' </summary>
    ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso Not (components Is Nothing) Then
            components.Dispose()
        End If

        MyBase.Dispose(disposing)
    End Sub

#Region "Windows Form Designer generated code"

    ''' <summary>
    ''' Required method for Designer support - do not modify
    ''' the contents of this method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent()
        Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(PolygonProperties))
        Me.DataSetName = New System.Windows.Forms.ComboBox()
        Me.tabControl1 = New System.Windows.Forms.TabControl()
        Me.tabPage1 = New System.Windows.Forms.TabPage()
        Me.label12 = New System.Windows.Forms.Label()
        Me.Hyperlink = New System.Windows.Forms.ComboBox()
        Me.label11 = New System.Windows.Forms.Label()
        Me.label10 = New System.Windows.Forms.Label()
        Me.PointID = New System.Windows.Forms.ComboBox()
        Me.label9 = New System.Windows.Forms.Label()
        Me.ShapeID = New System.Windows.Forms.ComboBox()
        Me.label8 = New System.Windows.Forms.Label()
        Me.YValue = New System.Windows.Forms.ComboBox()
        Me.label7 = New System.Windows.Forms.Label()
        Me.XValue = New System.Windows.Forms.ComboBox()
        Me.tabPage2 = New System.Windows.Forms.TabPage()
        Me.label2 = New System.Windows.Forms.Label()
        Me.Translucency = New System.Windows.Forms.ComboBox()
        Me.label5 = New System.Windows.Forms.Label()
        Me.MaxY = New System.Windows.Forms.TextBox()
        Me.label6 = New System.Windows.Forms.Label()
        Me.MinY = New System.Windows.Forms.TextBox()
        Me.label4 = New System.Windows.Forms.Label()
        Me.MaxX = New System.Windows.Forms.TextBox()
        Me.label3 = New System.Windows.Forms.Label()
        Me.MinX = New System.Windows.Forms.TextBox()
        Me.label1 = New System.Windows.Forms.Label()
        Me.ShapeColor = New System.Windows.Forms.ComboBox()
        Me.ProportionalScaling = New System.Windows.Forms.CheckBox()
        Me.OK = New System.Windows.Forms.Button()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.tabControl1.SuspendLayout()
        Me.tabPage1.SuspendLayout()
        Me.tabPage2.SuspendLayout()
        Me.SuspendLayout()
        ' 
        ' DataSetName
        ' 
        Me.DataSetName.FormattingEnabled = True
        resources.ApplyResources(Me.DataSetName, "DataSetName")
        Me.DataSetName.Name = "DataSetName"
        ' 
        ' tabControl1
        ' 
        Me.tabControl1.Controls.Add(Me.tabPage1)
        Me.tabControl1.Controls.Add(Me.tabPage2)
        resources.ApplyResources(Me.tabControl1, "tabControl1")
        Me.tabControl1.Name = "tabControl1"
        Me.tabControl1.SelectedIndex = 0
        ' 
        ' tabPage1
        ' 
        Me.tabPage1.Controls.Add(Me.label12)
        Me.tabPage1.Controls.Add(Me.Hyperlink)
        Me.tabPage1.Controls.Add(Me.label11)
        Me.tabPage1.Controls.Add(Me.DataSetName)
        Me.tabPage1.Controls.Add(Me.label10)
        Me.tabPage1.Controls.Add(Me.PointID)
        Me.tabPage1.Controls.Add(Me.label9)
        Me.tabPage1.Controls.Add(Me.ShapeID)
        Me.tabPage1.Controls.Add(Me.label8)
        Me.tabPage1.Controls.Add(Me.YValue)
        Me.tabPage1.Controls.Add(Me.label7)
        Me.tabPage1.Controls.Add(Me.XValue)
        resources.ApplyResources(Me.tabPage1, "tabPage1")
        Me.tabPage1.Name = "tabPage1"
        ' 
        ' label12
        ' 
        resources.ApplyResources(Me.label12, "label12")
        Me.label12.Name = "label12"
        ' 
        ' Hyperlink
        ' 
        Me.Hyperlink.FormattingEnabled = True
        resources.ApplyResources(Me.Hyperlink, "Hyperlink")
        Me.Hyperlink.Name = "Hyperlink"
        ' 
        ' label11
        ' 
        resources.ApplyResources(Me.label11, "label11")
        Me.label11.Name = "label11"
        ' 
        ' label10
        ' 
        resources.ApplyResources(Me.label10, "label10")
        Me.label10.Name = "label10"
        ' 
        ' PointID
        ' 
        Me.PointID.FormattingEnabled = True
        resources.ApplyResources(Me.PointID, "PointID")
        Me.PointID.Name = "PointID"
        ' 
        ' label9
        ' 
        resources.ApplyResources(Me.label9, "label9")
        Me.label9.Name = "label9"
        ' 
        ' ShapeID
        ' 
        Me.ShapeID.FormattingEnabled = True
        resources.ApplyResources(Me.ShapeID, "ShapeID")
        Me.ShapeID.Name = "ShapeID"
        ' 
        ' label8
        ' 
        resources.ApplyResources(Me.label8, "label8")
        Me.label8.Name = "label8"
        ' 
        ' YValue
        ' 
        Me.YValue.FormattingEnabled = True
        resources.ApplyResources(Me.YValue, "YValue")
        Me.YValue.Name = "YValue"
        ' 
        ' label7
        ' 
        resources.ApplyResources(Me.label7, "label7")
        Me.label7.Name = "label7"
        ' 
        ' XValue
        ' 
        Me.XValue.FormattingEnabled = True
        resources.ApplyResources(Me.XValue, "XValue")
        Me.XValue.Name = "XValue"
        ' 
        ' tabPage2
        ' 
        Me.tabPage2.Controls.Add(Me.label2)
        Me.tabPage2.Controls.Add(Me.Translucency)
        Me.tabPage2.Controls.Add(Me.label5)
        Me.tabPage2.Controls.Add(Me.MaxY)
        Me.tabPage2.Controls.Add(Me.label6)
        Me.tabPage2.Controls.Add(Me.MinY)
        Me.tabPage2.Controls.Add(Me.label4)
        Me.tabPage2.Controls.Add(Me.MaxX)
        Me.tabPage2.Controls.Add(Me.label3)
        Me.tabPage2.Controls.Add(Me.MinX)
        Me.tabPage2.Controls.Add(Me.label1)
        Me.tabPage2.Controls.Add(Me.ShapeColor)
        Me.tabPage2.Controls.Add(Me.ProportionalScaling)
        resources.ApplyResources(Me.tabPage2, "tabPage2")
        Me.tabPage2.Name = "tabPage2"
        ' 
        ' label2
        ' 
        resources.ApplyResources(Me.label2, "label2")
        Me.label2.Name = "label2"
        ' 
        ' Translucency
        ' 
        Me.Translucency.FormattingEnabled = True
        Me.Translucency.Items.AddRange(New Object() {resources.GetString("Translucency.Items"), resources.GetString("Translucency.Items1"), resources.GetString("Translucency.Items2"), resources.GetString("Translucency.Items3")})
        resources.ApplyResources(Me.Translucency, "Translucency")
        Me.Translucency.Name = "Translucency"
        ' 
        ' label5
        ' 
        resources.ApplyResources(Me.label5, "label5")
        Me.label5.Name = "label5"
        ' 
        ' MaxY
        ' 
        resources.ApplyResources(Me.MaxY, "MaxY")
        Me.MaxY.Name = "MaxY"
        ' 
        ' label6
        ' 
        resources.ApplyResources(Me.label6, "label6")
        Me.label6.Name = "label6"
        ' 
        ' MinY
        ' 
        resources.ApplyResources(Me.MinY, "MinY")
        Me.MinY.Name = "MinY"
        ' 
        ' label4
        ' 
        resources.ApplyResources(Me.label4, "label4")
        Me.label4.Name = "label4"
        ' 
        ' MaxX
        ' 
        resources.ApplyResources(Me.MaxX, "MaxX")
        Me.MaxX.Name = "MaxX"
        ' 
        ' label3
        ' 
        resources.ApplyResources(Me.label3, "label3")
        Me.label3.Name = "label3"
        ' 
        ' MinX
        ' 
        resources.ApplyResources(Me.MinX, "MinX")
        Me.MinX.Name = "MinX"
        ' 
        ' label1
        ' 
        resources.ApplyResources(Me.label1, "label1")
        Me.label1.Name = "label1"
        ' 
        ' ShapeColor
        ' 
        Me.ShapeColor.FormattingEnabled = True
        resources.ApplyResources(Me.ShapeColor, "ShapeColor")
        Me.ShapeColor.Name = "ShapeColor"
        ' 
        ' ProportionalScaling
        ' 
        resources.ApplyResources(Me.ProportionalScaling, "ProportionalScaling")
        Me.ProportionalScaling.Name = "ProportionalScaling"
        ' 
        ' OK
        ' 
        Me.OK.DialogResult = System.Windows.Forms.DialogResult.OK
        resources.ApplyResources(Me.OK, "OK")
        Me.OK.Name = "OK"
        ' 
        ' Cancel
        ' 
        Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        resources.ApplyResources(Me.Cancel, "Cancel")
        Me.Cancel.Name = "Cancel"
        ' 
        ' PolygonProperties
        ' 
        Me.AcceptButton = Me.OK
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel
        Me.Controls.Add(Cancel)
        Me.Controls.Add(OK)
        Me.Controls.Add(tabControl1)
        Me.Name = "PolygonProperties"
        Me.tabControl1.ResumeLayout(False)
        Me.tabPage1.ResumeLayout(False)
        Me.tabPage1.PerformLayout()
        Me.tabPage2.ResumeLayout(False)
        Me.tabPage2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private tabControl1 As System.Windows.Forms.TabControl
    Private tabPage1 As System.Windows.Forms.TabPage
    Private tabPage2 As System.Windows.Forms.TabPage
    Private label1 As System.Windows.Forms.Label
    Private WithEvents ShapeColor As System.Windows.Forms.ComboBox
    Private ProportionalScaling As System.Windows.Forms.CheckBox
    Private label5 As System.Windows.Forms.Label
    Private MaxY As System.Windows.Forms.TextBox
    Private label6 As System.Windows.Forms.Label
    Private MinY As System.Windows.Forms.TextBox
    Private label4 As System.Windows.Forms.Label
    Private MaxX As System.Windows.Forms.TextBox
    Private label3 As System.Windows.Forms.Label
    Private MinX As System.Windows.Forms.TextBox
    Private label10 As System.Windows.Forms.Label
    Private WithEvents PointID As System.Windows.Forms.ComboBox
    Private label9 As System.Windows.Forms.Label
    Private WithEvents ShapeID As System.Windows.Forms.ComboBox
    Private label8 As System.Windows.Forms.Label
    Private WithEvents YValue As System.Windows.Forms.ComboBox
    Private label7 As System.Windows.Forms.Label
    Private WithEvents XValue As System.Windows.Forms.ComboBox
    Private label11 As System.Windows.Forms.Label
    Private label12 As System.Windows.Forms.Label
    Private WithEvents Hyperlink As System.Windows.Forms.ComboBox
    Private WithEvents OK As System.Windows.Forms.Button
    Private Cancel As System.Windows.Forms.Button
    Private WithEvents DataSetName As System.Windows.Forms.ComboBox
    Private label2 As System.Windows.Forms.Label
    Private WithEvents Translucency As System.Windows.Forms.ComboBox
End Class