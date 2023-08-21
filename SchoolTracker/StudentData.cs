using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolTracker
{
    public class StudentData
    {
        private string _phoneNumber;

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                // Check if the provided value matches the pattern for valid phone numbers
                if (IsValidPhoneNumber(value))
                    _phoneNumber = value;
                else
                    MessageBox.Show("Invalid phone number. Please enter a valid Philippine phone number.", "PUP-SIS", MessageBoxButtons.OK, MessageBoxIcon.Error);          
            }
        }

        // Check if the provided phone number is valid according to the Philippine phone number format.
        private bool IsValidPhoneNumber(string phoneNumber) => Regex.IsMatch(phoneNumber, @"^(09\d{9}|(\+63|0)[2-8]\d{7})$");
    }
}
