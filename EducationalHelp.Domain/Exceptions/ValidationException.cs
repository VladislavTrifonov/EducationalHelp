using EducationalHelp.Core.Validation;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EducationalHelp.Services.Exceptions
{
    public class ValidationException : ServiceException
    {
        public ValidationResult ValidationResult { get; internal set; }

        public ValidationException()
        {
        }

        public ValidationException(string message) : base(message)
        {
        }

        public ValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
