﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolTracker
{
    class Functionality
    {
        public void OpenWeb(int num)
        {
            string[] urls = { "https://www.pup.edu.ph/", "https://www.pup.edu.ph/terms/", "https://www.pup.edu.ph/privacy/", "https://login.microsoftonline.com/common/oauth2/authorize?client_id=c9a559d2-7aab-4f13-a6ed-e7e9c52aec87&resource=c9a559d2-7aab-4f13-a6ed-e7e9c52aec87&response_type=code%20id_token&scope=openid%20profile&state=OpenIdConnect.AuthenticationProperties%3DeyJ2ZXJzaW9uIjoxLCJkYXRhIjp7IklkZW50aXR5UHJvdmlkZXIiOiJBUWlFUTNsTk1vREpEZk1ubG5pNVdzRDdZVWgxZE1sd2NkdElGeU9Hb0llMllBUURQV3c2cVNjY0s3RDlnRkJyUVJXVThCalU3R0VUa2gzNDRXMkxvVXciLCIucmVkaXJlY3QiOiJodHRwczovL2Zvcm1zLm9mZmljZS5jb20vUGFnZXMvUmVzcG9uc2VQYWdlLmFzcHg_aWQ9Y1lXcFRlcmNPVWlQc1F2ZFhjbHAtVlMyTmdXbHlFZEh2TnRnQkJVQjRCZFVRazFXT0VSUlZVbzRXamhTU2pCT1dsaFhNRWN4VlVwWlR5NHUmc2lkPTZmM2MwNWI3LWU4ZmEtNGI0Ny05MGJhLTQzMjUyMjk5YTM4MyJ9fQ&response_mode=form_post&nonce=638218355774463644.Yjc5ZDQ2MTAtM2U3Yi00YmYyLTgxNWQtMjQ4YTMzN2E2ZjM0MmZjNjdiYmUtYjNmYS00MzI2LWFkOTEtZmI5ZjMyZWZkMTg1&redirect_uri=https%3A%2F%2Fforms.office.com%2Flanding&msafed=0&x-client-SKU=ID_NET472&x-client-ver=6.16.0.0&sso_reload=true" };
            string websiteUrl = urls[num];

            try
            {
                System.Diagnostics.Process.Start(websiteUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening website: {ex.Message}", "Open Website", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
