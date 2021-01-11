#nullable enable
using EducationalHelp.Core.Entities;
using EducationalHelp.Data;
using System;
using System.Linq;
using EducationalHelp.Services.Exceptions;
using EducationalHelp.Data.Exceptions;
using EducationalHelp.Services.Files;
using System.Collections.Generic;
using EducationalHelp.Services.Groups;

namespace EducationalHelp.Services.Profile
{
    public class UserService
    {
        private readonly IRepository<User> _usersRepository;
        private readonly IRepository<UserFiles> _userFilesRepository;
        private readonly IRepository<GroupUsers> _groupUsersRepository;
        private readonly IRepository<Group> _groupsRepository;
        private readonly FilesService _filesService;
        private readonly GroupService _groupsService;

        public UserService(
            IRepository<User> usersRepository,
            IRepository<UserFiles> userFilesRepository,
            FilesService filesService,
            IRepository<GroupUsers> groupUsersRepository,
            IRepository<Group> groupsRepository,
            GroupService groupsService)
        {
            _usersRepository = usersRepository;
            _userFilesRepository = userFilesRepository;
            _filesService = filesService;
            _groupUsersRepository = groupUsersRepository;
            _groupsRepository = groupsRepository;
            _groupsService = groupsService;
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
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _usersRepository.Insert(user);
            _groupsService.CreateLocalUserGroup(user);
        }

        public bool IsMemberOfGroup(Guid userId, Guid groupId)
        {
            var groupUserRecords = from gu in _groupUsersRepository.AllData
                        where gu.UserId == userId && gu.GroupId == groupId
                        select gu;

            return groupUserRecords.Count() != 0;
        }

        public IEnumerable<Group> GetMemberGroups(Guid userId)
        {
            var groups = from gu in _groupUsersRepository.AllData
                                   where gu.UserId == userId
                                   join g in _groupsRepository.AllData on gu.GroupId equals g.Id
                                   select g;

            return groups.AsEnumerable();
        }

        public void LeaveFromGroup(Guid userId, Guid groupId)
        {
            var userGroups = GetMemberGroups(userId);
            if (userGroups.Count() == 1)
            {
                throw new InvalidOperationException("Нельзя выйти из всех групп");
            }

            var userGroupRecord = (from gu in _groupUsersRepository.AllData
                                   where gu.GroupId == groupId && gu.UserId == userId
                                   select gu)
                                  .FirstOrDefault();

            _groupUsersRepository.Delete(userGroupRecord);
        }
    }
}
