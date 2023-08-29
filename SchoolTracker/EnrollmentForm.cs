using Guna.UI2.WinForms;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SchoolTracker
{
    public partial class EnrollmentForm : MaterialForm
    {
        #region CLASSES

        // Instances of classes that will store the validated data.
        StudentData studentData = new StudentData();
        MotherData motherData = new MotherData();
        FatherData fatherData = new FatherData();
        GuardianData guardianData = new GuardianData();

        Functionality functions = new Functionality();

        #endregion

        #region FIELDS

        int previousTab = 0;

        #region FIELDS FOR DEPARTMENTS AND COURSES SELECTION

        private List<DataTable> deptDTableList = new List<DataTable>();

        DataTable firstDept = new DataTable();
        DataTable secondDept = new DataTable();
        DataTable thirdDept = new DataTable();
        DataTable fourthDept = new DataTable();
        DataTable fifthDept = new DataTable();

        #endregion

        #endregion

        public EnrollmentForm()
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

        #region GENERAL FUNCTIONS FOR BASIC INFORMATION TAB

        #region FUNCTIONS FOR SWITCHES

        #region FUNCTION FOR IF SAME CURRENT/PERMANENT ADDRESS 

        // Event handler method triggered when the state of the checkbox 'permaAddSwitch' changes
        private void permaAddSwitch_CheckedChanged(object sender, EventArgs e)
        {
            // Create a list to store instances of 'MaterialTextBox' controls
            List<MaterialTextBox> textBoxList = new List<MaterialTextBox>
            {
                //CURRENT ADDRESS TEXTBOXES
                stNumBox,
                stNameBox,
                brgyBox,
                cityBox,
                provBox,

                //PERMANENT ADDRESS TEXTBOXES
                pStNumBox,
                pStNameBox,
                pBrgyBox,
                pCityBox,
                pProvBox
            };

            for (int i = 0; i < 5; i++)
            {
                /* If checked, copy the text from current address textbox (at index 'i')
                   to the corresponding permanent address textbox (at index 'i + 5') */
                // If not checked, clear the text in the corresponding permanent address textbox
                if (permaAddSwitch.Checked)
                    textBoxList[i + 5].Text = textBoxList[i].Text;
                else
                    textBoxList[i + 5].Clear();
            }
        }

        #endregion

        #region FUNCTION FOR EXTRA INFORMATION ENABLE/DISABLE TEXTBOXES LOGIC

        // Event handler method triggered when the state of the checkbox 'extraInfoSwitch' changes
        private void extraInfoSwitch_CheckedChanged(object sender, EventArgs e)
        {
            // Cast the sender object to a 'MaterialSwitch' type
            MaterialSwitch materialSwitch = (MaterialSwitch)sender;

            // Get the name and checked state of the switch that triggered the event
            string checkedSwitch = materialSwitch.Name;
            bool ifChecked = materialSwitch.Checked;

            // Check if the triggered switch is 'fpsSwitch' and if not, it is 'ipSwitch'
            if (checkedSwitch == "fpsSwitch")
                fpsBox.Enabled = ifChecked;
            else
                ipBox.Enabled = ifChecked;
        }

        #endregion

        #endregion

        #region FUNCTIONS FOR CHECKBOXES

        #region FUNCTION TO HANDLE PARENT/GUARDIAN (NONE) CHECKBOXES LOGIC

        // This event handler is triggered when any of the "noneParentsGuardiansBtn" checkboxes' checked state changes.
        private void noneParentsGuardiansBtn_CheckedChanged(object sender, EventArgs e)
        {
            // Cast the sender object to a MaterialCheckbox, representing the checkbox that triggered the event.
            MaterialCheckbox materialCheckbox = (MaterialCheckbox)sender;

            // Create a list to hold MaterialTextBox controls that need to be enabled/disabled.
            List<MaterialTextBox> materialTextBoxList = new List<MaterialTextBox>();

            // Get the name of the checked checkbox and calculate the inverse of its checked state.
            string checkedCheckBox = materialCheckbox.Name;
            bool ifChecked = materialCheckbox.Checked;

            // Perform different enabling/disabling logic based on the checked checkbox.
            // Adding textboxes to the materialTextBoxList based on the checkbox that triggered the event.
            switch (checkedCheckBox)
            {
                case "mNoneBtn":
                    materialTextBoxList.Add(mLNameBox);
                    materialTextBoxList.Add(mFNameBox);
                    materialTextBoxList.Add(mMNameBox);
                    materialTextBoxList.Add(mCNumBox);

                    gMotherBtn.Enabled = !ifChecked;

                    if (ifChecked)
                        gMotherBtn.Checked = false;
                    break;

                case "fNoneBtn":
                    materialTextBoxList.Add(fLNameBox);
                    materialTextBoxList.Add(fFNameBox);
                    materialTextBoxList.Add(fMNameBox);
                    materialTextBoxList.Add(fCNumBox);

                    fExtNameCBox.Enabled = !ifChecked;

                    if (ifChecked)
                        fExtNameCBox.SelectedIndex = 0;

                    gFatherBtn.Enabled = !ifChecked;

                    if (ifChecked)
                        gFatherBtn.Checked = false;
                    break;

                case "gNoneBtn":
                    materialTextBoxList.Add(gLNameBox);
                    materialTextBoxList.Add(gFNameBox);
                    materialTextBoxList.Add(gMNameBox);
                    materialTextBoxList.Add(gCNumBox);


                    if (ifChecked)
                        gExtNameCBox.SelectedIndex = 0;

                    gExtNameCBox.Enabled = !ifChecked;

                    if (!mNoneBtn.Checked)
                        gMotherBtn.Enabled = !ifChecked;

                    if (!fNoneBtn.Checked)
                        gFatherBtn.Enabled = !ifChecked;

                    if (ifChecked)
                    {
                        // UNSUBSCRIBE AND SUBSCRIBE AFTER CHANGING THE CHECKED STATE OF BOTH BUTTONS TO AVOID CheckChanged DOMINO.
                        gMotherBtn.CheckedChanged -= ifMotherFatherIsAGuardian_CheckedChanged;
                        gFatherBtn.CheckedChanged -= ifMotherFatherIsAGuardian_CheckedChanged;

                        gMotherBtn.Checked = false;
                        gFatherBtn.Checked = false;

                        gMotherBtn.CheckedChanged += ifMotherFatherIsAGuardian_CheckedChanged;
                        gFatherBtn.CheckedChanged += ifMotherFatherIsAGuardian_CheckedChanged;
                    }
                    break;
            }

            // Iterate through the list of MaterialTextBox controls and enable/disable them accordingly.
            foreach (MaterialTextBox textBox in materialTextBoxList)
            {
                textBox.Enabled = !ifChecked;

                // If the checkbox is checked, clear the content of the MaterialTextBox.
                if (ifChecked)
                    textBox.Clear();
            }
        }


        #endregion

        #region FUNCTION TO HANDLE (IF MOTHER/FATHER -> GUARDIAN) CHECKBOXES LOGIC

        // This event handler is triggered when the checked state of the "gMotherBtn" or "gFatherBtn" checkboxes changes.
        private void ifMotherFatherIsAGuardian_CheckedChanged(object sender, EventArgs e)
        {
            // Cast the sender object to a MaterialCheckbox, representing the checkbox that triggered the event.
            MaterialCheckbox materialCheckBox = (MaterialCheckbox)sender;

            // Create a list to hold MaterialTextBox controls for guardians' information.
            List<MaterialTextBox> materialGuardianTextBoxList = new List<MaterialTextBox>
            {
                gLNameBox,
                gFNameBox,
                gMNameBox,
                gCNumBox
            };

            // Create a list to hold MaterialTextBox controls for parents' information.
            List<MaterialTextBox> materialParentTextBoxList = new List<MaterialTextBox>();

            // Get the name of the checked checkbox and its checked state.
            string checkedCheckBox = materialCheckBox.Name;
            bool ifChecked = materialCheckBox.Checked;

            // Perform different logic based on the checked checkbox.
            switch (checkedCheckBox)
            {
                case "gMotherBtn":
                    materialParentTextBoxList.Add(mLNameBox);
                    materialParentTextBoxList.Add(mFNameBox);
                    materialParentTextBoxList.Add(mMNameBox);
                    materialParentTextBoxList.Add(mCNumBox);

                    gExtNameCBox.SelectedIndex = 0;
                    gExtNameCBox.Enabled = !ifChecked;

                    gFatherBtn.CheckedChanged -= ifMotherFatherIsAGuardian_CheckedChanged;
                    gFatherBtn.Checked = false;
                    gFatherBtn.CheckedChanged += ifMotherFatherIsAGuardian_CheckedChanged;
                    break;

                case "gFatherBtn":
                    materialParentTextBoxList.Add(fLNameBox);
                    materialParentTextBoxList.Add(fFNameBox);
                    materialParentTextBoxList.Add(fMNameBox);
                    materialParentTextBoxList.Add(fCNumBox);

                    // Set the guardian's extended name combo box index based on the checked state.
                    gExtNameCBox.SelectedIndex = ifChecked
                        ? fExtNameCBox.SelectedIndex
                        : 0;

                    gExtNameCBox.Enabled = true;
                    gExtNameCBox.Refresh();

                    gMotherBtn.CheckedChanged -= ifMotherFatherIsAGuardian_CheckedChanged;
                    gMotherBtn.Checked = false;
                    gMotherBtn.CheckedChanged += ifMotherFatherIsAGuardian_CheckedChanged;
                    break;
            }

            // Loop through the guardian and parent text boxes and perform necessary actions.
            for (int i = 0; i < 4; i++)
            {
                if (ifChecked) // Copy parent's information to guardian's text boxes if the checkbox is checked.
                    materialGuardianTextBoxList[i].Text = materialParentTextBoxList[i].Text;
                else // Clear guardian's text boxes if the checkbox is not checked.
                    materialGuardianTextBoxList[i].Clear();
            }
        }


        #endregion

        #endregion

        #region FUNCTION FOR BUTTONS

        #region FUNCTION TO SELECT PHOTO

        private void selectPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // To handle any exceptions that might occur during the process.
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Bitmap selectedImage = new Bitmap(openFileDialog.FileName);

                    // Check if the loaded image is square using the IsSquareImage function.
                    // If the image is square, set it as the student's picture.
                    // Else, display a warning message.
                    if (IsSquareImage(selectedImage))
                    {
                        studentPicture.Image = selectedImage;
                    }
                    else
                        MessageBox.Show("Please select a square image.", "PUP-SIS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error selecting photo: {ex.Message}", "PUP-SIS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // This method checks if an image is square based on its width and height.
        private bool IsSquareImage(Bitmap image) => image.Width == image.Height;

        #endregion

        #region FUNCTIONS FOR VALIDATING THE INFORMATIONS BEFORE THE APPROVAL OF SUBMISSION

        private void submitInfoBtn_Click(object sender, EventArgs e)
        {
            //If the information is validated(all checks pass), the user will be directed to the next tab.
            if (ValidateInformation())
            {
                RetrievingInformation();

                // Calculate the index of the next tab to be displayed.
                int nextTabIndex = enrollmentTab.SelectedIndex + 1;

                // Temporarily remove the event handler "enrollmentTab_Selecting".
                // Set the selected index of the tab control to the calculated nextTabIndex.
                // Add back the event handler "enrollmentTab_Selecting".
                enrollmentTab.Selecting -= enrollmentTab_Selecting;
                enrollmentTab.SelectedIndex = nextTabIndex;
                enrollmentTab.Selecting += enrollmentTab_Selecting;
            }
        }

        #region FUNCTION FOR INFORMATION'S VALIDATION

        // This method is responsible for validating the information provided in various input controls.
        private bool ValidateInformation()
        {
            #region LIST OF CONTROLS THAT CONTAIN INFORMATION TO BE VALIDATED

            List<Control> controlList = new List<Control>
            {
                studentPicture,
                lNameBox,
                fNameBox,
                mNameBox,
                bDatePicker,
                genderCard,
                studCNumBox,
                landlineNumBox,
                gmailAddBox,
                fbLinkBox,
                birthPlaceBox,
                stNumBox,
                stNameBox,
                brgyBox,
                cityBox,
                provBox,
                pStNumBox,
                pStNameBox,
                pBrgyBox,
                pCityBox,
                pProvBox,
                mLNameBox,
                mFNameBox,
                mMNameBox,
                mCNumBox,
                fLNameBox,
                fFNameBox,
                fMNameBox,
                fCNumBox,
                gLNameBox,
                gFNameBox,
                gMNameBox,
                gCNumBox,
                eSchoolName,
                hSchoolName,
                shSchoolName,
                lrnBox,
            };

            #endregion

            // Create an array of base class type PersonData to store instances of different people
            PersonData[] personDatas = new PersonData[]
            {
                studentData,
                motherData,
                fatherData,
                guardianData
            };

            // Index to keep track of the current person in the array.
            int indexPerson = 0;
            bool ifReturn = true;

            // Loop through each control in controlList to be validated.
            foreach (Control control in controlList)
            {
                switch (control)
                {
                    // Handling student picture
                    case PictureBox pictureBox when pictureBox == studentPicture:
                        // If student picture is empty, show an error message. Return false to indicate validation failure.
                        // Else, store the student picture in student data.
                        if (functions.ImageEquals(pictureBox.Image, SchoolTracker.Properties.Resources.user))
                        {
                            MessageBox.Show("The student picture is empty. Please select your picture.", "PUP-SIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        else
                            studentData.Image = pictureBox.Image;

                        break;

                    // Handling birth date and age
                    case Guna2DateTimePicker datePicker when datePicker == bDatePicker:
                        // If student age and birth date don't match, show an error message. Return false to indicate validation failure.
                        // Else, set birth date and age in student data.
                        if (!functions.ValidateStudentAgeInBDate(bDatePicker, Convert.ToInt32(ageCBox.Text)))
                        {
                            MessageBox.Show("The age and birth date did not match. Please enter a correct birth date or age.", "PUP-SIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        else
                        {
                            studentData.BirthDate = datePicker.Value.ToString("MMMM dd, yyyy");
                            studentData.Age = ageCBox.Text;
                        }

                        break;

                    // Handling gender selection
                    case MaterialCard card when card == genderCard:
                        // If student selected gender is empty, show an error message. Return false to indicate validation failure.
                        // Else, set gender in student data.
                        if (!maleBtn.Checked && !femaleBtn.Checked)
                        {
                            MessageBox.Show("Selected gender is empty. Please select your gender.", "PUP-SIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        else
                            studentData.Gender = (maleBtn.Checked) ? "Male" : "Female";

                        break;

                    // Handling phone number text boxes
                    case MaterialTextBox textBox when control.Name.Contains("CNumBox"):
                        if (textBox.Enabled)
                            if (string.IsNullOrEmpty(textBox.Text))
                            {
                                MessageBox.Show("Phone number is empty. Please enter a phone number.", "PUP-SIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                textBox.Focus();
                                return false;
                            }
                            else
                            {
                                // Set the ContactNumber property of the current person's data based on the index
                                personDatas[indexPerson].ContactNumber = textBox.Text.Trim();

                                if (string.IsNullOrEmpty(personDatas[indexPerson].ContactNumber))
                                {
                                    textBox.Focus();
                                    return false;
                                }
                            }

                        // Move to the next person in the array.
                        indexPerson++;
                        break;

                    // Handling specific text boxes with validation
                    case MaterialTextBox textBox when control == landlineNumBox:
                        ifReturn = ValidateAndAssign(textBox, studentData, "Landline number", "LandlineNumber");
                        break;
                    case MaterialTextBox textBox when control == gmailAddBox:
                        ifReturn = ValidateAndAssign(textBox, studentData, "Gmail address", "GmailAddress");
                        break;
                    case MaterialTextBox textBox when control == fbLinkBox:
                        ifReturn = ValidateAndAssign(textBox, studentData, "Facebook link", "FacebookLink");
                        break;

                    // Handling other MaterialTextBox controls
                    case MaterialTextBox materialTextBox:
                        if (String.IsNullOrEmpty(materialTextBox.Text) && materialTextBox.Enabled)
                        {
                            string capitalizedString = char.ToUpper(materialTextBox.Hint[0]) + materialTextBox.Hint.Substring(1).ToLower();
                            MessageBox.Show($"{capitalizedString} is empty. Please enter a {materialTextBox.Hint.ToLower()}.", "PUP-SIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            materialTextBox.Focus();
                            return false;
                        }
                        break;
                }

                // If above statement return a false value for ifReturn, this statement will return false to indicate validation failure.
                if (!ifReturn)
                    return false;
            }

            if (fpsSwitch.Checked)
            {
                if (string.IsNullOrEmpty(fpsBox.Text))
                {
                    MessageBox.Show($"4Ps household id number is empty. Please enter a valid id number.", "PUP-SIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    fpsBox.Focus();
                    return false;
                }
                studentData.FPSNumber = fpsBox.Text;
            }
            else
                studentData.FPSNumber = "N/A";

            if (ipSwitch.Checked)
            {
                if (string.IsNullOrEmpty(ipBox.Text))
                {
                    MessageBox.Show($"Indigenous people community is empty. Please enter a community.", "PUP-SIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ipBox.Focus();
                    return false;
                }
                studentData.IPCommunity = ipBox.Text;
            }
            else
                studentData.IPCommunity = "N/A";

            PassingInformation();

            // If all validations pass, return true.
            return true;
        }

        #endregion

        #region FUNCTION FOR STORING THE REMAINING INFORMATION TO THE CLASSES.

        private void PassingInformation()
        {
            studentData.LastName = lNameBox.Text;
            studentData.FirstName = fNameBox.Text;
            studentData.MiddleName = mNameBox.Text;
            studentData.ExtensionName = extNameBox.Text;
            studentData.BirthPlace = birthPlaceBox.Text;

            studentData.StreetNumber = stNumBox.Text;
            studentData.StreetName = stNameBox.Text;
            studentData.Barangay = brgyBox.Text;
            studentData.City = cityBox.Text;
            studentData.Province = provBox.Text;

            studentData.PStreetNumber = pStNumBox.Text;
            studentData.PStreetName = pStNameBox.Text;
            studentData.PBarangay = pBrgyBox.Text;
            studentData.PCity = pCityBox.Text;
            studentData.PProvince = pProvBox.Text;

            motherData.LastName = mLNameBox.Text;
            motherData.FirstName = mFNameBox.Text;
            motherData.MiddleName = mMNameBox.Text;

            fatherData.LastName = fLNameBox.Text;
            fatherData.FirstName = fFNameBox.Text;
            fatherData.MiddleName = fMNameBox.Text;
            fatherData.ExtensionName = fExtNameCBox.Text;

            guardianData.LastName = gLNameBox.Text;
            guardianData.FirstName = gFNameBox.Text;
            guardianData.MiddleName = gMNameBox.Text;
            guardianData.ExtensionName = gExtNameCBox.Text;

            studentData.ElementarySchool = eSchoolName.Text;
            studentData.HighSchool = hSchoolName.Text;
            studentData.SeniorHighSchool = shSchoolName.Text;

            studentData.LRN = lrnBox.Text;
        }

        #endregion

        #endregion

        #region FUNCTION TO VALIDATE FACEBOOK LINK, GMAIL ADDRESS AND LANDLINE NUMBER

        // It will return true if there is an issue; otherwise, it will return false.
        private bool ValidateAndAssign(Control textBox, StudentData studentData, string fieldName, string property)
        {
            string input = textBox.Text.Trim();

            // Show an error message using the field name if the input is null or empty, then focus on the textbox and return true (indicating validation failure)
            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show($"{fieldName} is empty. Please enter a {fieldName}.", "PUP-SIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Focus();

                return false;
            }
            else
            {
                // Using reflection, set the value of the specified property in the 'studentData' object to the 'input'
                studentData.GetType().GetProperty(property).SetValue(studentData, input);

                // Using reflection, get the value of the specified property in the 'studentData' object,
                // convert it to a string, check if it's empty or null
                if (string.IsNullOrEmpty(studentData.GetType().GetProperty(property).GetValue(studentData) as string))
                {
                    textBox.Focus();

                    return false;
                }
            }

            return true;
        }

        #endregion

        #endregion

        #region FUNCTION TO RESET ALL FILLED INFORMATION

        private void resetInfoBtn_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Clicking reset will clear all your provided data. Would you like to continue?", "PUP-SIS", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                // Iterate through each control in the basicInfoTab's Controls collection.
                foreach (Control control in basicInfoTab.Controls)
                {
                    // Use a switch statement with pattern matching to handle different control types.
                    switch (control)
                    {
                        case MaterialTextBox textBox:
                            textBox.Clear();
                            break;
                        case MaterialComboBox comboBox:
                            comboBox.SelectedIndex = 0;
                            break;
                        case MaterialSwitch switchControl:
                            switchControl.Checked = false;
                            break;
                        case MaterialCheckbox checkbox:
                            checkbox.Checked = false;
                            break;
                    }
                }

                maleBtn.Checked = false;
                femaleBtn.Checked = false;

                // Set the studentPicture Image to the default user image.
                studentPicture.Image = SchoolTracker.Properties.Resources.user;
            }
        }

        #endregion

        #region FUNCTIONS FOR KEY PRESS

        private void infoBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                submitInfoBtn_Click(sender, e);
        }

        #endregion

        #endregion

        #region GENERAL FUNCTIONS FOR BASIC INFORMATION REVIEW TAB

        #region FUNCTION FOR SUBMIT AND BACK BUTTON

        private void finalizeInfoBtn_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to continue?", "School Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                MessageBox.Show("Finalized successfully.", "School Admin", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CoursesSelectionLoad();

                // Calculate the index of the next tab to be displayed.
                int nextTabIndex = enrollmentTab.SelectedIndex + 1;

                enrollmentTab.Selecting -= enrollmentTab_Selecting;
                enrollmentTab.SelectedIndex = nextTabIndex;
                enrollmentTab.Selecting += enrollmentTab_Selecting;
            }
        }

        private void infoRevBackBtn_Click(object sender, EventArgs e)
        {
            // Calculate the index of the previous tab to be displayed.
            int nextTabIndex = enrollmentTab.SelectedIndex - 1;

            enrollmentTab.Selecting -= enrollmentTab_Selecting;
            enrollmentTab.SelectedIndex = nextTabIndex;
            enrollmentTab.Selecting += enrollmentTab_Selecting;
        }

        #endregion

        #region FUNCTION FOR RETRIEVING INFORMATION FROM THE CLASSES

        private void RetrievingInformation()
        {
            revStudentPicture.Image = studentData.Image;

            revLNameBox.Text = studentData.LastName;
            revFNameBox.Text = studentData.FirstName;
            revMNameBox.Text = studentData.MiddleName;
            revENameBox.Text = studentData.ExtensionName;

            revBDateBox.Text = studentData.BirthDate;
            revAgeBox.Text = studentData.Age;
            revGenderBox.Text = studentData.Gender;
            revCNumBox.Text = studentData.ContactNumber;
            revTelNumBox.Text = studentData.LandlineNumber;
            revGmailAddBox.Text = studentData.GmailAddress;
            revFBLinkBox.Text = studentData.FacebookLink;
            revBPlaceBox.Text = studentData.BirthPlace;

            revStreetNumBox.Text = studentData.StreetNumber;
            revStreetNameBox.Text = studentData.StreetName;
            revBrgyBox.Text = studentData.Barangay;
            revCityBox.Text = studentData.City;
            revProvBox.Text = studentData.Province;

            revPStreetNumBox.Text = studentData.PStreetNumber;
            revPStreetNameBox.Text = studentData.PStreetName;
            revPBrgyBox.Text = studentData.PBarangay;
            revPCityBox.Text = studentData.PCity;
            revPProvBox.Text = studentData.PProvince;

            revMLNameBox.Text = motherData.LastName;
            revMFNameBox.Text = motherData.FirstName;
            revMMNameBox.Text = motherData.MiddleName;
            revMCNumBox.Text = motherData.ContactNumber;

            revFLNameBox.Text = fatherData.LastName;
            revFFNameBox.Text = fatherData.FirstName;
            revFMNameBox.Text = fatherData.MiddleName;
            revFENameBox.Text = fatherData.ExtensionName;
            revFCNumBox.Text = fatherData.ContactNumber;

            revGLNameBox.Text = guardianData.LastName;
            revGFNameBox.Text = guardianData.FirstName;
            revGMNameBox.Text = guardianData.MiddleName;
            revGENameBox.Text = guardianData.ExtensionName;
            revGCNumBox.Text = guardianData.ContactNumber;

            revESchoolBox.Text = studentData.ElementarySchool;
            revHSchoolBox.Text = studentData.HighSchool;
            revSHSchoolBox.Text = studentData.SeniorHighSchool;

            revLRNBox.Text = studentData.LRN;
            revFPSBox.Text = studentData.FPSNumber;
            revIPBox.Text = studentData.IPCommunity;
        }

        #endregion

        #endregion

        #region GENERAL FUNCTIONS FOR COURSES SELECTION TAB

        #region FUNCTION TO LOAD THE COURSES SELECTION TAB

        private void CoursesSelectionLoad()
        {
            FillDepartmentTable();

            firstDepartment.DataSource = firstDept;
            firstDepartment.DisplayMember = "DName";

            secondDepartment.DataSource = secondDept;
            secondDepartment.DisplayMember = "DName";

            thirdDepartment.DataSource = thirdDept;
            thirdDepartment.DisplayMember = "DName";

            fourthDepartment.DataSource = fourthDept;
            fourthDepartment.DisplayMember = "DName";

            fifthDepartment.DataSource = fifthDept;
            fifthDepartment.DisplayMember = "DName";
        }

        #endregion

        #region FUNCTION TO FILL THE DEPARTMENT TABLES

        private void FillDepartmentTable()
        {
            deptDTableList.Clear();
            deptDTableList.Add(firstDept);
            deptDTableList.Add(secondDept);
            deptDTableList.Add(thirdDept);
            deptDTableList.Add(fourthDept);
            deptDTableList.Add(fifthDept);

            foreach (DataTable dt in deptDTableList)
            {
                dt.Columns.Clear();
                dt.Columns.Add("DID", typeof(int));
                dt.Columns.Add("DName");

                dt.Rows.Clear();
                dt.Rows.Add(1, "- Select your department -");
                dt.Rows.Add(2, "College of Accountancy and Finance (CAF)");
                dt.Rows.Add(3, "College of Architecture, Design and the Built Environment (CADBE)");
                dt.Rows.Add(4, "College of Arts and Letters (CAL)");
                dt.Rows.Add(5, "College of Business Administration (CBA)");
                dt.Rows.Add(6, "College of Communication (COC)");
                dt.Rows.Add(7, "College of Computer and Information Sciences (CCIS)");
                dt.Rows.Add(8, "College of Education (COED)");
                dt.Rows.Add(9, "College of Engineering (CE)");
                dt.Rows.Add(10, "College of Human Kinetics (CHK)");
                dt.Rows.Add(11, "College of Law (CL)");
                dt.Rows.Add(12, "College of Political Science and Public Administration (CPSPA)");
                dt.Rows.Add(13, "College of Social Sciences and Development (CSSD)");
                dt.Rows.Add(14, "College of Science (CS)");
                dt.Rows.Add(15, "College of Tourism, Hospitality and Transportation Management (CTHTM)");
                dt.Rows.Add(16, "Institute of Technology");
            }
        }

        #endregion

        #region FUNCTIONS FOR SUBMIT AND BACK BUTTON

        private void submitCoursesBtn_Click(object sender, EventArgs e)
        {
            // Calculate the index of the next tab to be displayed.
            int nextTabIndex = enrollmentTab.SelectedIndex + 1;

            enrollmentTab.Selecting -= enrollmentTab_Selecting;
            enrollmentTab.SelectedIndex = nextTabIndex;
            enrollmentTab.Selecting += enrollmentTab_Selecting;
        }

        private void coursesBackBtn_Click(object sender, EventArgs e)
        {
            // Calculate the index of the previous tab to be displayed.
            int nextTabIndex = enrollmentTab.SelectedIndex - 1;

            enrollmentTab.Selecting -= enrollmentTab_Selecting;
            enrollmentTab.SelectedIndex = nextTabIndex;
            enrollmentTab.Selecting += enrollmentTab_Selecting;
        }

        #endregion

        #endregion

        #region GENERAL FUNCTIONS FOR COURSES SELECTION REVIEW TAB

        private void finalizeCoursesBtn_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to continue?", "School Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                MessageBox.Show("Finalized successfully.", "School Admin", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Calculate the index of the next tab to be displayed.
                int nextTabIndex = enrollmentTab.SelectedIndex + 1;

                enrollmentTab.Selecting -= enrollmentTab_Selecting;
                enrollmentTab.SelectedIndex = nextTabIndex;
                enrollmentTab.Selecting += enrollmentTab_Selecting;
            }
        }

        private void coursesRevBackBtn_Click(object sender, EventArgs e)
        {
            // Calculate the index of the previous tab to be displayed.
            int nextTabIndex = enrollmentTab.SelectedIndex - 1;

            enrollmentTab.Selecting -= enrollmentTab_Selecting;
            enrollmentTab.SelectedIndex = nextTabIndex;
            enrollmentTab.Selecting += enrollmentTab_Selecting;
        }

        #endregion

        #region GENERAL FUNCTIONS FOR ENROLLMENT FORM

        #region FUNCTIONS TO OPEN VARIOUS WEBSITES

        private void openWebBtn_Click(object sender, EventArgs e) => functions.OpenWeb(0);
        private void termUseBtn_Click(object sender, EventArgs e) => functions.OpenWeb(1);
        private void privacyStateBtn_Click(object sender, EventArgs e) => functions.OpenWeb(2);

        #endregion

        #region FUNCTION FOR DISABLING TAB CONTROL AND FOR RETURNING TO THE FRONT FORM

        private void enrollmentTab_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (enrollmentTab.SelectedIndex != 5)
                //Prevent manual tab changing.
                e.Cancel = true;


            if (e.TabPageIndex == 5)
            {
                DialogResult dr = MessageBox.Show("Returning to the front form will clear all your provided data. Would you like to continue?", "PUP-SIS", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dr == DialogResult.Yes)
                {
                    // Returning to the front form.
                    this.Hide();
                    var FrontForm = new FrontForm();
                    FrontForm.FormClosed += (s, args) => this.Close();
                    FrontForm.Show();
                }
                else
                    e.Cancel = true; // Cancel the tab selection change.
            }
            else
                previousTab = e.TabPageIndex; // Update previousTab for other tab changes.
        }

        #endregion

        #region FUNCTION TO CHANGE THE THEME COLOR [LIGHT/DARK]

        // This method is an event handler that is triggered when the state of the thSwitch (toggle switch) changes.
        private void thSwitch_CheckedChanged(object sender, EventArgs e)
        {
            // Get the current MaterialSkinManager instance to manage the application's visual theme.
            var skin = MaterialSkinManager.Instance;

            // Determine whether the current theme is light or dark.
            bool isLight = skin.Theme == MaterialSkinManager.Themes.LIGHT;

            // If the current theme is light, switch to dark theme and update the text of the toggle switch.
            // Else, the current theme is dark, switch to light theme and update the text of the toggle switch.
            if (isLight)
            {
                skin.Theme = MaterialSkinManager.Themes.DARK;
                thSwitch.Text = "DARK MODE";
            }
            else
            {
                skin.Theme = MaterialSkinManager.Themes.LIGHT;
                thSwitch.Text = "LIGHT MODE";
            }
        }

        #endregion

        #endregion

    }
}
