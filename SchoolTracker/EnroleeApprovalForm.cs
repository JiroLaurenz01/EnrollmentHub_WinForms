using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolTracker
{
    public partial class EnroleeApprovalForm : MaterialForm
    {
        #region CLASSES

        Functionality functions = new Functionality();

        DBAccess objDBAccess = new DBAccess();

        #endregion

        #region FIELDS

        int number = 0;

        DataTable studentDataTable = new DataTable();
        String selectedCourse = "";

        #endregion

        public EnroleeApprovalForm(DataTable StudentDataTable, String SelectedCourse)
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

            studentDataTable = StudentDataTable;
            selectedCourse = SelectedCourse;
        }

        #region FUNCTION TO LOAD THIS FORM

        private void EnroleeApprovalForm_Load(object sender, EventArgs e)
        {
            EnroleeAccountQRLoad();
            ApprovedCourseLoad();
        }

        #endregion

        #region FUNCTION TO LOAD THE APPROVED COURSE

        // Truncates the last 4 characters from 'selectedCourse' and assigns the result to 'enrolledCourse.Text'.
        private void ApprovedCourseLoad() => enrolledCourse.Text = selectedCourse.Substring(0, selectedCourse.Length - 4);

        #endregion

        #region FUNCTION TO LOAD THE QR CODE AND ENROLEE ACCOUNT

        private void EnroleeAccountQRLoad()
        {
            // Generate an student number and a random password using custom functions and store them in variables.
            string studentNumber = functions.GenerateEnroleeNumber(true, ref number);
            string studentPassword = functions.GenerateRandomPassword();

            studentNumberBox.Text = studentNumber;
            passwordBox.Text = studentPassword;

            string qrText = $"Student Number: {studentNumber}\nPassword: {studentPassword}";

            // Generate a QR code image using the "GetCode" function and the "qrText" as data, then display it in the "qrCodeBox" control.
            qrCodeBox.Image = functions.GetCode(qrText);
        }

        #endregion

        #region FUNCTION TO SAVE THE QR CODE IMAGE

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
                saveFileDialog.FileName = $"PUPSIS_{studentDataTable.Rows[0]["LastName"]}.jpg";
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

        #endregion

        #region FUNCTIONS TO FINALIZE THE APPROVED ENROLLMENT

        private void understandBtn_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you really understand?", "PUP-SIS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
                PassInformationInDatabase(); // If the user confirms understanding, call PassInformationInDatabase method.
        }

        #region FUNCTION TO PASS THE STUDENT INFORMATION IN DATABASE

        private void PassInformationInDatabase()
        {
            // Create a SQL insert command to insert student information into the database.
            SqlCommand insertCommand = new SqlCommand("insert into StudentData(LastName,FirstName,MiddleName,ExtName,BirthDate,BirthPlace,Age,Gender,TelNumber,ContactNumber,Email,FacebookLink,StreetNum,StreetName,Barangay,City,Province,PStreetNum,PStreetName,PBarangay,PCity,PProvince,MLName,MFName,MMName,MConNum,FLName,FFName,FMName,FExtName,FConNum,GLName,GFName,GMName,GExtName,GConNum,SHighSchool,JHighSchool,ElemSchool,LRN,FPS,IP,UserPhoto,StudentNumber,Password,CourseCode,Year,ScholasticStatus,CourseDescription) values(@LastName,@FirstName,@MiddleName,@ExtName,@BirthDate,@BirthPlace,@Age,@Gender,@TelNumber,@ContactNumber,@Email,@FacebookLink,@StreetNum,@StreetName,@Barangay,@City,@Province,@PStreetNum,@PStreetName,@PBarangay,@PCity,@PProvince,@MLName,@MFName,@MMName,@MConNum,@FLName,@FFName,@FMName,@FExtName,@FConNum,@GLName,@GFName,@GMName,@GExtName,@GConNum,@SHighSchool,@JHighSchool,@ElemSchool,@LRN,@FPS,@IP,@UserPhoto,@StudentNumber,@Password,@CourseCode,@Year,@ScholasticStatus,@CourseDescription)");

            // Set parameters for the SQL insert command with values from studentDataTable.
            insertCommand.Parameters.AddWithValue("@LastName", studentDataTable.Rows[0]["LastName"].ToString());
            insertCommand.Parameters.AddWithValue("@FirstName", studentDataTable.Rows[0]["FirstName"].ToString());
            insertCommand.Parameters.AddWithValue("@MiddleName", studentDataTable.Rows[0]["MiddleName"].ToString());
            insertCommand.Parameters.AddWithValue("@ExtName", studentDataTable.Rows[0]["ExtName"].ToString());
            insertCommand.Parameters.AddWithValue("@BirthDate", studentDataTable.Rows[0]["BirtthDate"].ToString());
            insertCommand.Parameters.AddWithValue("@BirthPlace", studentDataTable.Rows[0]["BirthPlace"].ToString());
            insertCommand.Parameters.AddWithValue("@Age", studentDataTable.Rows[0]["Age"].ToString());
            insertCommand.Parameters.AddWithValue("@Gender", studentDataTable.Rows[0]["Gender"].ToString());
            insertCommand.Parameters.AddWithValue("@TelNumber", studentDataTable.Rows[0]["TelNumber"].ToString());
            insertCommand.Parameters.AddWithValue("@ContactNumber", studentDataTable.Rows[0]["ContactNumber"].ToString());
            insertCommand.Parameters.AddWithValue("@Email", studentDataTable.Rows[0]["Email"].ToString());
            insertCommand.Parameters.AddWithValue("@FacebookLink", studentDataTable.Rows[0]["FacebookLink"].ToString());
            insertCommand.Parameters.AddWithValue("@StreetNum", studentDataTable.Rows[0]["StreetNum"].ToString());
            insertCommand.Parameters.AddWithValue("@StreetName", studentDataTable.Rows[0]["StreetName"].ToString());
            insertCommand.Parameters.AddWithValue("@Barangay", studentDataTable.Rows[0]["Barangay"].ToString());
            insertCommand.Parameters.AddWithValue("@City", studentDataTable.Rows[0]["City"].ToString());
            insertCommand.Parameters.AddWithValue("@Province", studentDataTable.Rows[0]["Province"].ToString());
            insertCommand.Parameters.AddWithValue("@PStreetNum", studentDataTable.Rows[0]["PStreetNum"].ToString());
            insertCommand.Parameters.AddWithValue("@PStreetName", studentDataTable.Rows[0]["PStreetName"].ToString());
            insertCommand.Parameters.AddWithValue("@PBarangay", studentDataTable.Rows[0]["PBarangay"].ToString());
            insertCommand.Parameters.AddWithValue("@PCity", studentDataTable.Rows[0]["PCity"].ToString());
            insertCommand.Parameters.AddWithValue("@PProvince", studentDataTable.Rows[0]["PProvince"].ToString());
            insertCommand.Parameters.AddWithValue("@MLName", studentDataTable.Rows[0]["MLName"].ToString());
            insertCommand.Parameters.AddWithValue("@MFName", studentDataTable.Rows[0]["MFName"].ToString());
            insertCommand.Parameters.AddWithValue("@MMName", studentDataTable.Rows[0]["MMName"].ToString());
            insertCommand.Parameters.AddWithValue("@MConNum", studentDataTable.Rows[0]["MConNum"].ToString());
            insertCommand.Parameters.AddWithValue("@FLName", studentDataTable.Rows[0]["FLName"].ToString());
            insertCommand.Parameters.AddWithValue("@FFName", studentDataTable.Rows[0]["FFName"].ToString());
            insertCommand.Parameters.AddWithValue("@FMName", studentDataTable.Rows[0]["FMName"].ToString());
            insertCommand.Parameters.AddWithValue("@FExtName", studentDataTable.Rows[0]["FExtName"].ToString());
            insertCommand.Parameters.AddWithValue("@FConNum", studentDataTable.Rows[0]["FConNum"].ToString());
            insertCommand.Parameters.AddWithValue("@GLName", studentDataTable.Rows[0]["GLName"].ToString());
            insertCommand.Parameters.AddWithValue("@GFName", studentDataTable.Rows[0]["GFName"].ToString());
            insertCommand.Parameters.AddWithValue("@GMName", studentDataTable.Rows[0]["GMName"].ToString());
            insertCommand.Parameters.AddWithValue("@GExtName", studentDataTable.Rows[0]["GExtName"].ToString());
            insertCommand.Parameters.AddWithValue("@GConNum", studentDataTable.Rows[0]["GConNum"].ToString());
            insertCommand.Parameters.AddWithValue("@SHighSchool", studentDataTable.Rows[0]["SHighSchool"].ToString());
            insertCommand.Parameters.AddWithValue("@JHighSchool", studentDataTable.Rows[0]["JHighSchool"].ToString());
            insertCommand.Parameters.AddWithValue("@ElemSchool", studentDataTable.Rows[0]["ElemSchool"].ToString());
            insertCommand.Parameters.AddWithValue("@LRN", studentDataTable.Rows[0]["LRN"].ToString());
            insertCommand.Parameters.AddWithValue("@FPS", studentDataTable.Rows[0]["FPS"].ToString());
            insertCommand.Parameters.AddWithValue("@IP", studentDataTable.Rows[0]["IP"].ToString());
            insertCommand.Parameters.AddWithValue("@UserPhoto", (byte[])studentDataTable.Rows[0]["UserPhoto"]);
            insertCommand.Parameters.AddWithValue("@StudentNumber", studentNumberBox.Text);
            insertCommand.Parameters.AddWithValue("@Password", passwordBox.Text);
            insertCommand.Parameters.AddWithValue("@CourseCode", enrolledCourse.Text.Substring(enrolledCourse.Text.IndexOf("(") + 1, enrolledCourse.Text.IndexOf(")") - enrolledCourse.Text.IndexOf("(") - 1));
            insertCommand.Parameters.AddWithValue("@Year", 1);
            insertCommand.Parameters.AddWithValue("@ScholasticStatus", "Regular");
            insertCommand.Parameters.AddWithValue("@CourseDescription", Regex.Replace(enrolledCourse.Text, " \\([^\\)]*\\)", "").ToUpper());

            // Execute the SQL insert command and store the result in 'row'.
            int row = objDBAccess.executeQuery(insertCommand);

            if (row == 1)
            {
                // Close the database connection.
                objDBAccess.closeConn();

                // Update the StudentNumber table.
                string query = "UPDATE StudentNumber SET Number = '" + @number + "'";
                SqlCommand updateCommand = new SqlCommand(query);
                updateCommand.Parameters.AddWithValue("@nums", @number);
                objDBAccess.executeQuery(updateCommand);
                objDBAccess.closeConn();

                // Delete the record from the ApplicantData table.
                query = "DELETE from ApplicantData WHERE EnroleeNumber = '" + studentDataTable.Rows[0]["EnroleeNumber"].ToString() + "'";
                SqlCommand deleteCommand = new SqlCommand(query);
                row = objDBAccess.executeQuery(deleteCommand);

                if (row == 1)
                {
                    functions.Alert("PADAYON, ISKOLAR NG BAYAN!", AlertForm.Type.Success);

                    this.Hide();
                    var ELoginForm = new ELoginForm();
                    ELoginForm.FormClosed += (s, args) => this.Close();
                    ELoginForm.Show();
                }
                else
                    functions.Alert("Error Occured. Try Again!", AlertForm.Type.Error);
            }
            else
                functions.Alert("Error Occured. Try Again!", AlertForm.Type.Error);
        }

        #endregion

        #endregion
    }
}
