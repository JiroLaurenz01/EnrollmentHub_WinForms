using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolTracker
{
    public partial class FLoginForm : MaterialForm
    {
        bool ifShow = false;

        public FLoginForm()
        {
            InitializeComponent();
            var skin = MaterialSkinManager.Instance;
            skin.AddFormToManage(this);
            skin.Theme = MaterialSkinManager.Themes.DARK;
            skin.ColorScheme = new ColorScheme(
                    Primary.Red800,
                    Primary.Red900,
                    Primary.Red500,
                    Accent.Red200,
                    TextShade.WHITE
                );
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            var FrontForm = new FrontForm();
            FrontForm.FormClosed += (s, args) => this.Close();
            FrontForm.Show();
        }

        private void facPassBox_TrailingIconClick(object sender, EventArgs e)
        {
            if (!ifShow)
            {
                facPassBox.TrailingIcon = Properties.Resources.hide;
                facPassBox.Password = true;
                ifShow = true;
            }
            else
            {
                facPassBox.TrailingIcon = Properties.Resources.show;
                facPassBox.Password = false;
                ifShow = false;
            }
        }

        private void signInBtn_Click(object sender, EventArgs e)
        {
            UnvisibleWarning();

            if (IfWarning())
            {
                MessageBox.Show("There are items that require your attention", "PUPSIS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void UnvisibleWarning()
        {
            snWarning.Visible = false;
            bdWarning.Visible = false;
            passWarning.Visible = false;

            bMonthComBox.Location = new Point(bMonthComBox.Location.X, 89);
            bDayComBox.Location = new Point(bDayComBox.Location.X, 89);
            bYearComBox.Location = new Point(bYearComBox.Location.X, 89);
            facPassBox.Location = new Point(facPassBox.Location.X, 156);
            signInBtn.Location = new Point(signInBtn.Location.X, 227);
            resetBtn.Location = new Point(resetBtn.Location.X, 227);

            bdWarning.Location = new Point(bdWarning.Location.X, 142);
            passWarning.Location = new Point(passWarning.Location.X, 206);

            studentCard.Size = new Size(studentCard.Size.Width, 277);
            forgotPassBtn.Location = new Point(forgotPassBtn.Location.X, 608);

            this.Size = new Size(this.Size.Width, 650);
        }

        private Boolean IfWarning()
        {
            bool userNBox, bDayBox, userPBox;

            if (userNBox = (String.IsNullOrEmpty(facNumBox.Text)))
                ChangeLocSize(3);

            if (bDayBox = (bMonthComBox.SelectedIndex == 0 || bDayComBox.SelectedIndex == 0 || bYearComBox.SelectedIndex == 0))
                ChangeLocSize(2);

            if (userPBox = (String.IsNullOrEmpty(facPassBox.Text)))
                ChangeLocSize(1);

            return userNBox || bDayBox || userPBox;
        }

        private void ChangeLocSize(int numBox) //NEVER TOUCH THIS!
        {
            if (numBox == 3)
            {
                snWarning.Visible = true;
                bMonthComBox.Location = new Point(bMonthComBox.Location.X, bMonthComBox.Location.Y + 27);
                bDayComBox.Location = new Point(bDayComBox.Location.X, bDayComBox.Location.Y + 27);
                bYearComBox.Location = new Point(bYearComBox.Location.X, bYearComBox.Location.Y + 27);
                facPassBox.Location = new Point(facPassBox.Location.X, facPassBox.Location.Y + 27);

                bdWarning.Location = new Point(bdWarning.Location.X, bdWarning.Location.Y + 27);
                passWarning.Location = new Point(passWarning.Location.X, passWarning.Location.Y + 28);
            }

            if (numBox == 2)
            {
                bdWarning.Visible = true;
                facPassBox.Location = new Point(facPassBox.Location.X, facPassBox.Location.Y + 27);

                passWarning.Location = new Point(passWarning.Location.X, passWarning.Location.Y + 28);
            }

            if (numBox == 1)
                passWarning.Visible = true;

            signInBtn.Location = new Point(signInBtn.Location.X, signInBtn.Location.Y + 27);
            resetBtn.Location = new Point(resetBtn.Location.X, resetBtn.Location.Y + 27);

            studentCard.Size = new Size(studentCard.Size.Width, studentCard.Size.Height + 26);
            forgotPassBtn.Location = new Point(forgotPassBtn.Location.X, forgotPassBtn.Location.Y + 27);

            this.MaximumSize = new Size(this.Size.Width, this.Size.Height + 27);
            this.Size = new Size(this.Size.Width, this.Size.Height + 27);
        }
    }
}
