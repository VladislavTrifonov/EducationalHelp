using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalHelp.Web.Models.System
{
    public class ErrorModel
    {
        public string ErrorCode { get; set; }
        public string ErrorType { get; set; }
        public object Details { get; set; }
    }
}
