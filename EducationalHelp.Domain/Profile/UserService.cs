using EducationalHelp.Core.Entities;
using EducationalHelp.Data;
using System;
using System.Linq;
using EducationalHelp.Services.Exceptions;

namespace EducationalHelp.Services.Profile
{
    public class UserService
    {
        private readonly IRepository<User> _usersRepository;

        public UserService(IRepository<User> usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public User GetUserByName(string pseudonym)
        {
            var user = _usersRepository.AllData.FirstOrDefault(u => u.Pseudonym.Equals(pseudonym, StringComparison.InvariantCulture));
            if (user == null)
            {
                throw new ResourceNotFoundException($"User with pseudonym \"{pseudonym}\" wasn't found!");
            }

            return user;
        }
    }
}
