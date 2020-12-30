using EducationalHelp.Core.Entities;
using EducationalHelp.Data;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using EducationalHelp.Services.Exceptions;
using System.Security.Claims;

namespace EducationalHelp.Services.Profile
{
    public abstract class BaseAuthenticationService
    {
        protected readonly UserService _userService; 

        public BaseAuthenticationService(UserService userService)
        {
            _userService = userService; 
        }

        public Guid GetUserIdFromClaims(IEnumerable<Claim> claims)
        {
            var identifierClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (identifierClaim == null)
            {
                string claimsSerialized = "";
                foreach (var claim in claims)
                {
                    claimsSerialized += string.Format("{0}:{1}", claim.Type, claim.Value);
                }
                    
                throw new ResourceNotFoundException($"User with claims ({claims.Count()}): \n{claimsSerialized}\n wasn't found!");
            }

            return new Guid(identifierClaim.Value);
        }

        protected bool CheckCredentials(UserCredentials credentials)
        {
            var user = _userService.GetUserByName(credentials.Pseudonym);

            var hashedPass = BaseAuthenticationService.GetSHA256(credentials.Password);

            return user.PassHash.Equals(hashedPass, StringComparison.Ordinal);
        }

        protected User UpdateCredentials(User user, UserCredentials credentials)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (String.IsNullOrEmpty(credentials.Password))
            {
                throw new ArgumentNullException(nameof(credentials.Password));
            }

            if (String.IsNullOrEmpty(credentials.Pseudonym))
            {
                throw new ArgumentNullException(nameof(credentials.Pseudonym));
            }

            user.Pseudonym = credentials.Pseudonym;
            user.PassHash = GetSHA256(credentials.Password);

            return user;
        }

        protected Claim[] GetUserClaims(User user)
        {
            return new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Pseudonym),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
           
        }

        private static string GetSHA256(string text)
        {
            var encoding = new UTF8Encoding();
            var textBytes = encoding.GetBytes(text);
            byte[] hashBytes;

            using var hash = new SHA256Managed();
            hashBytes = hash.ComputeHash(textBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}
