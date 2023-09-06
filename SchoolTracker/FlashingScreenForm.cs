﻿using System;
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
    public partial class FlashingScreenForm : Form
    {
        public FlashingScreenForm()
        {
            InitializeComponent();
        }

        private void FlashingScreenForm_Load(object sender, EventArgs e)
        {
            // Start the timer when the form loads
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // Close the form when the timer ticks (after 10 seconds)
            Close();
        }
    }
}
