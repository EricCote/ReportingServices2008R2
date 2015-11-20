namespace RSPermissionsCS
{
    partial class ConnectToServerDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectToServerDialog));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.reportserverurlTextbox = new System.Windows.Forms.TextBox();
            this.reportserverurlButton = new System.Windows.Forms.Button();
            this.reportserverurlLabel = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(505, 126);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.reportserverurlTextbox);
            this.panel2.Controls.Add(this.reportserverurlButton);
            this.panel2.Controls.Add(this.reportserverurlLabel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 126);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(505, 22);
            this.panel2.TabIndex = 1;
            // 
            // reportserverurlTextbox
            // 
            this.reportserverurlTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportserverurlTextbox.Location = new System.Drawing.Point(87, 0);
            this.reportserverurlTextbox.Name = "reportserverurlTextbox";
            this.reportserverurlTextbox.Size = new System.Drawing.Size(343, 20);
            this.reportserverurlTextbox.TabIndex = 9;
            // 
            // reportserverurlButton
            // 
            this.reportserverurlButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.reportserverurlButton.Location = new System.Drawing.Point(430, 0);
            this.reportserverurlButton.Name = "reportserverurlButton";
            this.reportserverurlButton.Size = new System.Drawing.Size(75, 22);
            this.reportserverurlButton.TabIndex = 8;
            this.reportserverurlButton.Text = "Go";
            this.reportserverurlButton.UseVisualStyleBackColor = true;
            this.reportserverurlButton.Click += new System.EventHandler(this.reportserverurlButton_Click);
            // 
            // reportserverurlLabel
            // 
            this.reportserverurlLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.reportserverurlLabel.Location = new System.Drawing.Point(0, 0);
            this.reportserverurlLabel.Name = "reportserverurlLabel";
            this.reportserverurlLabel.Size = new System.Drawing.Size(87, 22);
            this.reportserverurlLabel.TabIndex = 7;
            this.reportserverurlLabel.Text = "Report Server url";
            this.reportserverurlLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ConnectToServerDialog
            // 
            this.AcceptButton = this.reportserverurlButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 148);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectToServerDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connect To Server";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox reportserverurlTextbox;
        private System.Windows.Forms.Button reportserverurlButton;
        private System.Windows.Forms.Label reportserverurlLabel;


    }
}