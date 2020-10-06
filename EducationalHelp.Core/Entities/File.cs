using System;
using System.Collections.Generic;
using System.Text;

namespace EducationalHelp.Core.Entities
{
    public class File : BaseEntity
    {
        public bool IsStoredLocal { get; private set; }

        private string _pathToFile; 
        public string PathToFile
        {
            get
            {
                if (!IsStoredLocal)
                    throw new InvalidOperationException("FileInfo not set, you should store local file to use this");

                return _pathToFile;
            }
            set
            {
                if (value != null)
                {
                    _pathToFile = value;
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
