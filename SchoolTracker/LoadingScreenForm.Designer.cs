namespace SchoolTracker
{
    partial class LoadingScreenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadingScreenForm));
            this.loadingPercent = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.progressBar = new Guna.UI2.WinForms.Guna2ProgressBar();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // loadingPercent
            // 
            this.loadingPercent.BackColor = System.Drawing.SystemColors.Desktop;
            this.loadingPercent.Enabled = false;
            this.loadingPercent.Font = new System.Drawing.Font("Bahnschrift Light", 18F);
            this.loadingPercent.ForeColor = System.Drawing.Color.White;
            this.loadingPercent.Location = new System.Drawing.Point(337, 272);
            this.loadingPercent.Name = "loadingPercent";
            this.loadingPercent.Size = new System.Drawing.Size(38, 31);
            this.loadingPercent.TabIndex = 10;
            this.loadingPercent.Text = "0 %";
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.SystemColors.Desktop;
            this.guna2HtmlLabel2.Enabled = false;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Bahnschrift Light", 19.1F);
            this.guna2HtmlLabel2.ForeColor = System.Drawing.Color.White;
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(198, 173);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(480, 32);
            this.guna2HtmlLabel2.TabIndex = 9;
            this.guna2HtmlLabel2.Text = "POWERED BY JIRO LAURENZ SAMIA AGAD";
            // 
            // progressBar
            // 
            this.progressBar.BackColor = System.Drawing.Color.Transparent;
            this.progressBar.BorderColor = System.Drawing.Color.Transparent;
            this.progressBar.Location = new System.Drawing.Point(15, 309);
            this.progressBar.Name = "progressBar";
            this.progressBar.ProgressColor = System.Drawing.Color.Maroon;
            this.progressBar.ProgressColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.progressBar.ShadowDecoration.BorderRadius = 1;
            this.progressBar.Size = new System.Drawing.Size(683, 10);
            this.progressBar.TabIndex = 8;
            this.progressBar.Text = "guna2ProgressBar1";
            this.progressBar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.SystemColors.Desktop;
            this.guna2HtmlLabel1.Enabled = false;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Bahnschrift Condensed", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.ForeColor = System.Drawing.Color.White;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(198, 88);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(497, 79);
            this.guna2HtmlLabel1.TabIndex = 7;
            this.guna2HtmlLabel1.Text = "PUP - ENROLLMENT HUB";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SchoolTracker.Properties.Resources.pngkey_com_phillies_logo_png_528919;
            this.pictureBox1.Location = new System.Drawing.Point(17, 64);
            this.pictureBox1.MaximumSize = new System.Drawing.Size(175, 175);
            this.pictureBox1.MinimumSize = new System.Drawing.Size(175, 175);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(175, 175);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            // 
            // LoadingScreenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(713, 341);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.loadingPercent);
            this.Controls.Add(this.guna2HtmlLabel2);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.guna2HtmlLabel1);
            this.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoadingScreenForm";
            this.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoadingScreenForm";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.SystemColors.Desktop;
            this.Load += new System.EventHandler(this.LoadingScreenForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2HtmlLabel loadingPercent;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2ProgressBar progressBar;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
    }
}