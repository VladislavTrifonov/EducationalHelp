using System;
using System.Collections.Generic;
using System.Text;

namespace EducationalHelp.Services.Profile
{
    public struct UserCredentials
    {
        public string Pseudonym { get; set; }
        public string Password { get; set; }

        public UserCredentials(string login, string password)
        {
            Pseudonym = login;
            Password = password;
        }
    }
}
