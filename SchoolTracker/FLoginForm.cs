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
    }
}
