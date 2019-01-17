using Framework.Core.Messages;
using Framework.FileStorage.AspNetCore.FileStorage.Models;

namespace Framework.Storage.FileStorage.TemporaryStorage
{
    public interface IFileRetriever
    {
        OperationResponse<IFileData> GetFileData(object identifier);
    }
}