using CommunicationModel.Commons;
using Framework.Commons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Framework.Storage.FileStorage.TemporaryStorage
{
    public class TemporaryFileUploadDTO
    {
        //public IFormFile File { get; set; }
        public string FileName { get; }
        public long Size { get; }
        public string ContentType { get; }
        public byte[] Content { get; }
        public Guid UniqueIdentifier { get; }
        public string TemporaryFileName
        {
            get
            {
                var result = $"{this.UniqueIdentifier}_{Path.GetFileName(this.FileName)}";
                return result;
            }
        }

        public bool Saved { get; private set; }

        public TemporaryFileUploadDTO(string fileName, string contentType, Stream stream)
        {
            this.UniqueIdentifier = Guid.NewGuid();
            //this.File = file;
            this.FileName = fileName;
            this.Size = stream.Length;
            this.ContentType = contentType;
            var memStream = new MemoryStream();
            stream.CopyTo(memStream);
            this.Content = memStream.GetBuffer();
        }

        public TemporaryFileUpdatedResult SaveFile(string folderPath)
        {
            if (this.Saved) return null;

            if (string.IsNullOrWhiteSpace(folderPath))
            {
                folderPath = AppConfig.Instance.FileStorageSettings.TemporaryFolderPath; 
            }

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fileName = this.TemporaryFileName;
            var filePath = Path.Combine(folderPath, fileName);
            using (var fileStream = System.IO.File.Create(filePath)) {
                using (var memoryStream = new MemoryStream(this.Content)) {
                    memoryStream.CopyTo(fileStream);
                    memoryStream.Flush();
                }
            }
            this.Saved = true;

            return this.BuildResult();
        }

        private TemporaryFileUpdatedResult BuildResult()
        {
            var result = new TemporaryFileUpdatedResult();
            result.UniqueIdentifier = this.UniqueIdentifier;
            result.OriginalFileName = this.FileName;
            result.ContentLength = this.Content.Length;
            result.TemporaryFileName = this.TemporaryFileName;
            result.ContentType = this.ContentType;

            return result;
        }
    }
}
