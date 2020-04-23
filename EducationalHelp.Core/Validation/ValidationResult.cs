using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalHelp.Core.Validation
{
    public class ValidationResult 
    {
        public static ValidationResult NoErrors { get; } = new ValidationResult(null);

        public bool Success => ErrorCount == 0;
        public int ErrorCount => ListOfErrors.Count();
        public IEnumerable<ValidationError> ListOfErrors { get; private set; }

        public ValidationResult(IEnumerable<ValidationError> listOfErrors)
        {
            ListOfErrors = listOfErrors ?? new List<ValidationError>();
        }

      
    }

    public class ValidationError
    {
        public string ErrorMessage { get; set; }
        public string PropertyName { get; set; }

        public ValidationError(string message, string property)
            => (ErrorMessage, PropertyName) = (message, property);
    }

}
