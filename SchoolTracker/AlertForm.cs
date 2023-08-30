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
    }
}
