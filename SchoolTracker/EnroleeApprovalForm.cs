using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolTracker
{
    public partial class EnroleeApprovalForm : MaterialForm
    {
        StudentData studentData = new StudentData();
        Functionality functions = new Functionality();

        public EnroleeApprovalForm()
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

        private void EnroleeApprovalForm_Load(object sender, EventArgs e)
        {
        }

        private void saveQRBtn_Click(object sender, EventArgs e)
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

                        functions.Alert("QR Code Saved Successfully", AlertForm.Type.Success);

                        understandBtn.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving image: {ex.Message}", "PUP-SIS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }


    }
}
