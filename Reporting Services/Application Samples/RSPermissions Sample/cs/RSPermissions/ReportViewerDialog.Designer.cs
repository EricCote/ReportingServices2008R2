namespace RSPermissionsCS
{
    partial class ReportViewerDialog
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
            this.rptWebbrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // rptWebbrowser
            // 
            this.rptWebbrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rptWebbrowser.Location = new System.Drawing.Point(0, 0);
            this.rptWebbrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.rptWebbrowser.Name = "rptWebbrowser";
            this.rptWebbrowser.Size = new System.Drawing.Size(840, 591);
            this.rptWebbrowser.TabIndex = 0;
            // 
            // ReportViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 591);
            this.Controls.Add(this.rptWebbrowser);
            this.Name = "ReportViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReportViewer";
            this.Load += new System.EventHandler(this.ReportViewer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser rptWebbrowser;

    }
}