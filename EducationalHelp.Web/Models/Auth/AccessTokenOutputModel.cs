using EducationalHelp.Services.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalHelp.Web.Models.Auth
{
    public class AccessTokenOutputModel
    {
        public string AccessToken { get; set; }
        public string Login { get; set; }

        public AccessTokenOutputModel(string accessToken, string login)
        {
            AccessToken = accessToken;
            Login = login;
        }

        public AccessTokenOutputModel(string accessToken, UserCredentials credentials)
        {
            AccessToken = accessToken;
            Login = credentials.Login;
        }
    }
}
