using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static QRCoder.PayloadGenerator;

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
        private string _landlineNumber;

        public string LandlineNumber
        {
            get { return _landlineNumber; }
            set
            {
                string pattern = @"^(02|0[3-8])\d{7}$";

                // Check if the provided value matches the pattern for valid landline numbers
                if (Regex.IsMatch(value, pattern))
                    _landlineNumber = value;
                else
                    MessageBox.Show("Invalid landline number. Please enter a valid Philippine landline number.", "PUP-SIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string _gmailAddress;

        public string GmailAddress
        {
            get { return _gmailAddress; }
            set
            {
                string pattern = @"^[a-zA-Z0-9._%+-]+@gmail\.com$";

                // Check if the provided value matches the pattern for valid gmail address
                if (Regex.IsMatch(value, pattern))
                    _gmailAddress = value;
                else
                    MessageBox.Show("Invalid gmail address. Please enter a valid gmail address.", "PUP-SIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    class MotherData : PersonData { }
    class FatherData : PersonData { }
    class GuardianData : PersonData { }
}
