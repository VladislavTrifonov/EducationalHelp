using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EducationalHelp.Services.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace EducationalHelp.Web.Controllers
{
    public class FilesController : ControllerBase
    {
        private readonly FilesService _filesService;

        public FilesController(FilesService filesService)
        {
            _filesService = filesService;
        }

        [HttpGet("viewFile/{fileId}")]
        public IActionResult ViewFile(Guid fileId)
        {
            try
            {
                var (f, fs) = _filesService.GetFileById(fileId);
                return File(fs, "application/octet-stream", f.OriginalName);
            }
            catch (DataException)
            {
                return NotFound();
            }
        }

    }
}
