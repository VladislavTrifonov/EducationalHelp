using System;
using System.Linq;
using System.Collections.Generic;
using EducationalHelp.Data;
using EducationalHelp.Core.Entities;
using EducationalHelp.Services.Exceptions;
using EducationalHelp.Services.Lessons;
using Microsoft.EntityFrameworkCore;

namespace EducationalHelp.Services.Subjects
{
    public class SubjectsService
    {
        private readonly IRepository<Subject> _subjectsRepository;
        private readonly LessonsService _lessonsService;

        public SubjectsService(IRepository<Subject> subjectsRepository, LessonsService lessonsService)
        {
            _subjectsRepository = subjectsRepository;
            _lessonsService = lessonsService;
        }

        public List<Subject> GetAllSubjects(Guid groupId)
            => _subjectsRepository.AllData.Where(s => s.GroupId == groupId).ToList();

        public Subject GetSubject(Guid id)
        {
            var subject = _subjectsRepository.GetById(id);
            var lessons = _lessonsService.GetLessonsBySubjectId(id);

            if (lessons.Count() != 0)
                subject.Lessons = lessons;

            if (subject == null)
                throw new ServiceException($"Subject with id {id} was not found");

            return subject;
        }

        public Subject CreateSubject(Subject subject)
        {
            var resultOfValidation = subject.Validate();
            if (!resultOfValidation.Success)
                throw new ValidationException("Subject entity is invalid!") { ValidationResult = resultOfValidation };

            _subjectsRepository.Insert(subject);

            return subject;
        }

        public void DeleteSubject(Guid id)
        {
            var subject = GetSubject(id);
            _subjectsRepository.Delete(subject);
        }

        public void UpdateSubject(Subject newSubject)
        {
            if (newSubject == null)
                throw new ArgumentNullException(nameof(newSubject));

            _subjectsRepository.Update(newSubject);
        }

        public int GetSubjectsCountByUser(Guid userId)
        {
            return _subjectsRepository.AllData
                .Where(s => s.UserId == userId)
                .Count();
        }

        public Guid GetGroupIdFromSubjectId(Guid subjectId)
        {
            var groupId = (from s in _subjectsRepository.AllData
                           where s.Id == subjectId
                           select s.GroupId)
                           .FirstOrDefault();
            
            if (groupId == default)
            {
                throw new ResourceNotFoundException($"Subject with id {subjectId} wasn't found");
            }

            return groupId;
        }
    }
}
