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

        #region FIELDS

        DataTable enroleeInfo = new DataTable();
        DataTable enrolledCourse = new DataTable();

        string id, pass, bDay, birthDate;

        bool enroleeBoxState = false, bDateBoxState = false, passBoxState = false;

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
            // Check if there are any warnings (empty fields)
            if (IfWarning())
            {
                // Display an informational alert and return from the method
                functions.Alert("Complete the Requirements", AlertForm.Type.Info);
                return;
            }

            FlashingScreenForm flashingScreenForm = new FlashingScreenForm();

            // Show the FlashingScreenForm as a modal dialog (blocks interaction with the main form)
            flashingScreenForm.ShowDialog();

            // Call the SignInDatabase method
            SignInDatabase();
        }

        #endregion

        #region FUNCTIONS FOR SIGN-IN VALIDATION AND DATABASE ACCESS

        private void SignInDatabase()
        {
            id = enroleeNumberBox.Text;
            pass = enroleePasswordBox.Text;

            // Format the birthDay to have leading zero if less than 10.
            bDay = (Convert.ToInt16(bDayComBox.Text) < 10) ? $"0{bDayComBox.Text}" : bDayComBox.Text;

            // Format the full birthDate string.
            birthDate = $"{bMonthComBox.Text} {bDay}, {bYearComBox.Text}";

            // Create a SQL query to check if the user exists in the database.
            string query = "Select * from ApplicantData Where EnroleeNumber = '" + id + "' AND Password = '" + pass + "' AND BirtthDate = '" + birthDate + "'";

            // Execute the SQL query and store the result in the enroleeInfo DataTable.
            objDBAccess.readDatathroughAdapter(query, enroleeInfo);

            // Check if a single user was found (should be unique).
            if (enroleeInfo.Rows.Count == 1)
            {
                functions.Alert("Logged-In Successfully", AlertForm.Type.Success);

                // Check if the user is approved.
                bool ifApproved = Convert.ToBoolean(enroleeInfo.Rows[0]["Approved"]);

                enroleeNumberBox.Clear();
                enroleePasswordBox.Clear();

                // Close the database connection.
                objDBAccess.closeConn();

                // Redirect to the appropriate form based on approval status.
                if (!ifApproved)
                {
                    this.Hide();
                    var EnroleeWaitingForm = new EnroleeWaitingForm();
                    EnroleeWaitingForm.FormClosed += (s, args) => this.Close();
                    EnroleeWaitingForm.Show();
                }
                else
                {
                    FindEnrolledCourse();

                    this.Hide();
                    var EnroleeApprovalForm = new EnroleeApprovalForm(enroleeInfo, enrolledCourse.Rows[0]["SelectedCourse"].ToString());
                    EnroleeApprovalForm.FormClosed += (s, args) => this.Close();
                    EnroleeApprovalForm.Show();
                }
            }
            else
                functions.Alert("Incorrect Input", AlertForm.Type.Error);
        }

        private Boolean IfWarning()
        {
            bool userNBox, bDayBox, userPBox;

            userNBox = String.IsNullOrEmpty(enroleeNumberBox.Text);
            bDayBox = (bMonthComBox.SelectedIndex == 0 || bDayComBox.SelectedIndex == 0 || bYearComBox.SelectedIndex == 0);
            userPBox = String.IsNullOrEmpty(enroleePasswordBox.Text);

            // Compare the current and previous empty boxes.
            // If they are different, then the location of every boxes will be changed.
            if (CheckTheCurrentEmpty())
            {
                // If there was a previous warning, hide it
                UnvisibleWarning();

                if (userNBox)
                    ChangeLocSize(3);

                if (bDayBox)
                    ChangeLocSize(2);

                if (userPBox)
                    ChangeLocSize(1); 
            }

            // Return true if any of the input fields are empty, otherwise return false
            return userNBox || bDayBox || userPBox;
        }

        #endregion

        #region FUNCTION TO EASILY ACCESS THE ENROLLED COURSE IN DATABASE

        private void FindEnrolledCourse()
        {
            // Define a SQL query that selects courses with '1' at the end, based on EnroleeNumber, Password and BirthDate.
            string query = @"
                SELECT 
                    CASE 
                        WHEN FirstCourse LIKE '%1' THEN FirstCourse
                        WHEN SecondCourse LIKE '%1' THEN SecondCourse
                        WHEN ThirdCourse LIKE '%1' THEN ThirdCourse
                        WHEN FourthCourse LIKE '%1' THEN FourthCourse
                        WHEN FifthCourse LIKE '%1' THEN FifthCourse
                    END AS SelectedCourse
                FROM ApplicantData
                WHERE EnroleeNumber = '" + id + "' AND Password = '" + pass + "' AND BirtthDate = '" + birthDate + "'";          

            // Execute the SQL query and store the result in the 'enrolledCourse' DataTable.
            objDBAccess.readDatathroughAdapter(query, enrolledCourse);

            // Close the database connection.
            objDBAccess.closeConn();
        }


        #endregion

        #region FEATURES

        #region FUNCTIONS TO HANDLE THE RESPONSIVENESS OF VALIDATION - DO NOT TOUCH THESE.

        #region FUNCTION TO COMPARE THE CURRENT AND PREVIOUS BOXES

        private bool CheckTheCurrentEmpty()
        {
            bool thisEnroleeBoxState, thisBDateBoxState, thisPassBoxState;

            thisEnroleeBoxState = String.IsNullOrEmpty(enroleeNumberBox.Text);
            thisBDateBoxState = (bMonthComBox.SelectedIndex == 0) || (bDayComBox.SelectedIndex == 0) || (bYearComBox.SelectedIndex == 0);
            thisPassBoxState = String.IsNullOrEmpty(enroleePasswordBox.Text);

            // Compare the current states with the previous states (stored in class-level variables)
            // If they are all the same as before, return false (no change in the state)
            if ((thisEnroleeBoxState == enroleeBoxState) && (thisBDateBoxState == bDateBoxState) && (thisPassBoxState == passBoxState))
                return false;

            // Update the class-level variables with the current states for the next comparison
            enroleeBoxState = thisEnroleeBoxState;
            bDateBoxState = thisBDateBoxState;
            passBoxState = thisPassBoxState;

            // Return true if there was a change in the state of any input field
            return true;
        }

        #endregion

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
            enroleePasswordBox.Location = new Point(enroleePasswordBox.Location.X, 156);
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
                enroleePasswordBox.Location = new Point(enroleePasswordBox.Location.X, enroleePasswordBox.Location.Y + 27);

                bdWarning.Location = new Point(bdWarning.Location.X, bdWarning.Location.Y + 27);
                passWarning.Location = new Point(passWarning.Location.X, passWarning.Location.Y + 28);
            }

            if (numBox == 2)
            {
                // If there are two input boxes, show the corresponding warning label and adjust positions.
                bdWarning.Visible = true;
                enroleePasswordBox.Location = new Point(enroleePasswordBox.Location.X, enroleePasswordBox.Location.Y + 27);

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
            enroleeNumberBox.Text = "";
            enroleePasswordBox.Text = "";
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
            if (!enroleePasswordBox.Password)
            {
                // If not in password mode, switch to password mode.
                enroleePasswordBox.TrailingIcon = Properties.Resources.hide; // Change the trailing icon to "hide".
                enroleePasswordBox.Password = true; // Set the "Password" property to true to hide the password characters.
            }
            else
            {
                // If already in password mode, switch to text mode.
                enroleePasswordBox.TrailingIcon = Properties.Resources.show; // Change the trailing icon to "show".
                enroleePasswordBox.Password = false; // Set the "Password" property to false to show the password characters.
            }
        }

        #endregion

        #endregion
    }
}
