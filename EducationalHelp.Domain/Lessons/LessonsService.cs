using System;
using System.Collections.Generic;
using System.Linq;
using EducationalHelp.Core.Entities;
using EducationalHelp.Data;
using EducationalHelp.Services.Exceptions;
using EducationalHelp.Services.Groups;
using EducationalHelp.Services.Profile;
using Microsoft.EntityFrameworkCore;

namespace EducationalHelp.Services.Lessons
{
    public class LessonsService
    {
        private readonly IRepository<Lesson> _lessonRepository;
        private readonly IRepository<LessonUsers> _lessonUsersRepository;
        private readonly IRepository<Subject> _subjectsRepository;
        private readonly IRepository<User> _usersRepository;
        private readonly GroupService _groupsService;
        private readonly UserService _usersService;

        public LessonsService(IRepository<Lesson> lessonRepository,
            IRepository<LessonUsers> lessonUsersRepository
            , IRepository<Subject> subjectRepository,
            IRepository<User> usersRepository,
            GroupService groupsService,
            UserService usersService)
        {
            _lessonRepository = lessonRepository;
            _lessonUsersRepository = lessonUsersRepository;
            _subjectsRepository = subjectRepository;
            _usersRepository = usersRepository;
            _groupsService = groupsService;
            _usersService = usersService;
        }

       public List<Lesson> GetLessonsByGroup(Guid groupId)
        {
            var lessons = (from subjects in _subjectsRepository.AllData
                          where subjects.GroupId == groupId
                          join l in _lessonRepository.AllData on subjects.Id equals l.SubjectId
                          select l)
                          .ToList();

            return lessons;
        }

        public Lesson GetLessonById(Guid id)
        {
            var lesson = _lessonRepository.GetById(id);

            if (lesson == null)
                throw new ServiceException($"Lesson with id {id} was not found");

            return lesson;
        }

        public ICollection<Lesson> GetLessonsBySubjectId(Guid id)
        {
            var lessons = _lessonRepository.AllData.Where(l => l.SubjectId == id).ToHashSet();
            
            return lessons; 
        }

        public IEnumerable<Lesson> GetLessonsByUser(Guid userId)
        {
            var lessons = (from lu in _lessonUsersRepository.AllData
                          where lu.UserId == userId
                          join l in _lessonRepository.AllData on lu.LessonId equals l.Id
                          select l)
                          .AsEnumerable();

            return lessons;
        }

        public Lesson CreateLesson(Lesson lesson, Guid userId)
        {
            var resultOfValidation = lesson.Validate();
            if (!resultOfValidation.Success)
                throw new ValidationException("Lesson entity is invalid!") { ValidationResult = resultOfValidation };

            _lessonRepository.Insert(lesson);
            AddParticipant(lesson.Id, userId);

            return lesson;
        }

        public void DeleteLesson(Guid id)
        {
            var lesson = GetLessonById(id);
            _lessonRepository.Delete(lesson);
        }

        public void UpdateLesson(Lesson newLesson)
        {
            if (newLesson == null)
                throw new ArgumentNullException(nameof(newLesson));

            _lessonRepository.Update(newLesson);
        }

        public bool IsExist(Guid id)
        {
            return _lessonRepository.AllData.Any(l => l.Id == id);
        }

        public Guid GetGroupId(Guid lessonId)
        {
            var id = (from l in _lessonRepository.AllData
                      where l.Id == lessonId
                      join s in _subjectsRepository.AllData on l.SubjectId equals s.Id
                      select s.GroupId)
                     .First();

            return id;
        }

        public double GetAvgLessonMarkBySubject(Guid subjectId, Guid userId)
        {
            var lessons = (from lu in _lessonUsersRepository.AllData
                           where lu.UserId == userId
                           join l in _lessonRepository.AllData on lu.LessonId equals l.Id
                           where l.SubjectId == subjectId
                           select lu)
                          .AsEnumerable();
            
            if (lessons.Count() == 0)
                return -1;
            
            return lessons.Average(lu => lu.Mark.GetDigitOfMark());
        }


