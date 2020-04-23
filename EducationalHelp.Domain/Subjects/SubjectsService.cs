using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using EducationalHelp.Data;
using EducationalHelp.Core.Entities;
using EducationalHelp.Services.Exceptions;

namespace EducationalHelp.Services.Subjects
{
    public class SubjectsService
    {
        private IRepository<Subject> _subjectsRepository;

        public SubjectsService(IRepository<Subject> subjectsRepository)
        {
            _subjectsRepository = subjectsRepository;
        }

        public List<Subject> GetAllSubjects()
            => _subjectsRepository.AllData.ToList();

        public Subject GetSubject(Guid id)
        {
            var subject = _subjectsRepository.GetById(id);

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
    }
}
