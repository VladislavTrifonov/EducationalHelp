using EducationalHelp.Core.Entities;
using EducationalHelp.Data;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using EducationalHelp.Services.Exceptions;

namespace EducationalHelp.Services.Profile
{
    public abstract class BaseAuthenticationService
    {
        private readonly IRepository<User> _usersRepository;

        public BaseAuthenticationService(IRepository<User> usersRepository)
        {
            _usersRepository = usersRepository;
        }

        protected bool CheckCredentials(UserCredentials credentials)
        {
            var user = _usersRepository.AllData.FirstOrDefault(u => u.Pseudonym.Equals(credentials.Pseudonym, StringComparison.InvariantCulture));
            if (user == null)
            {
                throw new ResourceNotFoundException($"User with pseudonym \"{credentials.Pseudonym}\" wasn't found");
            }

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

        private static string GetSHA256(string text)
        {
            var encoding = new UTF8Encoding();
            var textBytes = encoding.GetBytes(text);
            byte[] hashBytes;

            var hash = new SHA256Managed();
            hashBytes = hash.ComputeHash(textBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}
