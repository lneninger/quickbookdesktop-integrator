using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.FileStorage.AspNetCore.FileStorage.Models
{
    public interface IFileData
    {
        object Identifier { get; }

        string FileSystemTypeId { get; }

        string ThumbnailFileName { get; }

        string FileName { get; }

        string RootPath { get; }

        string AccessPath { get; }

        string RelativePath { get; }
    }
}
