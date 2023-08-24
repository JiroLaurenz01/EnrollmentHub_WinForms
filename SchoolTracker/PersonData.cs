using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

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
        public Image Image { get; set; }
        public string ExtensionName { get; set; }
        public string BirthDate { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
       
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

        private string _facebookLink;

        public string FacebookLink
        {
            get { return _facebookLink; }
            set
            {
                // Regular expression pattern for a valid Facebook profile link
                string pattern = @"^(https?://)?(www\.)?facebook\.com/[\w.-]+/?$";

                if (Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase))
                    _facebookLink = value;
                else
                    MessageBox.Show("Invalid Facebook link. Please enter a valid link.", "PUP-SIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string BirthPlace { get; set; }

        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string Barangay { get; set; }
        public string City { get; set; }
        public string Province { get; set; }

        public string PStreetNumber { get; set; }
        public string PStreetName { get; set; }
        public string PBarangay { get; set; }
        public string PCity { get; set; }
        public string PProvince { get; set; }

        public string ElementarySchool { get; set; }
        public string HighSchool { get; set; }
        public string SeniorHighSchool { get; set; }

        public string LRN { get; set; }
        public string FPSNumber { get; set; }
        public string IPCommunity { get; set; }
    }

    class MotherData : PersonData { }
    class FatherData : PersonData
    {
        public string ExtensionName { get; set; }
    }
    class GuardianData : PersonData
    {
        public string ExtensionName { get; set; }
    }
}
