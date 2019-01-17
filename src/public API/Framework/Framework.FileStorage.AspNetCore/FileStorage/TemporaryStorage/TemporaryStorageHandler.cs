using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

using System.Web;
using Framework.Web.Helpers;
using Framework.Autofac;
using Autofac;
using Framework.Storage.FileStorage.interfaces;
using Framework.Core.Helpers;
using Framework.FileStorage.AspNetCore.FileStorage.Models;
using Framework.Logging.Log4Net;

namespace Framework.Storage.FileStorage.TemporaryStorage
{
    public class TemporaryStorageMiddleware//: BaseIoCDisposable
    {
        private readonly RequestDelegate _next;

        public TemporaryStorageMiddleware()
        {

        }

        public static string BaseTemporaryStorage => ConfigurationManager.AppSettings["FileStorageTemporaryFolder"];

        // private ILifetimeScope IoCScope { get; set; }
        public TemporaryStorage TemporaryStorage { get; }

        public TemporaryStorageMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Do something with context near the beginning of request processing.
            if (context.Request.Path.Value.EndsWith("upload.axd"))
            {
                //using (var scope = IoCGlobal.NewScope())
                //{
                //    var temporaryStorage = IoCGlobal.Resolve<TemporaryStorage>();

                var temporaryStorage = new TemporaryStorage();
                temporaryStorage.ProcessRequest(context);


                return;
                //}
            }

            await _next.Invoke(context);

            // Clean up.
        }

        //protected override void Dispose(bool disposing)
        //{
        //    //ReleaseBuffer(buffer); // release unmanaged memory  
        //    if (disposing)
        //    {
        //        if(this.IoCScope)
        //        this.IoCScope.Dispose();
        //    }
        //}

    }


    public static class TemporaryStorageMiddlewareExtension
    {
        public static IApplicationBuilder UseTempFileMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TemporaryStorageMiddleware>();
        }
    }

    public class TemporaryStorage
    {
        static LoggerCustom Logger = Framework.Logging.Log4Net.LoggerFactory.Create(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static string BaseTemporaryStorage => ConfigurationManager.AppSettings["FileStorageTemporaryFolder"];
        public TemporaryStorage()
        {

        }

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Method == "PUT")
            {
                //context.Response.Flush();
                //context.Response.End();
            }

            if (context.Request.Method == "POST")
            {
                var uploadFileResult = UploadFile(context);
                HttpHelpers.SendJson(context.Response, uploadFileResult);

            }

            if (context.Request.Method == "GET")
            {
                RetrieveFile(context);
            }

        }

        private List<TemporaryFileUpdatedResult> UploadFile(HttpContext context)
        {
            var files = context.Request.Form.Files;
            var result = new List<TemporaryFileUpdatedResult>();
            try
            {
                IFormFile file = null;
                for (var i = 0; i < files.Count; i++)
                {
                    file = files[i];

                    var fileEntry = new TemporaryFileUploadDTO(file.FileName, file.ContentType, file.OpenReadStream());
                    var resultItem = fileEntry.SaveFile(BaseTemporaryStorage);

                    result.Add(resultItem);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        private async Task RetrieveFile(HttpContext context)
        {
            var fileRepositoryIdStr = context.Request.Query["id"];
            bool returnThumbNail = true;
            {
                var returnThumbnailStr = context.Request.Query["thumbnail"];
                bool.TryParse(returnThumbnailStr, out returnThumbNail);
            }

            if (int.TryParse(fileRepositoryIdStr, out int fileRepositoryId))
            {
                //Scope name: AutofacWebRequest is mandatory to match the WebApi scope created. 
                //If the scope is changed some injections stop working with the message that "AutofacWebRequest" scope was not found.
                using (var scope = IoCGlobal.NewScope("AutofacWebRequest"))
                {
                    try
                    {

                        var getFileDataRetriever = IoCGlobal.Resolve<IFileRetriever>(null, scope);


                        var fileInfoResult = getFileDataRetriever.GetFileData(fileRepositoryId);
                        if (!fileInfoResult.IsSucceed)
                        {
                            return;
                        }

                        var fileInfo = fileInfoResult.Bag;

                        string mimeType = null;
                        var fileStorageService = IoCGlobal.Resolve<IFileStorageService>(fileInfo.FileSystemTypeId, scope);
                        byte[] result = null;
                        if (returnThumbNail)
                        {
                            var thumbnailFileName = fileInfo.ThumbnailFileName ?? fileInfo.FileName;
                            result = await fileStorageService.RetrieveFile(fileInfo.RootPath, fileInfo.AccessPath, fileInfo.RelativePath, thumbnailFileName);
                            mimeType = FileHelpers.GetMimeTypeByExtension(thumbnailFileName);
                        }
                        else
                        {
                            result = await fileStorageService.RetrieveFile(fileInfo.RootPath, fileInfo.AccessPath, fileInfo.RelativePath, fileInfo.FileName);
                            mimeType = FileHelpers.GetMimeTypeByExtension(fileInfo.FileName);
                        }


                        context.Response.OnStarting(state =>
                        {
                            var httpContext = (HttpContext)state;
                            httpContext.Response.ContentType = mimeType;
                            return Task.FromResult(0);
                        }, context);

                        var memStream = new MemoryStream(result);
                        memStream.CopyTo(context.Response.Body);
                        //context.Response.ContentType = mimeType;
                        context.Response.Body.Flush();

                    }
                    catch (Exception ex)
                    {
                        Logger.Error($"File Retrieve - {fileRepositoryId}", ex);
                        throw;
                    }
                    return;
                }
            }
        }
    }
}
