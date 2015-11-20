namespace Microsoft.Samples.ReportingServices.FindRenderSave
{
    partial class FindRenderSave
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindRenderSave));
            this.closeButton = new System.Windows.Forms.Button();
            this.renderAsLabel = new System.Windows.Forms.Label();
            this.itemsFoundLabel = new System.Windows.Forms.Label();
            this.introLabel = new System.Windows.Forms.Label();
            this.searchStringLabel = new System.Windows.Forms.Label();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.pathLabel = new System.Windows.Forms.Label();
            this.formatComboBox = new System.Windows.Forms.ComboBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.conditionComboBox = new System.Windows.Forms.ComboBox();
            this.searchByLabel = new System.Windows.Forms.Label();
            this.saveReportButton = new System.Windows.Forms.Button();
            this.reportListView = new System.Windows.Forms.ListBox();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.saveReportDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
            this.closeButton.Name = "closeButton";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // renderAsLabel
            // 
            resources.ApplyResources(this.renderAsLabel, "renderAsLabel");
            this.renderAsLabel.Name = "renderAsLabel";
            // 
            // itemsFoundLabel
            // 
            resources.ApplyResources(this.itemsFoundLabel, "itemsFoundLabel");
            this.itemsFoundLabel.Name = "itemsFoundLabel";
            // 
            // introLabel
            // 
            resources.ApplyResources(this.introLabel, "introLabel");
            this.introLabel.Name = "introLabel";
            // 
            // searchStringLabel
            // 
            resources.ApplyResources(this.searchStringLabel, "searchStringLabel");
            this.searchStringLabel.Name = "searchStringLabel";
            // 
            // pathTextBox
            // 
            this.pathTextBox.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.pathTextBox, "pathTextBox");
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.ReadOnly = true;
            // 
            // searchTextBox
            // 
            resources.ApplyResources(this.searchTextBox, "searchTextBox");
            this.searchTextBox.Name = "searchTextBox";
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.AcceptsReturn = true;
            this.descriptionTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.descriptionTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            resources.ApplyResources(this.descriptionTextBox, "descriptionTextBox");
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ReadOnly = true;
            // 
            // pathLabel
            // 
            resources.ApplyResources(this.pathLabel, "pathLabel");
            this.pathLabel.Name = "pathLabel";
            // 
            // formatComboBox
            // 
            this.formatComboBox.FormattingEnabled = true;
            this.formatComboBox.Items.AddRange(new object[] {
            resources.GetString("formatComboBox.Items"),
            resources.GetString("formatComboBox.Items1"),
            resources.GetString("formatComboBox.Items2"),
            resources.GetString("formatComboBox.Items3")});
            resources.ApplyResources(this.formatComboBox, "formatComboBox");
            this.formatComboBox.Name = "formatComboBox";
            // 
            // searchButton
            // 
            resources.ApplyResources(this.searchButton, "searchButton");
            this.searchButton.Name = "searchButton";
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // conditionComboBox
            // 
            this.conditionComboBox.FormattingEnabled = true;
            this.conditionComboBox.Items.AddRange(new object[] {
            resources.GetString("conditionComboBox.Items"),
            resources.GetString("conditionComboBox.Items1"),
            resources.GetString("conditionComboBox.Items2")});
            resources.ApplyResources(this.conditionComboBox, "conditionComboBox");
            this.conditionComboBox.Name = "conditionComboBox";
            // 
            // searchByLabel
            // 
            resources.ApplyResources(this.searchByLabel, "searchByLabel");
            this.searchByLabel.Name = "searchByLabel";
            // 
            // saveReportButton
            // 
            resources.ApplyResources(this.saveReportButton, "saveReportButton");
            this.saveReportButton.Name = "saveReportButton";
            this.saveReportButton.Click += new System.EventHandler(this.saveReportButton_Click);
            // 
            // reportListView
            // 
            this.reportListView.DisplayMember = "Text";
            this.reportListView.FormattingEnabled = true;
            resources.ApplyResources(this.reportListView, "reportListView");
            this.reportListView.Name = "reportListView";
            this.reportListView.SelectedIndexChanged += new System.EventHandler(this.reportListView_SelectedIndexChanged);
            // 
            // descriptionLabel
            // 
            resources.ApplyResources(this.descriptionLabel, "descriptionLabel");
            this.descriptionLabel.Name = "descriptionLabel";
            // 
            // FindRenderSave
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.renderAsLabel);
            this.Controls.Add(this.itemsFoundLabel);
            this.Controls.Add(this.introLabel);
            this.Controls.Add(this.searchStringLabel);
            this.Controls.Add(this.pathTextBox);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.pathLabel);
            this.Controls.Add(this.formatComboBox);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.conditionComboBox);
            this.Controls.Add(this.searchByLabel);
            this.Controls.Add(this.saveReportButton);
            this.Controls.Add(this.reportListView);
            this.Controls.Add(this.descriptionLabel);
            this.Name = "FindRenderSave";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label renderAsLabel;
        private System.Windows.Forms.Label itemsFoundLabel;
        private System.Windows.Forms.Label introLabel;
        private System.Windows.Forms.Label searchStringLabel;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.ComboBox formatComboBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.ComboBox conditionComboBox;
        private System.Windows.Forms.Label searchByLabel;
        private System.Windows.Forms.Button saveReportButton;
        private System.Windows.Forms.ListBox reportListView;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.SaveFileDialog saveReportDialog;
    }
}