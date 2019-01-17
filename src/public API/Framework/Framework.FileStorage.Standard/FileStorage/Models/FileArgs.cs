using Framework.Storage.DataHolders.Messages;
using Framework.Storage.FileStorage.TemporaryStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Storage.FileStorage.Models
{
    public abstract class FileArgs : IDisposable
    {
        public FileArgs()
        {
        }

        public Guid FileId { get; protected set; }

        public UploadedFile UploadedFile
        {
            get
            {
                return _uploadedFile;
            }
            set
            {
                this._uploadedFile = value;
                this.ParseFile();
            }
        }

        protected void ValidateParams()
        {
            var validatePathArguments = this.ValidateRelativePathArguments();
            var validateFileArguments = this.ValidateFileNameArguments();
            var validation = new OperationResponse().AddResponse(validatePathArguments).AddResponse(validateFileArguments);

            if (!validation.IsSucceed)
            {
                throw new OperationResponseException(validation);
            }
        }

        protected abstract OperationResponse ValidateFileNameArguments();

        protected abstract OperationResponse ValidateRelativePathArguments();

        private void ParseFile()
        {
            if (!this.HasFile) return;
            this.ContentType = this.UploadedFile.ContentType;

            using (var memoryStream = new MemoryStream())
            {
                this.UploadedFile.FileContent.CopyTo(memoryStream);
                memoryStream.Position = 0;
                this.FileContent = memoryStream.GetBuffer();
            }
        }

        public FileSourceEnum.Enum? FileSourceEnum { get; set; }

        public string FileSource { get {
                if (this.FileSourceEnum.HasValue)
                {
                    return this.FileSourceEnum.ToString();
                }

                return null;
            }
        }

        public string FilePrefix { get; set; }

        public string FileTypeId { get; set; }

        public string FileAreaId { get; set; }

        public string LanguageTypeId { get; set; }

        public string SourceLocaleLanguageId { get; set; }

        public byte[] FileContent { get; set; }

        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the target root path. Set to null to use the application configuration setting. appSettings -> FileSystemBaseStoragePath
        /// </summary>
        /// <remarks>
        /// Set to null to use the application configuration setting. appSettings -> FileSystemBaseStoragePath
        /// </remarks>
        /// <value>
        /// The target root path.
        /// </value>
        public string TargetRootPath { get; set; }

        public bool HasFile
        {
            get
            {
                return this.UploadedFile != null;
            }
        }

        public abstract string[] GetRelativePathElements();
        public abstract string[] GetFileNameElements();


        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            { // release other disposable objects  
            }
        }

        ~FileArgs()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Func<string[], string> BuildRelativePath;

        public Func<string[], string> BuildFileName;

        private UploadedFile _uploadedFile;
    }
}
