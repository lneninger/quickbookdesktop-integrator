namespace Framework.Commons
{
    public class FileStorageSettings
    {
        public string TemporaryFolderPath { get; set; }

        public string DefaultFileStorageDestination { get; set; }

        public AWSSettings AWS { get; set; }

        public FileSystemSettings FileSystem { get; set; }


        public class AWSSettings {
            public string AWSFileRepoAccessKey { get; set; }

            public string AWSFileRepoSecretKey { get; set; }

            public string AWSFileStoreRootURL { get; set; }

            public string AWSFileRepoRegionEndPoint { get; set; }

            public string AWSFileRepoBucketName { get; set; }
        }

        public class FileSystemSettings {
            public string FileSystemBaseStoragePath { get; set; }
        }
    }
}