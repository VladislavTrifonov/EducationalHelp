using EducationalHelp.Core.Validation;
using EducationalHelp.Web.Extensions;
using EducationalHelp.Web.Models.System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace EducationalHelp.Web.Filters
{
    public class ValidationFilter : IAlwaysRunResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (!((context.Result as ObjectResult)?.Value is ValidationResult))
                return;

            var errorObject = new ErrorModel
            {
                ErrorCode = (context.Controller as ControllerBase)?.GetFrontendName() + "/" + context.HttpContext.TraceIdentifier
                                                                                                                    ?? "undefined",
                ErrorType = "validation",
                Details = ((ObjectResult)context.Result).Value
            };
            context.Result = new BadRequestObjectResult(errorObject);
        }
    }
}
