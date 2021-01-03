using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalHelp.Web.Controllers.Extensions
{
    public static class DownloadFileLinkExtension
    {
        public static string GetDownloadLink(this ControllerBase controller, Guid _fileId)
        {
            return controller.Url.Link("downloadFile", new
            {
                fileId = _fileId
            });
        }
    }
}
