using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using CommunicationModel.Commons;
using Framework.FileStorage.Standard.FileStorage.Models;
using Framework.Storage.FileStorage.Models;

namespace Framework.Storage.FileStorage.StorageImplementations
{
    public class AZUREStorage : BaseFileStorageService
    {
        public AZUREStorage()
        {
        }

        public static string Identifier { get; } = FileSourceEnum.AZURE;

        public override Task<byte[]> RetrieveFile(string rootFolder, string accessPath, string folderPath, string fileName)
        {
            throw new NotImplementedException();
        }

        protected override FileStorageResultDTO InternalSave(FileArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
