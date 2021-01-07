using System;
using System.Collections.Generic;
using System.Linq;
using EducationalHelp.Core.Entities;
using EducationalHelp.Data;
using EducationalHelp.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EducationalHelp.Services.Lessons
{
    public class LessonsService
    {
        private readonly IRepository<Lesson> _lessonRepository;
        private readonly IRepository<LessonUsers> _lessonUsersRepository;
        private readonly IRepository<Subject> _subjectsRepository;
        private readonly IRepository<User> _usersRepository;

        public LessonsService(IRepository<Lesson> lessonRepository, IRepository<LessonUsers> lessonUsersRepository, IRepository<Subject> subjectRepository, IRepository<User> usersRepository)
        {
            _lessonRepository = lessonRepository;
            _lessonUsersRepository = lessonUsersRepository;
            _subjectsRepository = subjectRepository;
            _usersRepository = usersRepository;
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

        public Lesson CreateLesson(Lesson lesson)
        {
            var resultOfValidation = lesson.Validate();
            if (!resultOfValidation.Success)
                throw new ValidationException("Lesson entity is invalid!") { ValidationResult = resultOfValidation };

            _lessonRepository.Insert(lesson);

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

        public double GetAvgLessonMarkBySubject(Guid subjectId)
        {
            var lessons = this.GetLessonsBySubjectId(subjectId);
            
            if (lessons.Count == 0)
                return -1;
            
            return lessons.Average(l => l.SelfMark.GetDigitOfMark());
        }


        public double GetAvgLessonMarkByUser(Guid userId)
        {
            var lessons = GetLessonsByUser(userId);
            
            if (lessons.Count() == 0)
                return -1;

            return lessons.Average(l => l.SelfMark.GetDigitOfMark());
        }

        public int GetNumberOfLessonsBySubject(Guid subjectId)
        {
            return _lessonRepository.AllData
                .Where(l => l.SubjectId == subjectId)
                .Count();
        }

        public int GetMissedLessonsCountBySubject(Guid subjectId)
        {
            return _lessonRepository.AllData
                .Where(l => l.SubjectId == subjectId && l.IsVisited == false)
                .Count();
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

        public IEnumerable<User> GetLessonParticipants(Guid lessonId)
        {
            var users = from lu in _lessonUsersRepository.AllData
                        where lu.LessonId == lessonId
                        join u in _usersRepository.AllData on lu.UserId equals u.Id
                        select u;

            return users.AsEnumerable();
        }
    }
}
