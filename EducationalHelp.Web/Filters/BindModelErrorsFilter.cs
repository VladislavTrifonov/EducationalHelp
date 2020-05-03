using EducationalHelp.Core.Validation;
using EducationalHelp.Web.Extensions;
using EducationalHelp.Web.Models.System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalHelp.Web.Filters
{
    public class BindModelErrorsFilter : IAlwaysRunResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
           
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.ModelState.IsValid)
                return;

            var listOfErrors = new List<ValidationError>();
            foreach (var error in context.ModelState)
            {
                foreach (var itemError in error.Value.Errors)
                {
                    listOfErrors.Add(new ValidationError(itemError.ErrorMessage, error.Key));
                }
            }

            var validationResult = new ValidationResult(listOfErrors);
            var errorModel = new ErrorModel()
            {
                ErrorCode = (context.Controller as ControllerBase)?.GetFrontendName() + "/" + context.HttpContext.TraceIdentifier 
                                                                                                                    ?? "undefined",
                ErrorType = "validation",
                Details = validationResult
            };
            context.Result = new UnprocessableEntityObjectResult(errorModel);
        }
    }
}
