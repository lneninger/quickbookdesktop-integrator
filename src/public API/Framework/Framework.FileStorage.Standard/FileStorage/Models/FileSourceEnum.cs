using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Storage.FileStorage.Models
{
    public class FileSourceEnum
    {
        public static string FileSystem { get; } = "WIN";

        public static string AWS { get; } = "AWS";

        public static string AZURE { get; } = "AZU";

        public enum Enum
        {
            [Description("Windows File System")]
            FileSystem = 1,
            [Description("Amazon Cloud File Storage")]
            AWS = 2,
            [Description("Azure Cloud File Storage")]
            AZURE = 3
        }

    }
}
