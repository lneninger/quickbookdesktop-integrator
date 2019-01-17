using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Logging.Log4Net
{
    public class LoggerFactory
    {
        public const string CustomLogger_Elmah = "Elmah";
        public const string CustomLogger_ActivityLog = "ActivityLog";

        public static LoggerCustom Create(Type loggerTypeRef) {

            return new LoggerCustom(loggerTypeRef);
        }
    }
}
