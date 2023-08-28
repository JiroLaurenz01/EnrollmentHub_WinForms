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
                // Regular expression pattern for a valid Philippine contact number.
                string pattern = @"^(09\d{9}|(\+63|0)[2-8]\d{7})$";

                Validate(pattern, ref _contactNumber, value, "phone number", "Philippine phone number");
            }
        }

        // This method is defined to validate a given value against a regular expression pattern.
        // If the value matches the pattern, it's assigned to the specified privateHolder variable.
        // If the value doesn't match, an error message is displayed and the privateHolder variable is set to null.
        public void Validate(string pattern, ref string privateHolder, string value, string name, string secondName)
        {
            // Check if the provided value matches the specified regular expression pattern.
            if (Regex.IsMatch(value, pattern))
                privateHolder = value; // Assign the value to the privateHolder if it's valid.
            else
            {
                // Display an error message using string interpolation to include the invalid value and the expected type.
                MessageBox.Show($"Invalid {name}. Please enter a valid {secondName}.", "PUP-SIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                privateHolder = null; // Set the privateHolder to null since the value is invalid.
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
                // Regular expression pattern for a valid Philippine landline number.
                string pattern = @"^(02|0[3-8])\d{7}$";

                Validate(pattern, ref _landlineNumber, value, "landline number", "Philippine landline number");
            }
        }

        private string _gmailAddress;

        public string GmailAddress
        {
            get { return _gmailAddress; }
            set
            {
                // Regular expression pattern for a valid Gmail address.
                string pattern = @"^[a-zA-Z0-9._%+-]+@gmail\.com$";

                Validate(pattern, ref _gmailAddress, value, "Gmail address", "address");
            }
        }

        private string _facebookLink;

        public string FacebookLink
        {
            get { return _facebookLink; }
            set
            {
                // Regular expression pattern for a valid Facebook profile link.
                string pattern = @"^(https?://)?(www\.)?facebook\.com/[\w.-]+/?$";

                Validate(pattern, ref _facebookLink, value, "Facebook link", "link");
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

        public string FirstCourse { get; set; }
        public string SecondCourse { get; set; }
        public string ThirdCourse { get; set; }
        public string FourthCourse { get; set; }
        public string FifthCourse { get; set; }

        public string ApprovedCourse { get; set; }
    }

    class MotherData : PersonData { }
    class FatherData : PersonData { public string ExtensionName { get; set; } }
    class GuardianData : PersonData { public string ExtensionName { get; set; } }
}
