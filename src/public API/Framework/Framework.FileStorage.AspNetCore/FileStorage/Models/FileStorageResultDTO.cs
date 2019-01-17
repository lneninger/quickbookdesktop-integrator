using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.FileStorage.Standard.FileStorage.Models
{
    public class FileStorageResultDTO
    {
        public string RootPath { get; set; }

        public string AccessPath { get; set; }

        public string FolderPath { get; set; }

        public string FileName { get; set; }

        public string ThumbnailFileName { get; set; }

        public string ThumbnailFullFilePath { get; set; }

        public string FileSourceId { get; set; }

        public string FullFilePath { get; set; }
    }
}
