namespace RSPermissionsCS
{
    partial class ItemPropertiesDialog
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
            this.pnlgridview = new System.Windows.Forms.Panel();
            this.userpermissionsGridview = new System.Windows.Forms.DataGridView();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.adduserButton = new System.Windows.Forms.Button();
            this.removeuserButton = new System.Windows.Forms.Button();
            this.pnlgridview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userpermissionsGridview)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlgridview
            // 
            this.pnlgridview.Controls.Add(this.userpermissionsGridview);
            this.pnlgridview.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlgridview.Location = new System.Drawing.Point(0, 0);
            this.pnlgridview.Name = "pnlgridview";
            this.pnlgridview.Size = new System.Drawing.Size(782, 359);
            this.pnlgridview.TabIndex = 2;
            // 
            // userpermissionsGridview
            // 
            this.userpermissionsGridview.AllowUserToAddRows = false;
            this.userpermissionsGridview.AllowUserToDeleteRows = false;
            this.userpermissionsGridview.AllowUserToResizeColumns = false;
            this.userpermissionsGridview.AllowUserToResizeRows = false;
            this.userpermissionsGridview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.userpermissionsGridview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.userpermissionsGridview.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.userpermissionsGridview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.userpermissionsGridview.Location = new System.Drawing.Point(0, 0);
            this.userpermissionsGridview.MultiSelect = false;
            this.userpermissionsGridview.Name = "userpermissionsGridview";
            this.userpermissionsGridview.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.userpermissionsGridview.RowHeadersVisible = false;
            this.userpermissionsGridview.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.userpermissionsGridview.Size = new System.Drawing.Size(782, 359);
            this.userpermissionsGridview.TabIndex = 0;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(686, 16);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(86, 30);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(558, 16);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(105, 30);
            this.okButton.TabIndex = 6;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.okButton);
            this.panel2.Controls.Add(this.cancelButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 466);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(782, 58);
            this.panel2.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.adduserButton);
            this.panel1.Controls.Add(this.removeuserButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 406);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(782, 60);
            this.panel1.TabIndex = 5;
            // 
            // adduserButton
            // 
            this.adduserButton.Location = new System.Drawing.Point(558, 16);
            this.adduserButton.Name = "adduserButton";
            this.adduserButton.Size = new System.Drawing.Size(105, 30);
            this.adduserButton.TabIndex = 6;
            this.adduserButton.Text = "Add Group/User..";
            this.adduserButton.UseVisualStyleBackColor = true;
            this.adduserButton.Click += new System.EventHandler(this.adduserButton_Click);
            // 
            // removeuserButton
            // 
            this.removeuserButton.Location = new System.Drawing.Point(686, 16);
            this.removeuserButton.Name = "removeuserButton";
            this.removeuserButton.Size = new System.Drawing.Size(86, 30);
            this.removeuserButton.TabIndex = 5;
            this.removeuserButton.Text = "Remove";
            this.removeuserButton.UseVisualStyleBackColor = true;
            this.removeuserButton.Click += new System.EventHandler(this.removeuserButton_Click);
            // 
            // ItemPropertiesDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(782, 524);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlgridview);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ItemPropertiesDialog";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Permissions";
            this.pnlgridview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.userpermissionsGridview)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlgridview;
        private System.Windows.Forms.DataGridView userpermissionsGridview;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button adduserButton;
        private System.Windows.Forms.Button removeuserButton;

    }
}