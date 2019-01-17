using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class CustomAuthorize : AuthorizeAttribute
    {
        public CustomAuthorize(params string[] permissions): base()
        {

        }
       
    }
}
