using System;
using System.Collections.Generic;
using System.Linq;
using EducationalHelp.Core.Entities;
using EducationalHelp.Data;
using EducationalHelp.Services.Exceptions;

namespace EducationalHelp.Services.Lessons
{
    public class LessonsService
    {
        private readonly IRepository<Lesson> _lessonRepository;

        public LessonsService(IRepository<Lesson> lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public List<Lesson> GetAllLessons()
            => _lessonRepository.AllData.ToList();

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
    }
}
