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
    public partial class AlertForm : Form
    {
        private int x, y;

        public enum Action
        {
            wait,
            start,
            close
        }

        public enum Type
        {
            Success,
            Warning,
            Error,
            Info
        }

        private AlertForm.Action action;


        public AlertForm()
        {
            InitializeComponent();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            switch (action)
            {
                case Action.wait:
                    timer.Interval = 5000;
                    action = Action.close;
                    break;
                case Action.start:
                    timer.Interval = 1;
                    Opacity += 0.1;

                    if (x < Location.X)
                        Left--;
                    else
                        if (Opacity == 1.0)
                            action = Action.wait;
                    break;
                case Action.close:
                    timer.Interval = 1;
                    Opacity -= 0.1;

                    Left -= 3;
                    if (base.Opacity == 0.0)
                        base.Close();
                    break;
            }
        }

        private void removeBtn_Click(object sender, EventArgs e)
        {
            timer.Interval = 1;
            action = Action.close;
        }

        public void ShowAlert(string msg, Type type)
        {
            Opacity = 0.0;
            StartPosition = FormStartPosition.Manual;
            string fname;

            for (int i = 1; i < 10; i++)
            {
                fname = "alert" + i.ToString();
                AlertForm form = (AlertForm)Application.OpenForms[fname];

                if (form == null)
                {
                    Name = fname;
                    x = Screen.PrimaryScreen.WorkingArea.Width - Width + 15;
                    y = Screen.PrimaryScreen.WorkingArea.Height - Height * i - 5 * i;
                    Location = new Point(this.x, this.y);

                    break;
                }

            }

            x = Screen.PrimaryScreen.WorkingArea.Width - base.Width - 5;

            switch (type)
            {
                case Type.Success:
                    pictureBox1.Image = SchoolTracker.Properties.Resources.success;
                    BackColor = Color.SeaGreen;
                    break;
                case Type.Error:
                    pictureBox1.Image = SchoolTracker.Properties.Resources.error;
                    BackColor = Color.DarkRed;
                    break;
                case Type.Info:
                    pictureBox1.Image = SchoolTracker.Properties.Resources.info;
                    BackColor = Color.RoyalBlue;
                    break;
                case Type.Warning:
                    pictureBox1.Image = SchoolTracker.Properties.Resources.warning;
                    BackColor = Color.DarkOrange;
                    break;
            }


            labelMessage.Text = msg;

            Show();
            action = Action.start;
            timer.Interval = 1;
            timer.Start();
        }
    }
}
