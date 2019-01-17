using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Models.FileStorage.Models
{
    /// <summary>
    /// File persistence mechanism
    /// </summary>
    public class UploadedFileResult
    {
        public string RootPath { get; set; }

        public string AccessPath { get; set; }

        public string FolderPath { get; set; }

        public string FileName { get; set; }

        public string FileSourceId { get; set; }
    }
}
