using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EducationalHelp.Services.Exceptions
{
    public class ResourceNotFoundException : ServiceException
    {
        public ResourceNotFoundException()
        {
        }

        public ResourceNotFoundException(string message) : base(message)
        {
        }

        public ResourceNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ResourceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
