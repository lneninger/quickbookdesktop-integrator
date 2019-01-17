using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Hosting;

namespace Framework.Core.Helpers
{
    public class PlatformHelper
    {
        public static PlatformEnum ApplicationRuntimePlatform() {
            if (HostingEnvironment.AppDomainAppId != null)
            {
                //is web app
                return PlatformEnum.WEB;
            }
            else
            {
                //is windows app
                return PlatformEnum.WINDOWS;
            }
        }
    }

    public enum PlatformEnum
    {
        WEB,
        WINDOWS
    }
}
