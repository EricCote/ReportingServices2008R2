namespace Microsoft.Samples.ReportingServices.RSExplorer
{
    partial class CopyProgress
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Windows.Forms.Control.set_Text(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Windows.Forms.Control.set_Text(System.String)")]
        private void InitializeComponent()
        {
            this.itemNameLabel = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
// 
// itemNameLabel
// 
            this.itemNameLabel.Location = new System.Drawing.Point(13, 13);
            this.itemNameLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.itemNameLabel.Name = "itemNameLabel";
            this.itemNameLabel.Size = new System.Drawing.Size(272, 23);
            this.itemNameLabel.TabIndex = 3;
            this.itemNameLabel.Text = "Item:";
// 
// progressBar1
// 
            this.progressBar1.Location = new System.Drawing.Point(13, 37);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(272, 23);
            this.progressBar1.TabIndex = 2;
// 
// CopyProgress
// 
            this.ClientSize = new System.Drawing.Size(299, 76);
            this.Controls.Add(this.itemNameLabel);
            this.Controls.Add(this.progressBar1);
            this.Name = "CopyProgress";
            this.Text = "Copy Progress";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label itemNameLabel;
        public System.Windows.Forms.ProgressBar progressBar1;
    }
}