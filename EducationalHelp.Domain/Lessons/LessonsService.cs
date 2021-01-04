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

        public LessonsService(IRepository<Lesson> lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public List<Lesson> GetAllLessons(Guid userId)
            => _lessonRepository.AllData.Where(l => l.UserId == userId).ToList();

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

        public double GetAvgLessonMarkBySubject(Guid subjectId)
        {
            var lessons = this.GetLessonsBySubjectId(subjectId);
            
            if (lessons.Count == 0)
                return -1;
            
            return lessons.Average(l => l.SelfMark.GetDigitOfMark());
        }

        public double GetAvgLessonMarkByUser(Guid userId)
        {
            var lessons = _lessonRepository.AllData
                .Where(l => l.UserId == userId)
                .AsEnumerable();
            
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
    }
}
