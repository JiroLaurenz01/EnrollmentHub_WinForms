namespace SchoolTracker
{
    partial class FrontForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrontForm));
            this.userLabel = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.materialCard1 = new MaterialSkin.Controls.MaterialCard();
            this.enroleeBtn = new MaterialSkin.Controls.MaterialButton();
            this.facultyBtn = new MaterialSkin.Controls.MaterialButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.materialCard2 = new MaterialSkin.Controls.MaterialCard();
            this.WebsiteVisitBtn = new MaterialSkin.Controls.MaterialButton();
            this.materialCard1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.materialCard2.SuspendLayout();
            this.SuspendLayout();
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.Depth = 0;
            this.userLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.userLabel.FontType = MaterialSkin.MaterialSkinManager.fontType.Button;
            this.userLabel.Location = new System.Drawing.Point(19, 156);
            this.userLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(258, 17);
            this.userLabel.TabIndex = 5;
            this.userLabel.Text = "Polytechnic University of the Philippines";
            this.userLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.FontType = MaterialSkin.MaterialSkinManager.fontType.Caption;
            this.materialLabel1.Location = new System.Drawing.Point(69, 176);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(153, 14);
            this.materialLabel1.TabIndex = 6;
            this.materialLabel1.Text = "Student Information Tracker";
            this.materialLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // materialCard1
            // 
            this.materialCard1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialCard1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.materialCard1.Controls.Add(this.enroleeBtn);
            this.materialCard1.Controls.Add(this.materialLabel1);
            this.materialCard1.Controls.Add(this.facultyBtn);
            this.materialCard1.Controls.Add(this.userLabel);
            this.materialCard1.Controls.Add(this.pictureBox1);
            this.materialCard1.Depth = 0;
            this.materialCard1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialCard1.Location = new System.Drawing.Point(17, 41);
            this.materialCard1.Margin = new System.Windows.Forms.Padding(14);
            this.materialCard1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCard1.Name = "materialCard1";
            this.materialCard1.Padding = new System.Windows.Forms.Padding(14);
            this.materialCard1.Size = new System.Drawing.Size(298, 282);
            this.materialCard1.TabIndex = 7;
            // 
            // enroleeBtn
            // 
            this.enroleeBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.enroleeBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.enroleeBtn.Depth = 0;
            this.enroleeBtn.HighEmphasis = true;
            this.enroleeBtn.Icon = null;
            this.enroleeBtn.Location = new System.Drawing.Point(50, 227);
            this.enroleeBtn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.enroleeBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.enroleeBtn.Name = "enroleeBtn";
            this.enroleeBtn.NoAccentTextColor = System.Drawing.Color.Empty;
            this.enroleeBtn.Size = new System.Drawing.Size(85, 36);
            this.enroleeBtn.TabIndex = 1;
            this.enroleeBtn.Text = "ENROLEE";
            this.enroleeBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.enroleeBtn.UseAccentColor = false;
            this.enroleeBtn.UseVisualStyleBackColor = true;
            this.enroleeBtn.Click += new System.EventHandler(this.enroleeBtn_Click);
            // 
            // facultyBtn
            // 
            this.facultyBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.facultyBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.facultyBtn.Depth = 0;
            this.facultyBtn.HighEmphasis = true;
            this.facultyBtn.Icon = null;
            this.facultyBtn.Location = new System.Drawing.Point(160, 227);
            this.facultyBtn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.facultyBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.facultyBtn.Name = "facultyBtn";
            this.facultyBtn.NoAccentTextColor = System.Drawing.Color.Empty;
            this.facultyBtn.Size = new System.Drawing.Size(84, 36);
            this.facultyBtn.TabIndex = 0;
            this.facultyBtn.Text = "FACULTY";
            this.facultyBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.facultyBtn.UseAccentColor = false;
            this.facultyBtn.UseVisualStyleBackColor = true;
            this.facultyBtn.Click += new System.EventHandler(this.facultyBtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SchoolTracker.Properties.Resources.pngkey_com_phillies_logo_png_528919;
            this.pictureBox1.Location = new System.Drawing.Point(87, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 120);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // materialCard2
            // 
            this.materialCard2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialCard2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.materialCard2.Controls.Add(this.WebsiteVisitBtn);
            this.materialCard2.Depth = 0;
            this.materialCard2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.materialCard2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialCard2.Location = new System.Drawing.Point(3, 340);
            this.materialCard2.Margin = new System.Windows.Forms.Padding(14);
            this.materialCard2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCard2.Name = "materialCard2";
            this.materialCard2.Padding = new System.Windows.Forms.Padding(14);
            this.materialCard2.Size = new System.Drawing.Size(326, 36);
            this.materialCard2.TabIndex = 8;
            // 
            // WebsiteVisitBtn
            // 
            this.WebsiteVisitBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.WebsiteVisitBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.WebsiteVisitBtn.Depth = 0;
            this.WebsiteVisitBtn.HighEmphasis = true;
            this.WebsiteVisitBtn.Icon = null;
            this.WebsiteVisitBtn.Location = new System.Drawing.Point(84, 0);
            this.WebsiteVisitBtn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.WebsiteVisitBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.WebsiteVisitBtn.Name = "WebsiteVisitBtn";
            this.WebsiteVisitBtn.NoAccentTextColor = System.Drawing.Color.Empty;
            this.WebsiteVisitBtn.Size = new System.Drawing.Size(154, 36);
            this.WebsiteVisitBtn.TabIndex = 0;
            this.WebsiteVisitBtn.Text = "visit our website";
            this.WebsiteVisitBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.WebsiteVisitBtn.UseAccentColor = true;
            this.WebsiteVisitBtn.UseVisualStyleBackColor = true;
            // 
            // FrontForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 379);
            this.Controls.Add(this.materialCard2);
            this.Controls.Add(this.materialCard1);
            this.FormStyle = MaterialSkin.Controls.MaterialForm.FormStyles.ActionBar_None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrontForm";
            this.Padding = new System.Windows.Forms.Padding(3, 24, 3, 3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "School Tracker";
            this.TopMost = true;
            this.materialCard1.ResumeLayout(false);
            this.materialCard1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.materialCard2.ResumeLayout(false);
            this.materialCard2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private MaterialSkin.Controls.MaterialLabel userLabel;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialCard materialCard1;
        private MaterialSkin.Controls.MaterialButton enroleeBtn;
        private MaterialSkin.Controls.MaterialButton facultyBtn;
        private MaterialSkin.Controls.MaterialCard materialCard2;
        private MaterialSkin.Controls.MaterialButton WebsiteVisitBtn;
    }
}

