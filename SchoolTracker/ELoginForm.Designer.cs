using System.Windows.Forms;

namespace SchoolTracker
{
    partial class ELoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ELoginForm));
            this.userLabel = new MaterialSkin.Controls.MaterialLabel();
            this.studentCard = new MaterialSkin.Controls.MaterialCard();
            this.resetBtn = new MaterialSkin.Controls.MaterialButton();
            this.signInBtn = new MaterialSkin.Controls.MaterialButton();
            this.passWarning = new MaterialSkin.Controls.MaterialButton();
            this.bdWarning = new MaterialSkin.Controls.MaterialButton();
            this.snWarning = new MaterialSkin.Controls.MaterialButton();
            this.bYearComBox = new MaterialSkin.Controls.MaterialComboBox();
            this.bDayComBox = new MaterialSkin.Controls.MaterialComboBox();
            this.bMonthComBox = new MaterialSkin.Controls.MaterialComboBox();
            this.facPassBox = new MaterialSkin.Controls.MaterialTextBox();
            this.facNumBox = new MaterialSkin.Controls.MaterialTextBox();
            this.forgotPassBtn = new MaterialSkin.Controls.MaterialButton();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.backBtn = new MaterialSkin.Controls.MaterialButton();
            this.studentCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.Depth = 0;
            this.userLabel.Font = new System.Drawing.Font("Roboto", 34F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.userLabel.FontType = MaterialSkin.MaterialSkinManager.fontType.H4;
            this.userLabel.Location = new System.Drawing.Point(60, 238);
            this.userLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(306, 41);
            this.userLabel.TabIndex = 4;
            this.userLabel.Text = "PUP Enrollment Hub";
            this.userLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // studentCard
            // 
            this.studentCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.studentCard.Controls.Add(this.resetBtn);
            this.studentCard.Controls.Add(this.signInBtn);
            this.studentCard.Controls.Add(this.passWarning);
            this.studentCard.Controls.Add(this.bdWarning);
            this.studentCard.Controls.Add(this.snWarning);
            this.studentCard.Controls.Add(this.bYearComBox);
            this.studentCard.Controls.Add(this.bDayComBox);
            this.studentCard.Controls.Add(this.bMonthComBox);
            this.studentCard.Controls.Add(this.facPassBox);
            this.studentCard.Controls.Add(this.facNumBox);
            this.studentCard.Depth = 0;
            this.studentCard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.studentCard.Location = new System.Drawing.Point(7, 324);
            this.studentCard.Margin = new System.Windows.Forms.Padding(12);
            this.studentCard.MouseState = MaterialSkin.MouseState.HOVER;
            this.studentCard.Name = "studentCard";
            this.studentCard.Padding = new System.Windows.Forms.Padding(12);
            this.studentCard.Size = new System.Drawing.Size(412, 274);
            this.studentCard.TabIndex = 1;
            // 
            // resetBtn
            // 
            this.resetBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.resetBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.resetBtn.Depth = 0;
            this.resetBtn.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.resetBtn.HighEmphasis = true;
            this.resetBtn.Icon = null;
            this.resetBtn.Location = new System.Drawing.Point(214, 225);
            this.resetBtn.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.resetBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.resetBtn.Name = "resetBtn";
            this.resetBtn.NoAccentTextColor = System.Drawing.Color.Empty;
            this.resetBtn.Size = new System.Drawing.Size(65, 36);
            this.resetBtn.TabIndex = 6;
            this.resetBtn.Text = "Reset";
            this.resetBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.resetBtn.UseAccentColor = true;
            this.resetBtn.UseMnemonic = false;
            this.resetBtn.UseVisualStyleBackColor = false;
            this.resetBtn.Click += new System.EventHandler(this.resetBtn_Click);
            // 
            // signInBtn
            // 
            this.signInBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.signInBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.signInBtn.Depth = 0;
            this.signInBtn.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.signInBtn.HighEmphasis = true;
            this.signInBtn.Icon = null;
            this.signInBtn.Location = new System.Drawing.Point(133, 225);
            this.signInBtn.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.signInBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.signInBtn.Name = "signInBtn";
            this.signInBtn.NoAccentTextColor = System.Drawing.Color.Empty;
            this.signInBtn.Size = new System.Drawing.Size(73, 36);
            this.signInBtn.TabIndex = 5;
            this.signInBtn.Text = "Sign in";
            this.signInBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.signInBtn.UseAccentColor = false;
            this.signInBtn.UseMnemonic = false;
            this.signInBtn.UseVisualStyleBackColor = false;
            this.signInBtn.Click += new System.EventHandler(this.signInBtn_Click);
            // 
            // passWarning
            // 
            this.passWarning.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.passWarning.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.passWarning.Depth = 0;
            this.passWarning.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.passWarning.HighEmphasis = true;
            this.passWarning.Icon = null;
            this.passWarning.Location = new System.Drawing.Point(19, 210);
            this.passWarning.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.passWarning.MouseState = MaterialSkin.MouseState.HOVER;
            this.passWarning.Name = "passWarning";
            this.passWarning.NoAccentTextColor = System.Drawing.Color.Empty;
            this.passWarning.Size = new System.Drawing.Size(266, 36);
            this.passWarning.TabIndex = 10;
            this.passWarning.Text = "Please provide your password.";
            this.passWarning.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.passWarning.UseAccentColor = true;
            this.passWarning.UseMnemonic = false;
            this.passWarning.UseVisualStyleBackColor = false;
            this.passWarning.Visible = false;
            // 
            // bdWarning
            // 
            this.bdWarning.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bdWarning.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.bdWarning.Depth = 0;
            this.bdWarning.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.bdWarning.HighEmphasis = true;
            this.bdWarning.Icon = null;
            this.bdWarning.Location = new System.Drawing.Point(18, 142);
            this.bdWarning.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.bdWarning.MouseState = MaterialSkin.MouseState.HOVER;
            this.bdWarning.Name = "bdWarning";
            this.bdWarning.NoAccentTextColor = System.Drawing.Color.Empty;
            this.bdWarning.Size = new System.Drawing.Size(279, 36);
            this.bdWarning.TabIndex = 9;
            this.bdWarning.Text = "Please select an item in the list.";
            this.bdWarning.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.bdWarning.UseAccentColor = true;
            this.bdWarning.UseMnemonic = false;
            this.bdWarning.UseVisualStyleBackColor = false;
            this.bdWarning.Visible = false;
            // 
            // snWarning
            // 
            this.snWarning.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.snWarning.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.snWarning.Depth = 0;
            this.snWarning.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.snWarning.HighEmphasis = true;
            this.snWarning.Icon = null;
            this.snWarning.Location = new System.Drawing.Point(18, 76);
            this.snWarning.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.snWarning.MouseState = MaterialSkin.MouseState.HOVER;
            this.snWarning.Name = "snWarning";
            this.snWarning.NoAccentTextColor = System.Drawing.Color.Empty;
            this.snWarning.Size = new System.Drawing.Size(314, 36);
            this.snWarning.TabIndex = 9;
            this.snWarning.Text = "Please provide your enrolee number.";
            this.snWarning.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.snWarning.UseAccentColor = true;
            this.snWarning.UseMnemonic = false;
            this.snWarning.UseVisualStyleBackColor = false;
            this.snWarning.Visible = false;
            // 
            // bYearComBox
            // 
            this.bYearComBox.AutoResize = false;
            this.bYearComBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.bYearComBox.Depth = 0;
            this.bYearComBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.bYearComBox.DropDownHeight = 174;
            this.bYearComBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bYearComBox.DropDownWidth = 121;
            this.bYearComBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.bYearComBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.bYearComBox.FormattingEnabled = true;
            this.bYearComBox.IntegralHeight = false;
            this.bYearComBox.ItemHeight = 43;
            this.bYearComBox.Items.AddRange(new object[] {
            "Birth Year",
            "2013",
            "2012",
            "2011",
            "2010",
            "2009",
            "2008",
            "2007",
            "2006",
            "2005",
            "2004",
            "2003",
            "2002",
            "2001",
            "2000",
            "1999",
            "1998",
            "1997",
            "1996",
            "1995",
            "1994",
            "1993",
            "1992",
            "1991",
            "1990"});
            this.bYearComBox.Location = new System.Drawing.Point(283, 89);
            this.bYearComBox.MaxDropDownItems = 4;
            this.bYearComBox.MouseState = MaterialSkin.MouseState.OUT;
            this.bYearComBox.Name = "bYearComBox";
            this.bYearComBox.Size = new System.Drawing.Size(119, 49);
            this.bYearComBox.StartIndex = 0;
            this.bYearComBox.TabIndex = 3;
            // 
            // bDayComBox
            // 
            this.bDayComBox.AutoResize = false;
            this.bDayComBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.bDayComBox.Depth = 0;
            this.bDayComBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.bDayComBox.DropDownHeight = 174;
            this.bDayComBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bDayComBox.DropDownWidth = 121;
            this.bDayComBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.bDayComBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.bDayComBox.FormattingEnabled = true;
            this.bDayComBox.IntegralHeight = false;
            this.bDayComBox.ItemHeight = 43;
            this.bDayComBox.Items.AddRange(new object[] {
            "Birth Day",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31"});
            this.bDayComBox.Location = new System.Drawing.Point(157, 89);
            this.bDayComBox.MaxDropDownItems = 4;
            this.bDayComBox.MouseState = MaterialSkin.MouseState.OUT;
            this.bDayComBox.Name = "bDayComBox";
            this.bDayComBox.Size = new System.Drawing.Size(122, 49);
            this.bDayComBox.StartIndex = 0;
            this.bDayComBox.TabIndex = 2;
            // 
            // bMonthComBox
            // 
            this.bMonthComBox.AutoResize = false;
            this.bMonthComBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.bMonthComBox.Depth = 0;
            this.bMonthComBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.bMonthComBox.DropDownHeight = 174;
            this.bMonthComBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bMonthComBox.DropDownWidth = 121;
            this.bMonthComBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.bMonthComBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.bMonthComBox.FormattingEnabled = true;
            this.bMonthComBox.IntegralHeight = false;
            this.bMonthComBox.ItemHeight = 43;
            this.bMonthComBox.Items.AddRange(new object[] {
            "Birth Month",
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"});
            this.bMonthComBox.Location = new System.Drawing.Point(14, 89);
            this.bMonthComBox.MaxDropDownItems = 4;
            this.bMonthComBox.MouseState = MaterialSkin.MouseState.OUT;
            this.bMonthComBox.Name = "bMonthComBox";
            this.bMonthComBox.Size = new System.Drawing.Size(135, 49);
            this.bMonthComBox.StartIndex = 0;
            this.bMonthComBox.TabIndex = 1;
            // 
            // facPassBox
            // 
            this.facPassBox.AnimateReadOnly = false;
            this.facPassBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.facPassBox.Depth = 0;
            this.facPassBox.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.facPassBox.Hint = "Password";
            this.facPassBox.LeadingIcon = null;
            this.facPassBox.Location = new System.Drawing.Point(14, 156);
            this.facPassBox.MaxLength = 50;
            this.facPassBox.MouseState = MaterialSkin.MouseState.OUT;
            this.facPassBox.Multiline = false;
            this.facPassBox.Name = "facPassBox";
            this.facPassBox.Password = true;
            this.facPassBox.Size = new System.Drawing.Size(388, 50);
            this.facPassBox.TabIndex = 4;
            this.facPassBox.Text = "";
            this.facPassBox.TrailingIcon = global::SchoolTracker.Properties.Resources.hide;
            this.facPassBox.TrailingIconClick += new System.EventHandler(this.facPassBox_TrailingIconClick);
            // 
            // facNumBox
            // 
            this.facNumBox.AnimateReadOnly = false;
            this.facNumBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.facNumBox.Depth = 0;
            this.facNumBox.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.facNumBox.Hint = "Enrolee Number";
            this.facNumBox.LeadingIcon = null;
            this.facNumBox.Location = new System.Drawing.Point(14, 24);
            this.facNumBox.MaxLength = 50;
            this.facNumBox.MouseState = MaterialSkin.MouseState.OUT;
            this.facNumBox.Multiline = false;
            this.facNumBox.Name = "facNumBox";
            this.facNumBox.Size = new System.Drawing.Size(388, 50);
            this.facNumBox.TabIndex = 0;
            this.facNumBox.Text = "";
            this.facNumBox.TrailingIcon = global::SchoolTracker.Properties.Resources.user;
            // 
            // forgotPassBtn
            // 
            this.forgotPassBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.forgotPassBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.forgotPassBtn.Depth = 0;
            this.forgotPassBtn.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.forgotPassBtn.HighEmphasis = true;
            this.forgotPassBtn.Icon = null;
            this.forgotPassBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.forgotPassBtn.Location = new System.Drawing.Point(116, 606);
            this.forgotPassBtn.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.forgotPassBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.forgotPassBtn.Name = "forgotPassBtn";
            this.forgotPassBtn.NoAccentTextColor = System.Drawing.Color.Empty;
            this.forgotPassBtn.Size = new System.Drawing.Size(194, 36);
            this.forgotPassBtn.TabIndex = 3;
            this.forgotPassBtn.Text = "I forgot my password";
            this.forgotPassBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.forgotPassBtn.UseAccentColor = true;
            this.forgotPassBtn.UseMnemonic = false;
            this.forgotPassBtn.UseVisualStyleBackColor = false;
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel3.FontType = MaterialSkin.MaterialSkinManager.fontType.Subtitle1;
            this.materialLabel3.Location = new System.Drawing.Point(115, 283);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(197, 19);
            this.materialLabel3.TabIndex = 7;
            this.materialLabel3.Text = "Sign in to start your session";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SchoolTracker.Properties.Resources.pngkey_com_phillies_logo_png_528919;
            this.pictureBox1.Location = new System.Drawing.Point(143, 91);
            this.pictureBox1.MaximumSize = new System.Drawing.Size(140, 140);
            this.pictureBox1.MinimumSize = new System.Drawing.Size(140, 140);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(140, 140);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // backBtn
            // 
            this.backBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.backBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.backBtn.Depth = 0;
            this.backBtn.HighEmphasis = true;
            this.backBtn.Icon = global::SchoolTracker.Properties.Resources.back_button;
            this.backBtn.Location = new System.Drawing.Point(7, 31);
            this.backBtn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.backBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.backBtn.Name = "backBtn";
            this.backBtn.NoAccentTextColor = System.Drawing.Color.Empty;
            this.backBtn.Size = new System.Drawing.Size(87, 36);
            this.backBtn.TabIndex = 8;
            this.backBtn.Text = "Back";
            this.backBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.backBtn.UseAccentColor = false;
            this.backBtn.UseVisualStyleBackColor = true;
            this.backBtn.Click += new System.EventHandler(this.backBtn_Click);
            // 
            // ELoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 650);
            this.Controls.Add(this.backBtn);
            this.Controls.Add(this.materialLabel3);
            this.Controls.Add(this.forgotPassBtn);
            this.Controls.Add(this.studentCard);
            this.Controls.Add(this.userLabel);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.FormStyle = MaterialSkin.Controls.MaterialForm.FormStyles.ActionBar_None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(427, 650);
            this.MinimumSize = new System.Drawing.Size(427, 650);
            this.Name = "ELoginForm";
            this.Padding = new System.Windows.Forms.Padding(3, 21, 3, 3);
            this.Sizable = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUP Enrollment Hub";
            this.studentCard.ResumeLayout(false);
            this.studentCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox pictureBox1;
        private MaterialSkin.Controls.MaterialLabel userLabel;
        private MaterialSkin.Controls.MaterialCard studentCard;
        private MaterialSkin.Controls.MaterialTextBox facPassBox;
        private MaterialSkin.Controls.MaterialTextBox facNumBox;
        private MaterialSkin.Controls.MaterialComboBox bMonthComBox;
        private MaterialSkin.Controls.MaterialComboBox bDayComBox;
        private MaterialSkin.Controls.MaterialComboBox bYearComBox;
        private MaterialSkin.Controls.MaterialButton signInBtn;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialButton resetBtn;
        private MaterialSkin.Controls.MaterialButton forgotPassBtn;
        private MaterialSkin.Controls.MaterialButton snWarning;
        private MaterialSkin.Controls.MaterialButton bdWarning;
        private MaterialSkin.Controls.MaterialButton passWarning;
        private MaterialSkin.Controls.MaterialButton backBtn;
    }
}