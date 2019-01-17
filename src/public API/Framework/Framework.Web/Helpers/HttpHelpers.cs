using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Framework.Web.Helpers
{
    public class HttpHelpers
    {

        public static string GetProtocolAndDomain(Uri uri)
        {
            string retUri = null;

            if (uri != null)
            {
                try
                {
                    retUri = uri.GetLeftPart(UriPartial.Authority);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"GetProtocolAndDomain Error: [{ex.Message}]");
                    //throw;
                }
            }
            return retUri;
        }

        public static string GetProtocolAndDomain(string uriStr)
        {
            string retUri = null;

            if (!String.IsNullOrEmpty(uriStr))
            {
                try
                {
                    retUri = GetProtocolAndDomain(new Uri(uriStr));
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"GetProtocolAndDomain Error: [{ex.Message}]");
                    //throw;
                }
            }
            return retUri;
        }

        public static void SendJson(HttpResponse response, object responseObj)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(responseObj, settings);

            //response.Clear();
            response.ContentType = "application/json; charset=utf-8";
            response.WriteAsync(json);
            //response.Flush();
        }

        
    }
}
