using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolTracker
{
    // Abstract base class that defines common properties and behavior for personal data
    abstract class PersonData
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        private string _contactNumber;

        public string ContactNumber
        {
            get { return _contactNumber; }
            set
            {
                string pattern = @"^(09\d{9}|(\+63|0)[2-8]\d{7})$";
                // Check if the provided value matches the pattern for valid phone numbers
                if (Regex.IsMatch(value, pattern))
                    _contactNumber = value;
                else
                    MessageBox.Show("Invalid phone number. Please enter a valid Philippine phone number.", "PUP-SIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    class StudentData : PersonData
    {
        
    }

    class MotherData : PersonData
    {

    }

    class FatherData : PersonData
    {

    }

    class GuardianData : PersonData
    {

    }
}
