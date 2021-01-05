#nullable enable
using EducationalHelp.Core.Entities;
using EducationalHelp.Data;
using System;
using System.Linq;
using EducationalHelp.Services.Exceptions;
using EducationalHelp.Data.Exceptions;
using EducationalHelp.Services.Files;

namespace EducationalHelp.Services.Profile
{
    public class UserService
    {
        private readonly IRepository<User> _usersRepository;
        private readonly IRepository<UserFiles> _userFilesRepository;
        private readonly IRepository<GroupUsers> _groupUsersRepository;
        private readonly FilesService _filesService;

        public UserService(IRepository<User> usersRepository, IRepository<UserFiles> userFilesRepository, FilesService filesService, IRepository<GroupUsers> groupUsersRepository)
        {
            _usersRepository = usersRepository;
            _userFilesRepository = userFilesRepository;
            _filesService = filesService;
            _groupUsersRepository = groupUsersRepository;
        }

        public User GetUserByName(string login)
        {
            var user = _usersRepository.AllData.FirstOrDefault(u => u.Login == login);
            if (user == null)
            {
                throw new ResourceNotFoundException($"User with login \"{login}\" wasn't found!");
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

        public File? GetUserAvatar(Guid userId)
        {
            var uf =_userFilesRepository.AllData
                .Where(uf => uf.UserId == userId && uf.Type == UserFilesType.Avatar)
                .OrderByDescending(uf => uf.CreatedAt)
                .FirstOrDefault();

            if (uf == null)
            {
                return null;
            }

            return _filesService.GetFileModelById(uf.FileId);
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

        public bool IsMemberOfGroup(Guid userId, Guid groupId)
        {
            var groupUserRecords = from gu in _groupUsersRepository.AllData
                        where gu.UserId == userId && gu.GroupId == groupId
                        select gu;

            return groupUserRecords.Count() != 0;
        }
    }
}
