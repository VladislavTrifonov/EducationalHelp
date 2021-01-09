using EducationalHelp.Core.Entities;
using EducationalHelp.Data;
using EducationalHelp.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EducationalHelp.Services.Groups
{
    public class GroupService
    {
        private readonly IRepository<GroupUsers> _groupUsersRepository;
        private readonly IRepository<Group> _groupsRepository;
        private readonly IRepository<User> _usersRepository;

        public GroupService(IRepository<GroupUsers> groupUsersRepository, IRepository<Group> groupsRepository, IRepository<User> usersRepository)
        {
            _groupUsersRepository = groupUsersRepository;
            _groupsRepository = groupsRepository;
            _usersRepository = usersRepository;
        }

        public Group GetGroupById(Guid groupId)
        {
            var group = _groupsRepository.AllData.FirstOrDefault(g => g.Id == groupId);
            if (group == null)
            {
                throw new ResourceNotFoundException($"Group with ID {groupId} wasn't found");
            }

            return group;
        }

        public IEnumerable<User> GetGroupUsers(Guid groupId)
        {
            var users = from gu in _groupUsersRepository.AllData
                        where gu.GroupId == groupId
                        join u in _usersRepository.AllData on gu.UserId equals u.Id
                        select u;

            return users.AsEnumerable();
        }
    }
}
