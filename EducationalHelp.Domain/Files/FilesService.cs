using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading;
using EducationalHelp.Core.Entities;
using EducationalHelp.Data;
using EducationalHelp.Services.Exceptions;
using File = EducationalHelp.Core.Entities.File;

namespace EducationalHelp.Services.Files
{
    public class FilesService
    {
        private readonly IRepository<File> _files;
        private readonly IRepository<LessonFiles> _lessonFiles;
        private readonly IRepository<UserFiles> _userFiles;

        public FilesService(IRepository<File> files, IRepository<LessonFiles> lessonFiles, IRepository<UserFiles> userFiles)
        {
            _files = files;
            _lessonFiles = lessonFiles;
            _userFiles = userFiles;
        }

        public (File, FileStream) CreateNewFile(string fileName, string rootPath, long length)
        {
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException(nameof(fileName));
            }

            if (string.IsNullOrEmpty(rootPath) || string.IsNullOrWhiteSpace(rootPath))
            {
                throw new ArgumentException(nameof(rootPath));
            }

            if (length <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length), $"Length of file should be above 0.");
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
                OriginalName = fileName,
                Length = length
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

        public FileStream GetFileStreamByFileModel(File f)
        {
            return System.IO.File.Open(f.FullPath, FileMode.Open);
        }

        public File GetFileModelById(Guid fileId)
        {
            var f = _files.GetById(fileId);
            if (f == null)
                throw new ServiceException($"File with id {fileId} wasn't found");

            return f;
        }

        public void AttachFileToLesson(LessonFiles model)
        {
            _lessonFiles.Insert(model);
        }

        public void AttachFileToUser(Guid userId, Guid fileId, UserFilesType type)
        {
            var model = new UserFiles()
            {
                FileId = fileId,
                UserId = userId,
                Type = type
            };

            _userFiles.Insert(model);
        }

        public void DeleteFile(Guid fileId)
        {
            var f = GetFileModelById(fileId);
            if (System.IO.File.Exists(f.FullPath))
            {
                System.IO.File.Delete(f.FullPath);
            }

            _files.Delete(f);
        }


    }
}
