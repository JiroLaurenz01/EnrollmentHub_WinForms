using Guna.UI2.WinForms;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
        int nums = 0;
        bool ifLoaded = false;
        bool ifFilled = false;

        #region FIELDS FOR DEPARTMENTS AND COURSES SELECTION

        DataTable firstDept = new DataTable();
        DataTable secondDept = new DataTable();
        DataTable thirdDept = new DataTable();
        DataTable fourthDept = new DataTable();
        DataTable fifthDept = new DataTable();

        DataTable dtCourses = new DataTable();

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
                        functions.Alert("Select a Square Image", AlertForm.Type.Error);
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
                functions.Alert("Submitted Successfully", AlertForm.Type.Success);

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
                            functions.Alert("Select your Formal Picture", AlertForm.Type.Error);
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
                            functions.Alert("Invalid BirthDate or Age", AlertForm.Type.Error);
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
                            functions.Alert("Select your gender", AlertForm.Type.Error);
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
                                functions.Alert("Enter your Phone Number", AlertForm.Type.Error);
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
                        ifReturn = ValidateAndAssign(textBox, studentData, "Landline Number", "LandlineNumber");
                        break;
                    case MaterialTextBox textBox when control == gmailAddBox:
                        ifReturn = ValidateAndAssign(textBox, studentData, "Gmail Address", "GmailAddress");
                        break;
                    case MaterialTextBox textBox when control == fbLinkBox:
                        ifReturn = ValidateAndAssign(textBox, studentData, "Facebook Link", "FacebookLink");
                        break;

                    // Handling other MaterialTextBox controls
                    case MaterialTextBox materialTextBox:
                        if (String.IsNullOrEmpty(materialTextBox.Text) && materialTextBox.Enabled)
                        {
                            functions.Alert($"Enter a {materialTextBox.Hint}", AlertForm.Type.Error);
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
                    functions.Alert("Enter a Valid 4Ps ID Number", AlertForm.Type.Error);
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
                    functions.Alert("Enter a Valid I.P Community", AlertForm.Type.Error);
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
                functions.Alert($"Enter your {fieldName}", AlertForm.Type.Error);
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

                functions.Alert("Cleared Successfully", AlertForm.Type.Success);
            }
        }

        #endregion

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

        #region FUNCTION FOR FINALIZE AND BACK BUTTON

        private void finalizeInfoBtn_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to continue?", "School Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                functions.Alert("Finalized Successfully", AlertForm.Type.Success);

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

        #region FUNCTIONS TO HANDLE THE LOGIC OF DEPARTMENTS AND COURSES SELECTION

        #region FUNCTION TO LOAD THE COURSES SELECTION TAB

        // This method is responsible for populating the ComboBoxes with department information.
        private void CoursesSelectionLoad()
        {
            if (!ifLoaded)
            {
                // Call the method "FillDepartmentTable" to populate department-related data.
                FillDepartmentTable();

                // Call the method "FillCoursesTable" to populate courses depends on the selected department.
                FillCoursesTable();

                // Set the data source for the first to fifth ComboBoxes to the "firstDept" to "fifthDept" collections.
                // Specify that the "DName" property from the data source should be displayed in the ComboBox.

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
        }

        #endregion

        #region FUNCTION TO FILL THE DEPARTMENT TABLES

        // This method populates the department-related DataTables.
        private void FillDepartmentTable()
        {
            List<DataTable> deptDTableList = new List<DataTable>();

            // Add each department DataTable to the list.
            deptDTableList.Add(firstDept);
            deptDTableList.Add(secondDept);
            deptDTableList.Add(thirdDept);
            deptDTableList.Add(fourthDept);
            deptDTableList.Add(fifthDept);

            // For each department DataTable in the list.
            foreach (DataTable dt in deptDTableList)
            {
                //Add "DID" of integer type and "DName" column to the DataTable.
                dt.Columns.Add("DID", typeof(int));
                dt.Columns.Add("DName");

                // Add rows with department information to the DataTable.
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

            ifLoaded = true;
        }

        #endregion

        #region FUNCTION TO FILL THE COURSES TABLES

        // This method populate the courses depends on the selected department.
        private void FillCoursesTable()
        {
            dtCourses.Columns.Clear();

            // Add "CID" of integer type and "cName" column to the DataTable.
            dtCourses.Columns.Add("CID", typeof(int));
            dtCourses.Columns.Add("CName");

            dtCourses.Rows.Clear();
            // Add rows with courses information to the DataTable.
            dtCourses.Rows.Add(1, "- Select your course -");

            dtCourses.Rows.Add(2, "Bachelor of Science in Accountancy (BSA)");
            dtCourses.Rows.Add(2, "Bachelor of Science in Business Administration Major in Financial Management (BSBAFM)");
            dtCourses.Rows.Add(2, "Bachelor of Science in Management Accounting (BSMA)");

            dtCourses.Rows.Add(3, "Bachelor of Science in Architecture (BS-ARCH)");
            dtCourses.Rows.Add(3, "Bachelor of Science in Interior Design (BSID)");
            dtCourses.Rows.Add(3, "Bachelor of Science in Environmental Planning (BSEP)");

            dtCourses.Rows.Add(4, "Bachelor of Arts in English Language Studies (ABELS)");
            dtCourses.Rows.Add(4, "Bachelor of Arts in Filipinology (ABF)");
            dtCourses.Rows.Add(4, "Bachelor of Arts in Literary and Cultural Studies (ABLCS)");
            dtCourses.Rows.Add(4, "Bachelor of Arts in Philosophy (AB-PHILO)");
            dtCourses.Rows.Add(4, "Bachelor of Performing Arts major in Theater Arts (BPEA)");

            dtCourses.Rows.Add(5, "Bachelor of Science in Business Administration major in Human Resource Management (BSBAHRM)");
            dtCourses.Rows.Add(5, "Bachelor of Science in Business Administration major in Marketing Management (BSBA-MM)");
            dtCourses.Rows.Add(5, "Bachelor of Science in Entrepreneurship (BSENTREP)");
            dtCourses.Rows.Add(5, "Bachelor of Science in Office Administration (BSOA)");

            dtCourses.Rows.Add(6, "Bachelor in Advertising and Public Relations (BADPR)");
            dtCourses.Rows.Add(6, "Bachelor of Arts in Broadcasting (BA-Broad)");
            dtCourses.Rows.Add(6, "Bachelor of Arts in Communication Research (BACR)");
            dtCourses.Rows.Add(6, "Bachelor of Arts in Journalism (BAJ)");

            dtCourses.Rows.Add(7, "Bachelor of Science in Computer Science (BSCS)");
            dtCourses.Rows.Add(7, "Bachelor of Science in Information Technology (BSIT)");

            dtCourses.Rows.Add(8, "Bachelor of Technology and Livelihood Education (BTLEd)");
            dtCourses.Rows.Add(8, "Bachelor of Library and Information Science (BLIS)");
            dtCourses.Rows.Add(8, "Bachelor of Secondary Education (BSEd)");
            dtCourses.Rows.Add(8, "Bachelor of Elementary Education (BEEd)");
            dtCourses.Rows.Add(8, "Bachelor of Early Childhood Education (BECEd)");

            dtCourses.Rows.Add(9, "Bachelor of Science in Civil Engineering (BSCE)");
            dtCourses.Rows.Add(9, "Bachelor of Science in Computer Engineering (BSCpE)");
            dtCourses.Rows.Add(9, "Bachelor of Science in Electrical Engineering (BSEE)");
            dtCourses.Rows.Add(9, "Bachelor of Science in Electronics Engineering (BSECE)");
            dtCourses.Rows.Add(9, "Bachelor of Science in Industrial Engineering (BSIE)");
            dtCourses.Rows.Add(9, "Bachelor of Science in Mechanical Engineering (BSME)");
            dtCourses.Rows.Add(9, "Bachelor of Science in Railway Engineering (BSRE)");

            dtCourses.Rows.Add(10, "Bachelor of Physical Education (BPE)");
            dtCourses.Rows.Add(10, "Bachelor of Science in Exercises and Sports (BSESS)");

            dtCourses.Rows.Add(11, "Juris Doctor (JD)");

            dtCourses.Rows.Add(12, "Bachelor of Arts in Political Science (BAPS)");
            dtCourses.Rows.Add(12, "Bachelor of Arts in Political Economy (BAPE)");
            dtCourses.Rows.Add(12, "Bachelor of Arts in International Studies (BAIS)");
            dtCourses.Rows.Add(12, "Bachelor of Public Administration (BPA)");

            dtCourses.Rows.Add(13, "Bachelor of Arts in History (BAH)");
            dtCourses.Rows.Add(13, "Bachelor of Arts in Sociology (BAS)");
            dtCourses.Rows.Add(13, "Bachelor of Science in Cooperatives (BSC)");
            dtCourses.Rows.Add(13, "Bachelor of Science in Economics (BSE)");
            dtCourses.Rows.Add(13, "Bachelor of Science in Psychology (BSPSY)");

            dtCourses.Rows.Add(14, "Bachelor of Science Food Technology (BSFT)");
            dtCourses.Rows.Add(14, "Bachelor of Science in Applied Mathematics (BSAPMATH)");
            dtCourses.Rows.Add(14, "Bachelor of Science in Biology (BSBIO)");
            dtCourses.Rows.Add(14, "Bachelor of Science in Chemistry (BSCHEM)");
            dtCourses.Rows.Add(14, "Bachelor of Science in Mathematics (BSMATH)");
            dtCourses.Rows.Add(14, "Bachelor of Science in Nutrition and Dietetics (BSND)");
            dtCourses.Rows.Add(14, "Bachelor of Science in Physics (BSPHY)");
            dtCourses.Rows.Add(14, "Bachelor of Science in Statistics (BSSTAT)");

            dtCourses.Rows.Add(15, "Bachelor of Science in Hospitality Management (BSHM)");
            dtCourses.Rows.Add(15, "Bachelor of Science in Tourism Management (BSTM)");
            dtCourses.Rows.Add(15, "Bachelor of Science in Transportation Management (BSTRM)");

            dtCourses.Rows.Add(16, "Diploma in Computer Engineering Technology (DCET)");
            dtCourses.Rows.Add(16, "Diploma in Electrical Engineering Technology (DEET)");
            dtCourses.Rows.Add(16, "Diploma in Electronics Engineering Technology (DECET)");
            dtCourses.Rows.Add(16, "Diploma in Information Communication Technology (DICT)");
            dtCourses.Rows.Add(16, "Diploma in Mechanical Engineering Technology (DMET)");
            dtCourses.Rows.Add(16, "Diploma in Office Management (DOMT)");
        }

        #endregion

        #region FUNCTION FOR DEPARTMENT AND COURSE SELECTION LOGIC

        // This method populates a MaterialComboBox with course options based on the selected department.
        // It takes in a DataTable containing department data, the MaterialComboBox for department selection,
        // and the MaterialComboBox for course selection.
        private void Courses(DataTable dtDept, MaterialComboBox dept, MaterialComboBox course)
        {
            // Enable or disable the course MaterialComboBox based on whether a department is selected.
            // If the selected index is 0 (indicating a default/select option), disable the course selection;
            // otherwise, enable it.
            course.Enabled = !(dept.SelectedIndex == 0);

            // Select courses from the dtCourses DataTable where the Course ID (CID) matches the selected department's ID (DID).
            // Set the data source of the course MaterialComboBox to the DataTable containing the specific courses.
            // The .Select() method filters the dtCourses DataTable to include only the rows with matching CID.
            // Then, .CopyToDataTable() creates a new DataTable from the filtered results.
            course.DataSource = dtCourses.Select("CID = " + dtDept.Rows[dept.SelectedIndex]["DID"]).CopyToDataTable();

            // Specify that the "CName" property from the data source should be displayed in the course MaterialComboBox.
            course.DisplayMember = "CName";
        }

        #endregion

        #region FUNCTION FOR DEPARTMENT SELECTION EVENT HANDLERS

        // Event handlers for department selection changes. These methods are triggered when a department is selected
        // in its respective MaterialComboBox. They call the Courses() function to update the course selection based on
        // the chosen department. Each handler is associated with a specific department's data and MaterialComboBoxes.

        private void firstDepartment_SelectedIndexChanged(object sender, EventArgs e) => Courses(firstDept, firstDepartment, firstCourse);
        private void secondDepartment_SelectedIndexChanged(object sender, EventArgs e) => Courses(secondDept, secondDepartment, secondCourse);
        private void thirdDepartment_SelectedIndexChanged(object sender, EventArgs e) => Courses(thirdDept, thirdDepartment, thirdCourse);
        private void fourthDepartment_SelectedIndexChanged(object sender, EventArgs e) => Courses(fourthDept, fourthDepartment, fourthCourse);
        private void fifthDepartment_SelectedIndexChanged(object sender, EventArgs e) => Courses(fifthDept, fifthDepartment, fifthCourse);

        #endregion

        #endregion

        #region FUNCTIONS FOR SUBMIT AND BACK BUTTON

        private void submitCoursesBtn_Click(object sender, EventArgs e)
        {
            if (ValidateSelectedCourses())
            {
                functions.Alert("Submitted Successfully", AlertForm.Type.Success);
                RetrieveSelectedCourses();

                // Calculate the index of the next tab to be displayed.
                int nextTabIndex = enrollmentTab.SelectedIndex + 1;

                enrollmentTab.Selecting -= enrollmentTab_Selecting;
                enrollmentTab.SelectedIndex = nextTabIndex;
                enrollmentTab.Selecting += enrollmentTab_Selecting;
            }
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

        #region FUNCTION TO VALIDATE SELECTED COURSES

        private bool ValidateSelectedCourses()
        {
            // This method is used to validate selected courses before further processing.

            // Iterate through each MaterialComboBox control inside departmentBox.
            foreach (MaterialComboBox comboBox in departmentBox.Controls)
            {
                // Check if the selected index of the current combo box is 0, which usually indicates no valid selection.
                // Return false to indicate that validation failed.
                if (comboBox.SelectedIndex == 0)
                {
                    functions.Alert("Complete the Requirements", AlertForm.Type.Info);
                    return false;
                }
            }

            // Create an array containing the text of five course input fields.
            string[] courses = { firstCourse.Text, secondCourse.Text, thirdCourse.Text, fourthCourse.Text, fifthCourse.Text };

            // Loop through the courses array to compare each course with the others.
            for (int i = 0; i < courses.Length - 1; i++)
            {
                // Start an inner loop to compare the current course (courses[i]) with subsequent courses (courses[j]).
                for (int j = i + 1; j < courses.Length; j++)
                {
                    // Check if the current course is the same as any of the subsequent courses.
                    // Return false to indicate that validation failed.
                    if (courses[i] == courses[j])
                    {
                        functions.Alert("Select the Unselected Course", AlertForm.Type.Error);
                        return false;
                    }
                }
            }

            // Call the PassCoursesInformation method to perform further processing or storage of course information.
            PassCoursesInformation();

            // If all validation checks pass, return true to indicate that selected courses are valid.
            return true;
        }

        #endregion

        #region FUNCTION TO PASS THE COURSES INFORMATION

        private void PassCoursesInformation()
        {
            studentData.FirstCourse = firstCourse.Text;
            studentData.SecondCourse = secondCourse.Text;
            studentData.ThirdCourse = thirdCourse.Text;
            studentData.FourthCourse = fourthCourse.Text;
            studentData.FifthCourse = fifthCourse.Text;
        }

        #endregion

        #endregion

        #region GENERAL FUNCTIONS FOR COURSES SELECTION REVIEW TAB

        #region FUNCTION FOR FINALIZE AND BACK BUTTON

        private void finalizeCoursesBtn_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to continue?", "School Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                functions.Alert("Finalized successfully", AlertForm.Type.Success);

                if (!ifFilled)
                    functions.Alert("Firstly: Complete the Averages", AlertForm.Type.Info);

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

        #region FUNCTION FOR RETRIEVING INFORMATION FROM THE CLASSES

        private void RetrieveSelectedCourses()
        {
            firstCourseBox.Text = studentData.FirstCourse;
            secondCourseBox.Text = studentData.SecondCourse;
            thirdCourseBox.Text = studentData.ThirdCourse;
            fourthCourseBox.Text = studentData.FourthCourse;
            fifthCourseBox.Text = studentData.FifthCourse;
        }

        #endregion

        #endregion

        #region GENERAL FUNCTIONS FOR FINALIZATION TAB

        #region FUNCTIONS FOR SUBMIT AND RESET BUTTONS

        private void submitGradesBtn_Click(object sender, EventArgs e)
        {
            // Iterate through each control (user interface element) within the "gradesAverageCard" container.
            foreach (Control control in gradesAverageCard.Controls)
            {
                // Check if the current control is a MaterialTextBox (a specific type of text input control)
                // and if its Text property is empty or null (no text entered).
                if (control is MaterialTextBox && String.IsNullOrEmpty(control.Text))
                {
                    // If the condition is met, call the "Alert" function with an error message
                    // and an alert type of "Error".
                    functions.Alert("Complete the Requirements", AlertForm.Type.Error);

                    ifFilled = false;
                    // Exit the loop and return from this method.
                    return;
                }
            }

            // Assign the value from the specific text box to the specific student data object.
            studentData.FirstSem11Avg = firstSem11Box.Text;
            studentData.SecondSem11Avg = secondSem11Box.Text;
            studentData.FirstSem12Avg = firstSem12Box.Text;
            studentData.SecondSem12Avg = secondSem12Box.Text;

            ifFilled = true;
            // Load the QR code image and the text boxes for enrolee number and password.
            EnroleeAccountQRLoad();
        }

        private void resetGradesBtn_Click(object sender, EventArgs e)
        {
            // Iterate through each control within the "gradesAverageCard" container.
            // Check if the current control is a MaterialTextBox.
            // If true, set the text property of the MaterialTextBox control to an empty string,
            // effectively clearing the text entered by the user.
            foreach (Control control in gradesAverageCard.Controls)
                if (control is MaterialTextBox)
                    control.Text = "";
        }

        #endregion

        #region FUNCTION TO LOAD THE QR CODE AND ENROLEE ACCOUNT

        private void EnroleeAccountQRLoad()
        {
            FlashingScreenForm flashingScreenForm = new FlashingScreenForm();
            flashingScreenForm.ShowDialog();

            // Display an informational alert message with the text "Secondly: Save the QR Code".
            functions.Alert("Secondly: Save the QR Code", AlertForm.Type.Info);

            // Enable the "saveQrBtn" button, the "enroleeNumBox" textbox, and the "tempPassBox" textbox to allow user interaction.
            saveQrBtn.Enabled = true;
            enroleeNumBox.Enabled = true;
            tempPassBox.Enabled = true;

            // Generate an enrollment number and a temporary password using custom functions and store them in variables.
            string enroleeNumber = functions.GenerateEnroleeNumber(false, ref nums);
            string enroleePassword = functions.GenerateRandomPassword();

            enroleeNumBox.Text = enroleeNumber;
            tempPassBox.Text = enroleePassword;

            string qrText = $"Enrolee Number: {enroleeNumber}\nTemporary Password: {enroleePassword}";

            // Generate a QR code image using the "GetCode" function and the "qrText" as data, then display it in the "qrCodeBox" control.
            qrCodeBox.Image = functions.GetCode(qrText);
        }

        #endregion

        #region FUNCTIONS TO SAVE THE QR CODE IMAGE AND UNDERSTAND BUTTON

        private void saveQrBtn_Click(object sender, EventArgs e)
        {
            // Get the image from the qrCodeBox control.
            Image imageToSave = qrCodeBox.Image;

            // Using a SaveFileDialog to prompt the user for the save location and filename.
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                // Set the initial directory to the My Pictures folder.
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

                // Set the default filename for the saved image, including student data.
                // Filter the file types that can be saved (JPEG images in this case).
                saveFileDialog.FileName = $"PUPSIS_{studentData.LastName}.jpg";
                saveFileDialog.Filter = "JPEG Image|*.jpg";

                DialogResult result = saveFileDialog.ShowDialog();

                // Check if the user clicked the OK button in the SaveFileDialog.
                if (result == DialogResult.OK)
                {
                    try
                    {
                        // Get the chosen save path and filename.
                        string savePath = saveFileDialog.FileName;

                        // Save the image to the specified path.
                        imageToSave.Save(savePath);

                        functions.Alert("Image saved successfully!", AlertForm.Type.Success);
                        functions.Alert("Thirdly: Understand the READ ME", AlertForm.Type.Info);

                        understandBtn.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving image: {ex.Message}", "PUP-SIS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void understandBtn_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you really understand?", "PUP-SIS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                // Set the EnroleeNumber property of the studentData object to the text in enroleeNumBox.
                // Set the TemporaryPassword property of the studentData object to the text in tempPassBox.
                studentData.EnroleeNumber = enroleeNumBox.Text;
                studentData.TemporaryPassword = tempPassBox.Text;

                finalizeAllBtn.Enabled = true;

                functions.Alert("Lastly: Finalize the Enrollment", AlertForm.Type.Info);
            }
        }

        #endregion

        #region FUNCTIONS FOR FINALIZE AND BACK BUTTONS

        private void backFinalizationBtn_Click(object sender, EventArgs e)
        {
            // Calculate the index of the previous tab to be displayed.
            int nextTabIndex = enrollmentTab.SelectedIndex - 1;

            enrollmentTab.Selecting -= enrollmentTab_Selecting;
            enrollmentTab.SelectedIndex = nextTabIndex;
            enrollmentTab.Selecting += enrollmentTab_Selecting;
        }

        private void finalizeAllBtn_Click(object sender, EventArgs e)
        {
            functions.Alert("Finalizing Everything....", AlertForm.Type.Info);

            FlashingScreenForm flashingScreenForm = new FlashingScreenForm();
            flashingScreenForm.ShowDialog();

            DatabaseAccess();

            finalizeAllBtn.Enabled = false;
        }

        #region FUNCTION TO ACCESS THE DATABASE AND STORE THE ENROLEE'S INFORMATION

        private void DatabaseAccess()
        {
            DBAccess objDBAccess = new DBAccess(); // Create an instance of the DBAccess class for database operations.

            // Create a SQL command for inserting data into the database.
            SqlCommand insertCommand = new SqlCommand("insert into ApplicantData(LastName,FirstName,MiddleName,ExtName,BirtthDate,BirthPlace,Age,Gender,TelNumber,ContactNumber,Email,FacebookLink,StreetNum,StreetName,Barangay,City,Province,PStreetNum,PStreetName,PBarangay,PCity,PProvince,MLName,MFName,MMName,MConNum,FLName,FFName,FMName,FExtName,FConNum,GLName,GFName,GMName,GExtName,GConNum,SHighSchool,JHighSchool,ElemSchool,LRN,FPS,IP,FirstCourse,SecondCourse,ThirdCourse,FourthCourse,FifthCourse,ElevenGrade,TwelveGrade,UserPhoto,EnroleeNumber,Password, Approved) values(@LastName,@FirstName,@MiddleName,@ExtName,@BirtthDate,@BirthPlace,@Age,@Gender,@TelNumber,@ContactNumber,@Email,@FacebookLink,@StreetNum,@StreetName,@Barangay,@City,@Province,@PStreetNum,@PStreetName,@PBarangay,@PCity,@PProvince,@MLName,@MFName,@MMName,@MConNum,@FLName,@FFName,@FMName,@FExtName,@FConNum,@GLName,@GFName,@GMName,@GExtName,@GConNum,@SHighSchool,@JHighSchool,@ElemSchool,@LRN,@FPS,@IP,@FirstCourse,@SecondCourse,@ThirdCourse,@FourthCourse,@FifthCourse,@ElevenGrade,@TwelveGrade,@UserPhoto,@EnroleeNumber,@Password, @Approved)");

            // Add parameters to the insert command with values from studentData, motherData, fatherData, etc.
            // Parameters are placeholders for data that will be inserted into the database.
            insertCommand.Parameters.AddWithValue("@LastName", studentData.LastName);
            insertCommand.Parameters.AddWithValue("@FirstName", studentData.FirstName);
            insertCommand.Parameters.AddWithValue("@MiddleName", studentData.MiddleName);
            insertCommand.Parameters.AddWithValue("@ExtName", studentData.ExtensionName);
            insertCommand.Parameters.AddWithValue("@BirtthDate", studentData.BirthDate);
            insertCommand.Parameters.AddWithValue("@BirthPlace", studentData.BirthPlace);
            insertCommand.Parameters.AddWithValue("@Age", studentData.Age);
            insertCommand.Parameters.AddWithValue("@Gender", studentData.Gender);
            insertCommand.Parameters.AddWithValue("@TelNumber", studentData.LandlineNumber);
            insertCommand.Parameters.AddWithValue("@ContactNumber", studentData.ContactNumber);
            insertCommand.Parameters.AddWithValue("@Email", studentData.GmailAddress);
            insertCommand.Parameters.AddWithValue("@FacebookLink", studentData.FacebookLink);
            insertCommand.Parameters.AddWithValue("@StreetNum", studentData.StreetNumber);
            insertCommand.Parameters.AddWithValue("@StreetName", studentData.StreetName);
            insertCommand.Parameters.AddWithValue("@Barangay", studentData.Barangay);
            insertCommand.Parameters.AddWithValue("@City", studentData.City);
            insertCommand.Parameters.AddWithValue("@Province", studentData.Province);
            insertCommand.Parameters.AddWithValue("@PStreetNum", studentData.PStreetNumber);
            insertCommand.Parameters.AddWithValue("@PStreetName", studentData.PStreetName);
            insertCommand.Parameters.AddWithValue("@PBarangay", studentData.PBarangay);
            insertCommand.Parameters.AddWithValue("@PCity", studentData.PCity);
            insertCommand.Parameters.AddWithValue("@PProvince", studentData.PProvince);
            insertCommand.Parameters.AddWithValue("@MLName", motherData.LastName);
            insertCommand.Parameters.AddWithValue("@MFName", motherData.FirstName);
            insertCommand.Parameters.AddWithValue("@MMName", motherData.MiddleName);
            insertCommand.Parameters.AddWithValue("@MConNum", motherData.ContactNumber);
            insertCommand.Parameters.AddWithValue("@FLName", fatherData.LastName);
            insertCommand.Parameters.AddWithValue("@FFName", fatherData.FirstName);
            insertCommand.Parameters.AddWithValue("@FMName", fatherData.MiddleName);
            insertCommand.Parameters.AddWithValue("@FExtName", fatherData.ExtensionName);
            insertCommand.Parameters.AddWithValue("@FConNum", fatherData.ContactNumber);
            insertCommand.Parameters.AddWithValue("@GLName", guardianData.LastName);
            insertCommand.Parameters.AddWithValue("@GFName", guardianData.FirstName);
            insertCommand.Parameters.AddWithValue("@GMName", guardianData.MiddleName);
            insertCommand.Parameters.AddWithValue("@GExtName", guardianData.ExtensionName);
            insertCommand.Parameters.AddWithValue("@GConNum", guardianData.ContactNumber);
            insertCommand.Parameters.AddWithValue("@SHighSchool", studentData.SeniorHighSchool);
            insertCommand.Parameters.AddWithValue("@JHighSchool", studentData.HighSchool);
            insertCommand.Parameters.AddWithValue("@ElemSchool", studentData.ElementarySchool);
            insertCommand.Parameters.AddWithValue("@LRN", studentData.LRN);
            insertCommand.Parameters.AddWithValue("@FPS", studentData.FPSNumber);
            insertCommand.Parameters.AddWithValue("@IP", studentData.IPCommunity);
            insertCommand.Parameters.AddWithValue("@FirstCourse", studentData.FirstCourse);
            insertCommand.Parameters.AddWithValue("@SecondCourse", studentData.SecondCourse);
            insertCommand.Parameters.AddWithValue("@ThirdCourse", studentData.ThirdCourse);
            insertCommand.Parameters.AddWithValue("@FourthCourse", studentData.FourthCourse);
            insertCommand.Parameters.AddWithValue("@FifthCourse", studentData.FifthCourse);
            insertCommand.Parameters.AddWithValue("@ElevenGrade", studentData.SecondSem11Avg);
            insertCommand.Parameters.AddWithValue("@TwelveGrade", studentData.SecondSem12Avg);
            insertCommand.Parameters.AddWithValue("@UserPhoto", functions.getPhoto(studentData.Image));
            insertCommand.Parameters.AddWithValue("@EnroleeNumber", studentData.EnroleeNumber);
            insertCommand.Parameters.AddWithValue("@Password", studentData.TemporaryPassword);
            insertCommand.Parameters.AddWithValue("@Approved", false);

            // Execute the insert command to insert data into the database.
            int row = objDBAccess.executeQuery(insertCommand);

            if (row == 1)
            {
                // If the insertion was successful, display a success message.
                functions.Alert("Finalized Successfully", AlertForm.Type.Success);
                objDBAccess.closeConn(); // Close the database connection.

                // Update the ApplicantNumber in the database.
                string query = "Update ApplicantNumber SET Number = '" + @nums + "'";

                SqlCommand updateCommand = new SqlCommand(query);
                updateCommand.Parameters.AddWithValue("@nums", @nums);

                objDBAccess.executeQuery(updateCommand);
                objDBAccess.closeConn(); // Close the database connection.

                this.Hide();
                var ELoginForm = new ELoginForm();
                ELoginForm.FormClosed += (s, args) => this.Close();
                ELoginForm.Show();
            }
            else // If the insertion failed, display an error message and enable the finalizeAllBtn.
            { 
                functions.Alert("Error Occured. Try Again!", AlertForm.Type.Error);
                finalizeAllBtn.Enabled = true;
            }                
        }

        #endregion

        #endregion

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
