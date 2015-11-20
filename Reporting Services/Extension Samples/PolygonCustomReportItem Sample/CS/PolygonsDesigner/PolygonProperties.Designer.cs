namespace Microsoft.Samples.ReportingServices
{
	partial class PolygonProperties
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
        private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}

			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PolygonProperties));
            this.DataSetName = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label12 = new System.Windows.Forms.Label();
            this.Hyperlink = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.PointID = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ShapeID = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.YValue = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.XValue = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.Translucency = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.MaxY = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.MinY = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.MaxX = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.MinX = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ShapeColor = new System.Windows.Forms.ComboBox();
            this.ProportionalScaling = new System.Windows.Forms.CheckBox();
            this.OK = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataSetName
            // 
            this.DataSetName.FormattingEnabled = true;
            resources.ApplyResources(this.DataSetName, "DataSetName");
            this.DataSetName.Name = "DataSetName";
            this.DataSetName.SelectedIndexChanged += new System.EventHandler(this.DataSetName_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.Hyperlink);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.DataSetName);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.PointID);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.ShapeID);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.YValue);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.XValue);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // Hyperlink
            // 
            this.Hyperlink.FormattingEnabled = true;
            resources.ApplyResources(this.Hyperlink, "Hyperlink");
            this.Hyperlink.Name = "Hyperlink";
            this.Hyperlink.SelectionChangeCommitted += new System.EventHandler(this.EditableCombo_SelectionChangeCommitted);
            this.Hyperlink.SelectedIndexChanged += new System.EventHandler(this.EditableCombo_SelectedIndexChanged);
            this.Hyperlink.DropDown += new System.EventHandler(this.EditableCombo_DropDown);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // PointID
            // 
            this.PointID.FormattingEnabled = true;
            resources.ApplyResources(this.PointID, "PointID");
            this.PointID.Name = "PointID";
            this.PointID.SelectionChangeCommitted += new System.EventHandler(this.EditableCombo_SelectionChangeCommitted);
            this.PointID.SelectedIndexChanged += new System.EventHandler(this.EditableCombo_SelectedIndexChanged);
            this.PointID.DropDown += new System.EventHandler(this.EditableCombo_DropDown);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // ShapeID
            // 
            this.ShapeID.FormattingEnabled = true;
            resources.ApplyResources(this.ShapeID, "ShapeID");
            this.ShapeID.Name = "ShapeID";
            this.ShapeID.SelectionChangeCommitted += new System.EventHandler(this.EditableCombo_SelectionChangeCommitted);
            this.ShapeID.SelectedIndexChanged += new System.EventHandler(this.EditableCombo_SelectedIndexChanged);
            this.ShapeID.DropDown += new System.EventHandler(this.EditableCombo_DropDown);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // YValue
            // 
            this.YValue.FormattingEnabled = true;
            resources.ApplyResources(this.YValue, "YValue");
            this.YValue.Name = "YValue";
            this.YValue.SelectionChangeCommitted += new System.EventHandler(this.EditableCombo_SelectionChangeCommitted);
            this.YValue.SelectedIndexChanged += new System.EventHandler(this.EditableCombo_SelectedIndexChanged);
            this.YValue.DropDown += new System.EventHandler(this.EditableCombo_DropDown);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // XValue
            // 
            this.XValue.FormattingEnabled = true;
            resources.ApplyResources(this.XValue, "XValue");
            this.XValue.Name = "XValue";
            this.XValue.SelectionChangeCommitted += new System.EventHandler(this.EditableCombo_SelectionChangeCommitted);
            this.XValue.SelectedIndexChanged += new System.EventHandler(this.EditableCombo_SelectedIndexChanged);
            this.XValue.DropDown += new System.EventHandler(this.EditableCombo_DropDown);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.Translucency);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.MaxY);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.MinY);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.MaxX);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.MinX);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.ShapeColor);
            this.tabPage2.Controls.Add(this.ProportionalScaling);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // Translucency
            // 
            this.Translucency.FormattingEnabled = true;
            this.Translucency.Items.AddRange(new object[] {
            resources.GetString("Translucency.Items"),
            resources.GetString("Translucency.Items1"),
            resources.GetString("Translucency.Items2"),
            resources.GetString("Translucency.Items3")});
            resources.ApplyResources(this.Translucency, "Translucency");
            this.Translucency.Name = "Translucency";
            this.Translucency.SelectionChangeCommitted += new System.EventHandler(this.EditableCombo_SelectionChangeCommitted);
            this.Translucency.SelectedIndexChanged += new System.EventHandler(this.EditableCombo_SelectedIndexChanged);
            this.Translucency.DropDown += new System.EventHandler(this.EditableCombo_DropDown);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // MaxY
            // 
            resources.ApplyResources(this.MaxY, "MaxY");
            this.MaxY.Name = "MaxY";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // MinY
            // 
            resources.ApplyResources(this.MinY, "MinY");
            this.MinY.Name = "MinY";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // MaxX
            // 
            resources.ApplyResources(this.MaxX, "MaxX");
            this.MaxX.Name = "MaxX";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // MinX
            // 
            resources.ApplyResources(this.MinX, "MinX");
            this.MinX.Name = "MinX";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // ShapeColor
            // 
            this.ShapeColor.FormattingEnabled = true;
            resources.ApplyResources(this.ShapeColor, "ShapeColor");
            this.ShapeColor.Name = "ShapeColor";
            this.ShapeColor.SelectionChangeCommitted += new System.EventHandler(this.EditableCombo_SelectionChangeCommitted);
            this.ShapeColor.SelectedIndexChanged += new System.EventHandler(this.EditableCombo_SelectedIndexChanged);
            this.ShapeColor.DropDown += new System.EventHandler(this.EditableCombo_DropDown);
            // 
            // ProportionalScaling
            // 
            resources.ApplyResources(this.ProportionalScaling, "ProportionalScaling");
            this.ProportionalScaling.Name = "ProportionalScaling";
            // 
            // OK
            // 
            this.OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.OK, "OK");
            this.OK.Name = "OK";
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.Cancel, "Cancel");
            this.Cancel.Name = "Cancel";
            // 
            // PolygonProperties
            // 
            this.AcceptButton = this.OK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.tabControl1);
            this.Name = "PolygonProperties";
            this.Load += new System.EventHandler(this.PolygonProperties_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox ShapeColor;
		private System.Windows.Forms.CheckBox ProportionalScaling;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox MaxY;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox MinY;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox MaxX;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox MinX;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ComboBox PointID;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ComboBox ShapeID;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox YValue;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox XValue;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.ComboBox Hyperlink;
		private System.Windows.Forms.Button OK;
		private System.Windows.Forms.Button Cancel;
		private System.Windows.Forms.ComboBox DataSetName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox Translucency;
	}
}