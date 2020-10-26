using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using EducationalHelp.Core.Entities;
using EducationalHelp.Data;
using File = EducationalHelp.Core.Entities.File;

namespace EducationalHelp.Services.Files
{
    public class FilesService
    {
        private readonly IRepository<File> _files;
        private readonly IRepository<LessonFiles> _lessonFiles;

        public FilesService(IRepository<File> files, IRepository<LessonFiles> lessonFiles)
        {
            _files = files;
            _lessonFiles = lessonFiles;
        }

        public (File, FileStream) CreateNewFile(string fileName, string rootPath)
        {
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException(nameof(fileName));
            }

            if (string.IsNullOrEmpty(rootPath) || string.IsNullOrWhiteSpace(rootPath))
            {
                throw new ArgumentException(nameof(rootPath));
            }

            if (!Directory.Exists(rootPath))
            {
                throw new DirectoryNotFoundException($"Root path ({rootPath}) should be exist.");
            }

            var filePath = Path.Combine(rootPath, "userUploads");
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            var fullFileName = Path.GetRandomFileName();
            var fullFilePath = Path.Combine(filePath, fullFileName);

            var fileModel = new File()
            {
                FullPath = fullFilePath,
                OriginalName = fileName
            };

            _files.Insert(fileModel);

            return (fileModel, System.IO.File.Create(fullFilePath));
        }

        public (File, FileStream) GetFileById(Guid fileId)
        {
            var f =_files.GetById(fileId);
            var fs = System.IO.File.Open(f.FullPath, FileMode.Open);
            return (f, fs);
        }

        public void AddFileToLesson(Guid lessonId, Guid fileId)
        {
            var model = new LessonFiles()
            {
                FileId = fileId,
                LessonId = lessonId
            };

            _lessonFiles.Insert(model);
        }


    }
}
