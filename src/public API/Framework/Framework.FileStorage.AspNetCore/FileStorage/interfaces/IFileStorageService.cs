using Framework.FileStorage.Standard.FileStorage.Models;
using Framework.Storage.FileStorage.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Storage.FileStorage.interfaces
{
    public interface IFileStorageService
    {
        string BuildDefaultRelativePath(params string[] pathComponents);

        string BuildDefaultFileName(params string[] pathComponents);

        //string Identifier { get; }
        FileStorageResultDTO Save(FileArgs args);

        Task<byte[]> RetrieveFile(string rootPath, string accessPath, string folderPath, string fileName);
    }
}
