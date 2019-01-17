using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using CommunicationModel.Commons;
using Framework.Commons;
using Framework.FileStorage.Standard.FileStorage.Models;
using Framework.Storage.FileStorage.Models;

namespace Framework.Storage.FileStorage.StorageImplementations
{
    /// <summary>
    /// FileStorageService implementation for AWS S3
    /// </summary>
    /// <seealso cref="Framework.Storage.FileStorage.BaseFileStorageService" />
    public class AWSStorage : BaseFileStorageService
    {
        public static string Identifier { get; } = FileSourceEnum.AWS;
        public static string AWSCredentialKey { get; } = AppConfig.Instance.FileStorageSettings.AWS.AWSFileRepoAccessKey;
        public static string AWSFileRepoSecretKey { get; } = AppConfig.Instance.FileStorageSettings.AWS.AWSFileRepoSecretKey;
        public static string AWSFileStoreRootURL { get; } = AppConfig.Instance.FileStorageSettings.AWS.AWSFileStoreRootURL;
        public static string AWSFileRepoRegionEndPoint { get; } = AppConfig.Instance.FileStorageSettings.AWS.AWSFileRepoRegionEndPoint;
        public static string AWSFileRepoBucketName { get; } = AppConfig.Instance.FileStorageSettings.AWS.AWSFileRepoBucketName;

        /// <summary>
        /// Retrieves the file from AWS.
        /// </summary>
        /// <param name="rootFolder">The root folder.</param>
        /// <param name="accessPath">The access path.</param>
        /// <param name="folderPath">The folder path.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public override async Task<byte[]> RetrieveFile(string rootFolder, string accessPath, string folderPath, string fileName)
        {
            try
            {
                byte[] result = null;
                using (var client = this.CreateClient(rootFolder))
                {
                    var fileKey = this.BuildDefaultRelativePath(folderPath, fileName);
                    GetObjectRequest request = new GetObjectRequest
                    {
                        BucketName = accessPath,
                        Key = fileKey
                    };

                    using (GetObjectResponse response = await client.GetObjectAsync(request))
                    {
                        using (var memStream = new MemoryStream())
                        {
                            response.ResponseStream.CopyTo(memStream);
                            result = memStream.ToArray();
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AWSStorage.RetrieveFile ERROR - [{ex.Message}]");
                throw;
            }
        }

        /// <summary>
        /// Creates the client.
        /// </summary>
        /// <param name="rootFolder">The root folder.</param>
        /// <returns></returns>
        protected IAmazonS3 CreateClient(string rootFolder)
        {
            try
            {
                AWSCredentials credentials = new BasicAWSCredentials(AWSStorage.AWSCredentialKey,
                                                                          AWSStorage.AWSFileRepoSecretKey);
                AmazonS3Config config = new AmazonS3Config();
                config.ServiceURL = rootFolder; 
                config.RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(AWSStorage.AWSFileRepoRegionEndPoint/* ConfigurationManager.AppSettings["AWSFileRepoRegionEndPoint"]*/);

                var result = new AmazonS3Client(credentials, config);
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AWSStorage.CreateClient ERROR - [{ex.Message}]");
                throw;
            }
        }

        /// <summary>
        /// Internals the save.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        protected override FileStorageResultDTO InternalSave(FileArgs args)
        {
            string[] targetRelativePathElements = args.GetRelativePathElements();
            string[] targetFileNameElements = args.GetFileNameElements();

            var relativePath = (args.BuildRelativePath ?? this.BuildRelativePath)(targetRelativePathElements);
            var fileName = (args.BuildFileName ?? this.BuildFileName)(targetFileNameElements);

            fileName += Path.GetExtension(args.UploadedFile.OriginalFileName);

            string FullFileKey = string.Join("/", relativePath, fileName);// Path.Combine(relativePath, fileName);

            try
            {
                using (var client = this.CreateClient(AWSStorage.AWSFileStoreRootURL))
                {
                    AWSUploadPublicFileAsync(client, args.UploadedFile.FileContent, AWSStorage.AWSFileRepoBucketName/* ConfigurationManager.AppSettings["AWSFileRepoBucketName"]*/, FullFileKey).Wait();
                } 

                var result = new FileStorageResultDTO
                {
                    RootPath = AWSStorage.AWSFileStoreRootURL,
                    AccessPath = AWSStorage.AWSFileRepoBucketName,
                    FolderPath = relativePath,
                    FileName = fileName,
                    FileSourceId = Identifier
                };

                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AWSStorage.InternalSave ERROR - [{ex.Message}]");
                throw;
            }
        }

        /// <summary>
        /// Builds the default relative path.
        /// </summary>
        /// <param name="pathComponents">The path components.</param>
        /// <returns></returns>
        public override string BuildDefaultRelativePath(params string[] pathComponents)
        {
            var result = string.Join("/", pathComponents);
            return result;
        }

        /// <summary>
        /// AWSUploadPublicFileAsync
        /// </summary>
        /// <param name="s3Client"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="amazonBucket"></param>
        /// <param name="amazonFileKey"></param>
        /// <returns></returns>
        private async Task AWSUploadPublicFileAsync(IAmazonS3 s3Client, string sourceFilePath, string amazonBucket, string amazonFileKey)
        {
            try
            {
                using (FileStream fStreamToUpload = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
                {
                    await AWSUploadPublicFileAsync(s3Client, fStreamToUpload, amazonBucket, amazonFileKey);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Awses the upload public file asynchronous.
        /// </summary>
        /// <param name="s3Client">The s3 client.</param>
        /// <param name="fileStream">The file stream.</param>
        /// <param name="amazonBucket">The amazon bucket.</param>
        /// <param name="amazonFileKey">The amazon file key.</param>
        /// <returns></returns>
        private async Task AWSUploadPublicFileAsync(IAmazonS3 s3Client, Stream fileStream, string amazonBucket, string amazonFileKey)
        {
            try
            {
                fileStream.Position = 0;
                TransferUtility fileTransferUtility = new TransferUtility(s3Client);

                TransferUtilityUploadRequest uploadRequest = new TransferUtilityUploadRequest
                {
                    BucketName = amazonBucket,
                    Key = amazonFileKey,

                    InputStream = fileStream,
                    CannedACL = S3CannedACL.PublicRead,
                };

                await fileTransferUtility.UploadAsync(uploadRequest).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AWSStorage.AWSUploadPublicFileAsync ERROR - [{ex.Message}]");
                throw;
            }
        }
    }
}
