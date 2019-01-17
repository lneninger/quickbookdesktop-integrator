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

namespace Framework.Storage.FileStorage.TemporaryStorage
{
    public class TemporaryStorageMiddleware//IHttpModule
    {
        private readonly RequestDelegate _next;

        public static string BaseTemporaryStorage => ConfigurationManager.AppSettings["FileStorageTemporaryFolder"];
        public TemporaryStorageMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Do something with context near the beginning of request processing.
            if (context.Request.Path.Value.EndsWith("upload.axd"))
            {
                var temporaryStorage = new TemporaryStorage();
                var result = temporaryStorage.ProcessRequest(context);

                HttpHelpers.SendJson(context.Response, result);

                return;
            }

            await _next.Invoke(context);

            // Clean up.
        }

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
        public static string BaseTemporaryStorage => ConfigurationManager.AppSettings["FileStorageTemporaryFolder"];
        public TemporaryStorage()
        {

        }

        public bool IsReusable
        {
            get { return false; }
        }

        public List<TemporaryFileUpdatedResult> ProcessRequest(HttpContext context)
        {



            if (context.Request.Method == "PUT")
            {
                return null;
            }


            var files = context.Request.Form.Files;
            var result = new List<TemporaryFileUpdatedResult>();
            try
            {
                IFormFile file = null;
                for (var i = 0; i < files.Count; i++)
                {
                    file = files[i];
                    var fileEntry = new TemporaryFileUploadDTO(file);
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
    }
}
