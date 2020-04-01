using System;
using System.Collections.Generic;
using System.Text;

namespace EducationalHelp.Core.Entities
{
    public class File : BaseEntity
    {
        public bool IsStoredLocal { get; private set; }

        private System.IO.FileInfo _fileInfo; 
        public System.IO.FileInfo FileInfo
        {
            get
            {
                if (!IsStoredLocal)
                    throw new InvalidOperationException("FileInfo not set, you should store local file to use this");
                else
                    return _fileInfo;
            }
            set
            {
                if (value != null)
                {
                    _fileInfo = value;
                    IsStoredLocal = true;
                }
                else
                {
                    throw new ArgumentNullException(nameof(value));
                }
            }
        }
    }
}