        public double GetAvgLessonMarkByUser(Guid userId)
        {
            var lessons = _lessonUsersRepository.AllData.Where(lu => lu.UserId == userId).AsEnumerable();
            
            if (lessons.Count() == 0)
                return -1;

            return lessons.Average(lu => lu.Mark.GetDigitOfMark());
        }

        public int GetNumberOfLessonsBySubject(Guid subjectId)
        {
            return _lessonRepository.AllData
                .Where(l => l.SubjectId == subjectId)
                .Count();
        }

        public int GetMissedLessonsCountBySubject(Guid subjectId, Guid userId)
        {
            var missedLessons = from lu in _lessonUsersRepository.AllData
                    where lu.UserId == userId
                    join l in _lessonRepository.AllData on lu.LessonId equals l.Id
                    where l.SubjectId == subjectId && lu.IsVisited == false
                    select lu.Id;

            return missedLessons.Count();
        }

        public void AddParticipant(Guid lessonId, Guid userId)
        {
            var lessonUser = new LessonUsers()
            {
                LessonId = lessonId,
                UserId = userId
            };

            _lessonUsersRepository.Insert(lessonUser);
        }

        public void RemoveParticipant(Guid lessonId, Guid userId)
        {
            var lessonUser = _lessonUsersRepository.AllData.First(lu => lu.LessonId == lessonId && lu.UserId == userId);

            _lessonUsersRepository.Delete(lessonUser);
        }

        public bool IsUserParticipate(Guid lessonId, Guid userId)
        {
            return _lessonUsersRepository.AllData.Any(lu => lu.LessonId == lessonId && lu.UserId == userId);
        }

        public IEnumerable<LessonUsers> GetLessonParticipants(Guid lessonId)
        {
            var users = from lu in _lessonUsersRepository.AllData
                        where lu.LessonId == lessonId
                        join u in _usersRepository.AllData on lu.UserId equals u.Id
                        select lu;

            return users.AsEnumerable();
        }

        public void UpdateParticipant(LessonUsers lessonUser)
        {
            if (lessonUser == null)
            {
                throw new ArgumentNullException(nameof(lessonUser));
            }

            _lessonUsersRepository.Update(lessonUser);
        }

        public LessonUsers GetLessonParticipant(Guid lessonId, Guid userId)
        {
            var user = _lessonUsersRepository.AllData.FirstOrDefault(lu => lu.LessonId == lessonId && lu.UserId == userId);
            if (user == null)
            {
                throw new ResourceNotFoundException($"User with id {userId} not a participant of lesson with id {lessonId}");
            }

            return user;
        }

        public User GetLessonParticipant(LessonUsers lessonUsers)
        {
            return lessonUsers.User;
        }

        public IEnumerable<User> GetPossibleParticipants(Guid lessonId)
        {
            if (!IsExist(lessonId))
            {
                throw new ResourceNotFoundException($"Lesson with id {lessonId} wasn't found");
            }

            var groupId = GetGroupId(lessonId);
            var groupUsers = _groupsService.GetGroupUsers(groupId);
            if (groupUsers.Count() == 0)
            {
                return new List<User>();
            }

            var lessonUsers = GetLessonParticipants(lessonId);
            var users = lessonUsers.Select(lu => GetLessonParticipant(lu));
            if (users.Count() == 0)
            {
                return groupUsers;
            }

            return groupUsers.Except(users);
        }

        public bool CanUserAccessToLesson(Guid userId, Guid lessonId)
        {
            var lessonGroupId = GetGroupId(lessonId);
            if (!_usersService.IsMemberOfGroup(userId, lessonGroupId))
            {
                return false;
            }

            if (!IsUserParticipate(lessonId, userId))
            {
                return false;
            }

            return true;
        }
    }
}
