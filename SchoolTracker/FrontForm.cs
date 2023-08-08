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
    public partial class FrontForm : MaterialForm
    {
        public FrontForm()
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

        #region OTHER FEATURES

        private void facultyBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            var FLoginForm = new FLoginForm();
            FLoginForm.FormClosed += (s, args) => this.Close();
            FLoginForm.Show();
        }

        private void enroleeBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            var EnrollmentForm = new EnrollmentForm();
            EnrollmentForm.FormClosed += (s, args) => this.Close();
            EnrollmentForm.Show();
        }

        #endregion
    }
}
