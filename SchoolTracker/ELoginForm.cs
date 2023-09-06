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
    public partial class ELoginForm : MaterialForm
    {
        #region CLASSES
        
        DBAccess objDBAccess = new DBAccess();

        Functionality functions = new Functionality();

        #endregion

        public ELoginForm()
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

        #region FUNCTION FOR SIGN-IN ACTION

        private void signInBtn_Click(object sender, EventArgs e)
        {
            UnvisibleWarning();

            if (IfWarning())
            {
                functions.Alert("Complete the Requirements", AlertForm.Type.Info);
                return;
            }
        }

        #endregion

        #region FUNCTION FOR SIGN-IN VALIDATION

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

        #endregion

        #region FEATURES

        #region FUNCTIONS TO HANDLE THE RESPONSIVENESS OF VALIDATION - DO NOT TOUCH THESE.

        // This method is used to hide warning labels and adjust the positions and sizes of various UI elements.
        private void UnvisibleWarning()
        {
            // Hide the warning labels.
            snWarning.Visible = false;
            bdWarning.Visible = false;
            passWarning.Visible = false;

            // Adjust the Y positions of input controls and buttons.
            bMonthComBox.Location = new Point(bMonthComBox.Location.X, 89);
            bDayComBox.Location = new Point(bDayComBox.Location.X, 89);
            bYearComBox.Location = new Point(bYearComBox.Location.X, 89);
            facPassBox.Location = new Point(facPassBox.Location.X, 156);
            signInBtn.Location = new Point(signInBtn.Location.X, 227);
            resetBtn.Location = new Point(resetBtn.Location.X, 227);

            // Adjust the Y positions of warning labels.
            bdWarning.Location = new Point(bdWarning.Location.X, 142);
            passWarning.Location = new Point(passWarning.Location.X, 206);

            // Adjust the height of the "studentCard" panel and the position of "forgotPassBtn".
            studentCard.Size = new Size(studentCard.Size.Width, 277);
            forgotPassBtn.Location = new Point(forgotPassBtn.Location.X, 608);

            // Adjust the form's height.
            this.Size = new Size(this.Size.Width, 650);
        }

        // This method is used to change the positions and sizes of UI elements based on the number of input boxes.
        private void ChangeLocSize(int numBox)
        {
            if (numBox == 3)
            {
                // If there are three input boxes, show the corresponding warning label and adjust positions.
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
                // If there are two input boxes, show the corresponding warning label and adjust positions.
                bdWarning.Visible = true;
                facPassBox.Location = new Point(facPassBox.Location.X, facPassBox.Location.Y + 27);

                passWarning.Location = new Point(passWarning.Location.X, passWarning.Location.Y + 28);
            }

            // If there is one input box, show the corresponding warning label.
            if (numBox == 1)
                passWarning.Visible = true;

            // Adjust the positions of buttons, panel, and form height.
            signInBtn.Location = new Point(signInBtn.Location.X, signInBtn.Location.Y + 27);
            resetBtn.Location = new Point(resetBtn.Location.X, resetBtn.Location.Y + 27);

            studentCard.Size = new Size(studentCard.Size.Width, studentCard.Size.Height + 26);
            forgotPassBtn.Location = new Point(forgotPassBtn.Location.X, forgotPassBtn.Location.Y + 27);

            this.MaximumSize = new Size(this.Size.Width, this.Size.Height + 27);
            this.Size = new Size(this.Size.Width, this.Size.Height + 27);
        }


        #endregion

        #region FUNCTIONS FOR BACK AND RESET BUTTONS

        // This event handler is called when the "backBtn" button is clicked.
        private void backBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            var FrontForm = new FrontForm();
            FrontForm.FormClosed += (s, args) => this.Close();
            FrontForm.Show();
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            // Clear the text in various input controls and reset dropdown selections.
            facNumBox.Text = "";
            facPassBox.Text = "";
            bMonthComBox.SelectedIndex = 0;
            bDayComBox.SelectedIndex = 0;
            bYearComBox.SelectedIndex = 0;
        }

        #endregion

        #region FUNCTION FOR HIDE AND SHOW PASSWORD BUTTON

        // This event handler is called when the trailing icon of "facPassBox" is clicked.
        private void facPassBox_TrailingIconClick(object sender, EventArgs e)
        {
            // Check if the "facPassBox" is currently in password mode.
            if (!facPassBox.Password)
            {
                // If not in password mode, switch to password mode.
                facPassBox.TrailingIcon = Properties.Resources.hide; // Change the trailing icon to "hide".
                facPassBox.Password = true; // Set the "Password" property to true to hide the password characters.
            }
            else
            {
                // If already in password mode, switch to text mode.
                facPassBox.TrailingIcon = Properties.Resources.show; // Change the trailing icon to "show".
                facPassBox.Password = false; // Set the "Password" property to false to show the password characters.
            }
        }

        #endregion

        #endregion
    }
}
