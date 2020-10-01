using System;
using System.Collections.Generic;
using System.Text;
using EducationalHelp.Core.Validation;

namespace EducationalHelp.Core.Entities
{
    public class Subject : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Teacher { get; set; }

        public virtual File Annotation { get; set; }
        public Guid? AnnotationId { get; set; }

        public virtual File Program { get; set; }
        public Guid? ProgramId { get; set; }

        public virtual File ValuationTools { get; set; }
        public Guid? ValuationToolsId { get; set; }


        public override ValidationResult Validate()
        {
            var validationErrors = new List<ValidationError>();

            #region Name validation
            if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name))
                validationErrors.Add(new ValidationError("Name doesn't empty", nameof(Name)));
            if (Name.Length < 3 || Name.Length > 40)
                validationErrors.Add(new ValidationError("Name must length be between 3 and 40 symbols", nameof(Name)));
            #endregion

            #region Description validation
            if (string.IsNullOrEmpty(Description) || string.IsNullOrWhiteSpace(Description))
                validationErrors.Add(new ValidationError("Description doesn't empty", nameof(Description)));
            if (Description.Length > 400 || Description.Length < 1)
                validationErrors.Add(new ValidationError("Description length must be between 1 and 400 symbols", nameof(Description)));
            #endregion

            #region Teacher validation
            if (string.IsNullOrWhiteSpace(Teacher) || string.IsNullOrEmpty(Teacher))
                validationErrors.Add(new ValidationError("Teacher doesn't empty", nameof(Teacher)));
            if (Teacher.Length > 50 || Teacher.Length < 5)
                validationErrors.Add(new ValidationError("Teacher length must be between 5 and 50 symbols", nameof(Teacher)));
            #endregion

            return validationErrors.Count != 0 ? new ValidationResult(validationErrors) : ValidationResult.NoErrors;
        }
    }
}
