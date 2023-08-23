using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolTracker
{
    public partial class EnrollmentForm : MaterialForm
    {
        Functionality functions = new Functionality();

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

        #region FUNCTIONS FOR SWITCHES

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
                fpsTextBox.Enabled = ifChecked;
            else
                ipTextBox.Enabled = ifChecked;
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

                    gExtNameCBox.Enabled = !ifChecked;

                    if (ifChecked)
                        gExtNameCBox.SelectedIndex = 0;

                    if (!mNoneBtn.Checked)
                        gMotherBtn.Enabled = !ifChecked;

                    if (!fNoneBtn.Checked)
                        gFatherBtn.Enabled = !ifChecked;

                    if (ifChecked)
                    {
                        gMotherBtn.Checked = false;
                        gFatherBtn.Checked = false;
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

                    gExtNameCBox.Enabled = !ifChecked;
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

                        // Note: The commented line below seems to be related to storing the user's image elsewhere.
                        // UD.userImage = studentPicture.Image;
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

        #region FUNCTION FOR VALIDATING THE INFORMATIONS BEFORE THE APPROVAL OF SUBMISSION

        private void submitInfoBtn_Click(object sender, EventArgs e)
        {
            StudentData studentData = new StudentData();
            MotherData motherData = new MotherData();
            FatherData fatherData = new FatherData();
            GuardianData guardianData = new GuardianData();

            // Create an array of base class type PersonData to store instances of different people
            PersonData[] personDatas = new PersonData[]
            {
                        studentData,
                        motherData,
                        fatherData,
                        guardianData
            };

            // Initialize an index to keep track of the current person in the array
            int indexPerson = 0;
            bool ifReturn = false;

            // Loop through each control in reverse order within the "basicInfoTab" control container
            foreach (Control textBox in basicInfoTab.Controls.Cast<Control>().Reverse())
            {
                if (textBox is MaterialTextBox)
                {
                    // Check if the control is for the contact number
                    if (textBox.Name.Contains("CNumBox"))
                    {
                        if (string.IsNullOrEmpty(textBox.Text))
                        {
                            MessageBox.Show("Phone number is empty. Please enter a phone number.", "PUP-SIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textBox.Focus();
                            return;
                        }
                        else
                        {
                            // Set the ContactNumber property of the current person's data based on the index
                            personDatas[indexPerson].ContactNumber = textBox.Text.Trim();

                            if (string.IsNullOrEmpty(personDatas[indexPerson].ContactNumber))
                            {
                                textBox.Focus();
                                return;
                            }
                        }

                        // Move to the next person in the array
                        indexPerson++;
                    }
                    // Check if the control is for the landline number
                    else if (textBox == landlineNumBox)
                        ifReturn = ValidateAndAssign(textBox, studentData, "Landline number", "LandlineNumber");
                    // Check if the control is for the gmail address
                    else if (textBox == gmailAddBox)
                        ifReturn = ValidateAndAssign(textBox, studentData, "Gmail address", "GmailAddress");
                   
                    if (ifReturn)
                        return;
                }
            }
        }

        // It will return true if there is an issue; otherwise, it will return false.
        private bool ValidateAndAssign(Control textBox, StudentData studentData, string fieldName, string property)
        {
            string input = textBox.Text.Trim();

            // Show an error message using the field name if the input is null or empty, then focus on the textbox and return true (indicating validation failure)
            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show($"{fieldName} is empty. Please enter a {fieldName}.", "PUP-SIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Focus();

                return true;
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

                    return true;
                }
            }

            return false;
        }

        #endregion

        #endregion

        #region FUNCTIONS TO OPEN VARIOUS WEBSITES

        private void openWebBtn_Click(object sender, EventArgs e) => functions.OpenWeb(0);
        private void termUseBtn_Click(object sender, EventArgs e) => functions.OpenWeb(1);
        private void privacyStateBtn_Click(object sender, EventArgs e) => functions.OpenWeb(2);


        #endregion

    }
}
