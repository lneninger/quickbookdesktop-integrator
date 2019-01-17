using Framework.FileStorage.Standard.FileStorage.Models;
using Framework.Logging.Log4Net;
using Framework.Storage.FileStorage.interfaces;
using Framework.Storage.FileStorage.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Storage.FileStorage
{
    public abstract class BaseFileStorageService: IFileStorageService
    {
        public LoggerCustom Logger = Framework.Logging.Log4Net.LoggerFactory.Create(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Func<string[], string> BuildRelativePath;
        public Func<string[], string> BuildFileName;

        //public abstract string Identifier { get; }

        public BaseFileStorageService()
        {
            this.BuildRelativePath = this.BuildDefaultRelativePath;
            this.BuildFileName = this.BuildDefaultFileName;
        }

        public virtual string BuildDefaultRelativePath(params string[] pathComponents)
        {
            var result = string.Join("\\", pathComponents);
            return result;
        }

        public virtual string BuildDefaultFileName(params string[] pathComponents)
        {
            var result = string.Join("_", pathComponents);
            return result;
        }

       
        protected abstract FileStorageResultDTO InternalSave(FileArgs args);

        public virtual FileStorageResultDTO Save(FileArgs args)
        {
           var result = this.InternalSave(args);
            return result;
        }

        public abstract Task<byte[]> RetrieveFile(string rootFolder, string accessPath, string folderPath, string fileName);
    }
}
