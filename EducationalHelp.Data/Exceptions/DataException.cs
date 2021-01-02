using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EducationalHelp.Data.Exceptions
{
    public class DataException : Exception
    {
        public DataException()
        {
        }

        public DataException(string message) : base(message)
        {
        }

        public DataException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
