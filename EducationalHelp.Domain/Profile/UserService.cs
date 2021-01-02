using EducationalHelp.Core.Entities;
using EducationalHelp.Data;
using System;
using System.Linq;
using EducationalHelp.Services.Exceptions;
using EducationalHelp.Data.Exceptions;

namespace EducationalHelp.Services.Profile
{
    public class UserService
    {
        private readonly IRepository<User> _usersRepository;

        public UserService(IRepository<User> usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public User GetUserByName(string login)
        {
            var user = _usersRepository.AllData.FirstOrDefault(u => u.Login == login);
            if (user == null)
            {
                throw new ResourceNotFoundException($"User with pseudonym \"{login}\" wasn't found!");
            }

            return user;
        }

        public User GetUserById(Guid userId)
        {
            var user = _usersRepository.GetById(userId);
            if (user == null)
            {
                throw new ResourceNotFoundException($"User with id \"{userId}\" wasn't found");
            }

            return user;
        }

        public void UpdateUser(User user)
        {
            try
            {
                _usersRepository.Update(user);
            }
            catch (DataException e)
            {
                throw new ServiceException("", e);
            }
        }

        public void AddUser(User user)
        {
            _usersRepository.Insert(user);
        }
    }
}
