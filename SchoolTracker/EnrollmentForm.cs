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

        #region FUNCTION TO CHANGE THE THEME COLOR [LIGHT/DARK]

        // This method is an event handler that is triggered when the state of the thSwitch (toggle switch) changes.
        private void thSwitch_CheckedChanged(object sender, EventArgs e)
        {
            // Get the current MaterialSkinManager instance to manage the application's visual theme.
            var skin = MaterialSkinManager.Instance;

            // Determine whether the current theme is light or dark.
            bool isLight = skin.Theme == MaterialSkinManager.Themes.LIGHT;

            // If the current theme is light, switch to dark theme and update the text of the toggle switch.
            if (isLight)
            {
                skin.Theme = MaterialSkinManager.Themes.DARK;  
                thSwitch.Text = "DARK MODE"; 
            }
            // If the current theme is dark, switch to light theme and update the text of the toggle switch.
            else
            {
                skin.Theme = MaterialSkinManager.Themes.LIGHT; 
                thSwitch.Text = "LIGHT MODE";
            }
        }

        #endregion

        #region FUNCTIONS TO OPEN SPECIFIC WEBSITE

        private void openWebBtn_Click(object sender, EventArgs e) => functions.OpenWeb(0);
        private void termUseBtn_Click(object sender, EventArgs e) => functions.OpenWeb(1);
        private void privacyStateBtn_Click(object sender, EventArgs e) => functions.OpenWeb(2);

        #endregion

        #region FUNCTIONS FOR CHECKBOX

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
            bool ifChecked = !materialCheckbox.Checked;

            // Perform different enabling/disabling logic based on the checked checkbox.
            switch (checkedCheckBox)
            {
                case "mNoneBtn":
                    materialTextBoxList.Add(mLNameBox);
                    materialTextBoxList.Add(mFNameBox);
                    materialTextBoxList.Add(mMNameBox);
                    materialTextBoxList.Add(mCNumBox);
                    break;

                case "fNoneBtn":
                    materialTextBoxList.Add(fLNameBox);
                    materialTextBoxList.Add(fFNameBox);
                    materialTextBoxList.Add(fMNameBox);
                    materialTextBoxList.Add(fCNumBox);

                    fExtNameCBox.Enabled = ifChecked;

                    if (!ifChecked)
                        fExtNameCBox.SelectedIndex = 0;
                    break;

                case "gNoneBtn":
                    materialTextBoxList.Add(gLNameBox);
                    materialTextBoxList.Add(gFNameBox);
                    materialTextBoxList.Add(gMNameBox);
                    materialTextBoxList.Add(gCNumBox);

                    gExtNameCBox.Enabled = ifChecked;

                    if (!ifChecked)
                        gExtNameCBox.SelectedIndex = 0;

                    gMotherBtn.Enabled = ifChecked;
                    gFatherBtn.Enabled = ifChecked;

                    if (!ifChecked)
                    {
                        gMotherBtn.Checked = false;
                        gFatherBtn.Checked = false;
                    }
                    break;
            }

            // Iterate through the list of MaterialTextBox controls and enable/disable them accordingly.
            foreach (MaterialTextBox textBox in materialTextBoxList)
            {
                textBox.Enabled = ifChecked;

                // If the checkbox is unchecked, clear the content of the MaterialTextBox.
                if (!ifChecked)
                    textBox.Clear();
            }
        }


        #endregion


        #region FUNCTION TO HANDLE (IF MOTHER/FATHER -> GUARDIAN) CHECKBOXES LOGIC

        private void ifMotherFatherIsAGuardian_CheckedChanged(object sender, EventArgs e)
        {
            // Cast the sender object to a MaterialCheckbox, representing the checkbox that triggered the event.
            MaterialCheckbox materialCheckbox = (MaterialCheckbox)sender;

            List<MaterialTextBox> materialTextBoxList = new List<MaterialTextBox>();

            // Get the name of the checked checkbox and calculate the inverse of its checked state.
            string checkedCheckBox = materialCheckbox.Name;
            bool ifChecked = !materialCheckbox.Checked;

            // Perform different enabling/disabling logic based on the checked checkbox.
            switch (checkedCheckBox)
            {
                case "mNoneBtn":
                    materialTextBoxList.Clear();

                    materialTextBoxList.Add(mLNameBox);
                    materialTextBoxList.Add(mFNameBox);
                    materialTextBoxList.Add(mMNameBox);
                    materialTextBoxList.Add(mCNumBox);
                    break;

                case "fNoneBtn":
                    fLNameBox.Enabled = ifChecked;
                    fFNameBox.Enabled = ifChecked;
                    fMNameBox.Enabled = ifChecked;
                    fCNumBox.Enabled = ifChecked;
                    fExtNameCBox.Enabled = ifChecked;
                    break;
                case "gNoneBtn":
                    gLNameBox.Enabled = ifChecked;
                    gFNameBox.Enabled = ifChecked;
                    gMNameBox.Enabled = ifChecked;
                    gCNumBox.Enabled = ifChecked;
                    gExtNameCBox.Enabled = ifChecked;

                    gMotherBtn.Enabled = ifChecked;
                    gFatherBtn.Enabled = ifChecked;

                    if (!ifChecked)
                    {
                        gMotherBtn.Checked = false;
                        gFatherBtn.Checked = false;
                    }
                    break;
            }

            foreach (MaterialTextBox textBox in materialTextBoxList)
            {
                textBox.Enabled = ifChecked;
                MessageBox.Show("1");
            }
        }

        #endregion

        #endregion

    }
}
