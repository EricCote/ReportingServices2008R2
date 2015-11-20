namespace RSPermissionsCS
{
    partial class PermissionsDialog
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PermissionsDialog));
            this.mnuTextFile = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuUserPermissions = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnProperties = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlObjectexplorerdetails = new System.Windows.Forms.Panel();
            this.pnlgridview = new System.Windows.Forms.Panel();
            this.lvChildren = new System.Windows.Forms.ListView();
            this.lblReportItemDetails = new System.Windows.Forms.Label();
            this.pnlFormbuttons = new System.Windows.Forms.Panel();
            this.pnlObjectexplorer = new System.Windows.Forms.Panel();
            this.tvReportitems = new System.Windows.Forms.TreeView();
            this.smallImageList = new System.Windows.Forms.ImageList(this.components);
            this.mnuTextFile.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlObjectexplorerdetails.SuspendLayout();
            this.pnlgridview.SuspendLayout();
            this.pnlObjectexplorer.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuTextFile
            // 
            this.mnuTextFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuUserPermissions});
            this.mnuTextFile.Name = "mnuTextFile";
            this.mnuTextFile.Size = new System.Drawing.Size(138, 26);
            // 
            // mnuUserPermissions
            // 
            this.mnuUserPermissions.Name = "mnuUserPermissions";
            this.mnuUserPermissions.Size = new System.Drawing.Size(137, 22);
            this.mnuUserPermissions.Text = "Permissions";
            this.mnuUserPermissions.Click += new System.EventHandler(this.mnuUserPermissions_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblMessage);
            this.panel1.Controls.Add(this.btnProperties);
            this.panel1.Controls.Add(this.btnConnect);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(998, 51);
            this.panel1.TabIndex = 1;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(301, 15);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(39, 14);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "label1";
            // 
            // btnProperties
            // 
            this.btnProperties.Image = ((System.Drawing.Image)(resources.GetObject("btnProperties.Image")));
            this.btnProperties.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProperties.Location = new System.Drawing.Point(196, 14);
            this.btnProperties.Name = "btnProperties";
            this.btnProperties.Size = new System.Drawing.Size(82, 24);
            this.btnProperties.TabIndex = 1;
            this.btnProperties.Text = " Properties";
            this.btnProperties.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnProperties.UseVisualStyleBackColor = true;
            this.btnProperties.Click += new System.EventHandler(this.btnProperties_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(12, 12);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(173, 27);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect to an RS Instance";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pnlObjectexplorerdetails);
            this.panel2.Controls.Add(this.pnlObjectexplorer);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 51);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(998, 521);
            this.panel2.TabIndex = 2;
            // 
            // pnlObjectexplorerdetails
            // 
            this.pnlObjectexplorerdetails.Controls.Add(this.pnlgridview);
            this.pnlObjectexplorerdetails.Controls.Add(this.pnlFormbuttons);
            this.pnlObjectexplorerdetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlObjectexplorerdetails.Location = new System.Drawing.Point(313, 0);
            this.pnlObjectexplorerdetails.Name = "pnlObjectexplorerdetails";
            this.pnlObjectexplorerdetails.Size = new System.Drawing.Size(685, 521);
            this.pnlObjectexplorerdetails.TabIndex = 8;
            // 
            // pnlgridview
            // 
            this.pnlgridview.Controls.Add(this.lvChildren);
            this.pnlgridview.Controls.Add(this.lblReportItemDetails);
            this.pnlgridview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlgridview.Location = new System.Drawing.Point(0, 0);
            this.pnlgridview.Name = "pnlgridview";
            this.pnlgridview.Size = new System.Drawing.Size(685, 443);
            this.pnlgridview.TabIndex = 1;
            // 
            // lvChildren
            // 
            this.lvChildren.AutoArrange = false;
            this.lvChildren.Dock = System.Windows.Forms.DockStyle.Top;
            this.lvChildren.Location = new System.Drawing.Point(0, 13);
            this.lvChildren.MultiSelect = false;
            this.lvChildren.Name = "lvChildren";
            this.lvChildren.ShowGroups = false;
            this.lvChildren.Size = new System.Drawing.Size(685, 188);
            this.lvChildren.TabIndex = 7;
            this.lvChildren.UseCompatibleStateImageBehavior = false;
            this.lvChildren.View = System.Windows.Forms.View.Details;
            this.lvChildren.DoubleClick += new System.EventHandler(this.lvChildren_DoubleClick);
            this.lvChildren.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvChildren_MouseUp);
            // 
            // lblReportItemDetails
            // 
            this.lblReportItemDetails.AutoSize = true;
            this.lblReportItemDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblReportItemDetails.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblReportItemDetails.Location = new System.Drawing.Point(0, 0);
            this.lblReportItemDetails.Name = "lblReportItemDetails";
            this.lblReportItemDetails.Size = new System.Drawing.Size(0, 13);
            this.lblReportItemDetails.TabIndex = 6;
            this.lblReportItemDetails.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlFormbuttons
            // 
            this.pnlFormbuttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFormbuttons.Location = new System.Drawing.Point(0, 443);
            this.pnlFormbuttons.Name = "pnlFormbuttons";
            this.pnlFormbuttons.Size = new System.Drawing.Size(685, 78);
            this.pnlFormbuttons.TabIndex = 0;
            // 
            // pnlObjectexplorer
            // 
            this.pnlObjectexplorer.Controls.Add(this.tvReportitems);
            this.pnlObjectexplorer.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlObjectexplorer.Location = new System.Drawing.Point(0, 0);
            this.pnlObjectexplorer.Name = "pnlObjectexplorer";
            this.pnlObjectexplorer.Size = new System.Drawing.Size(313, 521);
            this.pnlObjectexplorer.TabIndex = 7;
            // 
            // tvReportitems
            // 
            this.tvReportitems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvReportitems.Location = new System.Drawing.Point(0, 0);
            this.tvReportitems.Name = "tvReportitems";
            this.tvReportitems.PathSeparator = "/";
            this.tvReportitems.Size = new System.Drawing.Size(313, 521);
            this.tvReportitems.TabIndex = 0;
            this.tvReportitems.DoubleClick += new System.EventHandler(this.tvReportitems_DoubleClick);
            this.tvReportitems.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tvReportitems_MouseUp);
            this.tvReportitems.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvReportitems_AfterSelect);
            // 
            // smallImageList
            // 
            this.smallImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("smallImageList.ImageStream")));
            this.smallImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.smallImageList.Images.SetKeyName(0, "imgDatasource");
            this.smallImageList.Images.SetKeyName(1, "imgFolder");
            this.smallImageList.Images.SetKeyName(2, "ImgLinked.gif");
            this.smallImageList.Images.SetKeyName(3, "imgModel");
            this.smallImageList.Images.SetKeyName(4, "16doc.gif");
            this.smallImageList.Images.SetKeyName(5, "imgFile");
            // 
            // PermissionsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 572);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PermissionsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Report Permissions";
            this.mnuTextFile.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.pnlObjectexplorerdetails.ResumeLayout(false);
            this.pnlgridview.ResumeLayout(false);
            this.pnlgridview.PerformLayout();
            this.pnlObjectexplorer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip mnuTextFile;
        private System.Windows.Forms.ToolStripMenuItem mnuUserPermissions;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlObjectexplorerdetails;
        private System.Windows.Forms.Panel pnlgridview;
        private System.Windows.Forms.ListView lvChildren;
        private System.Windows.Forms.Label lblReportItemDetails;
        private System.Windows.Forms.Panel pnlFormbuttons;
        private System.Windows.Forms.Panel pnlObjectexplorer;
        private System.Windows.Forms.TreeView tvReportitems;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ImageList smallImageList;
        private System.Windows.Forms.Button btnProperties;
        private System.Windows.Forms.Label lblMessage;
    }
}