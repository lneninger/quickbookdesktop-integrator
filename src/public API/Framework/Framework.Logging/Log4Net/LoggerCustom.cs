using log4net;
using log4net.Appender;
using log4net.Repository.Hierarchy;
using System;

namespace Framework.Logging.Log4Net
{
    public class LoggerCustom
    {
        public Type LoggerTypeRef { get; set; }
        public ILog InternalLog { get; protected set; }

        public LoggerCustom(Type loggerTypeRef)
        {
            this.LoggerTypeRef = loggerTypeRef;
            this.InternalLog = LogManager.GetLogger(loggerTypeRef);
        }


        public bool IsDebugEnabled
        {
            get
            {
                return this.InternalLog.IsDebugEnabled;
            }
        }
        public bool IsInfoEnabled
        {
            get
            {
                return this.InternalLog.IsInfoEnabled;
            }
        }
        public bool IsWarnEnabled
        {
            get
            {
                return this.InternalLog.IsWarnEnabled;
            }
        }
        public bool IsErrorEnabled
        {
            get
            {
                return this.InternalLog.IsErrorEnabled;
            }
        }
        public bool IsFatalEnabled
        {
            get
            {
                return this.InternalLog.IsFatalEnabled;
            }
        }


        /* Log a message object */
        public void Debug(object message)
        {
            this.InternalLog.Debug(message);
        }

        public void Info(object message)
        {
            this.InternalLog.Info(message);
        }

        public void Warn(object message)
        {
            this.InternalLog.Warn(message);
        }

        public void Error(object message)
        {
            this.InternalLog.Error(message);
        }

        public void Fatal(object message)
        {
            this.InternalLog.Fatal(message);
        }

        /* Log a message object and exception */
        public void Debug(object message, Exception t)
        {
            this.InternalLog.Debug(message, t);
        }

        public void Info(object message, Exception t)
        {
            this.InternalLog.Info(message, t);
        }

        public void Warn(object message, Exception t)
        {
            this.InternalLog.Warn(message, t);
        }

        public void Error(object message, Exception t)
        {
            this.InternalLog.Error(message, t);
        }

        public void Fatal(object message, Exception t)
        {
            this.InternalLog.Fatal(message, t);
        }

        public void DebugFormat(string format, params object[] args)
        {
            this.InternalLog.DebugFormat(format, args);
        }

        public void InfoFormat(string format, params object[] args)
        {
            this.InternalLog.InfoFormat(format, args);
        }

        public void WarnFormat(string format, params object[] args)
        {
            this.InternalLog.WarnFormat(format, args);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            this.InternalLog.ErrorFormat(format, args);
        }

        public void FatalFormat(string format, params object[] args)
        {
            this.InternalLog.FatalFormat(format, args);
        }

        /* Log a message string using the System.String.Format syntax */
        public void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            this.InternalLog.DebugFormat(provider, format, args);
        }

        public void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            this.InternalLog.InfoFormat(provider, format, args);
        }
        public void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            this.InternalLog.WarnFormat(provider, format, args);
        }

        public void SetContextProperty(LoggerCustom.ContextEnum contextEnum, string propertyName, object propertyValue)
        {
            switch (contextEnum)
            {
                case ContextEnum.Global:
                    log4net.GlobalContext.Properties[propertyName] = propertyValue;
                    break;

                case ContextEnum.Thread:
                    log4net.ThreadContext.Properties[propertyName] = propertyValue;
                    break;

                case ContextEnum.LogicalThread:
                    log4net.ThreadContext.Properties[propertyName] = propertyValue;
                    break;

                default:
                    throw new ArgumentException("Log Context doesn't exists");
            }
        }

        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            this.InternalLog.ErrorFormat(provider, format, args);
        }
        public void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            this.InternalLog.FatalFormat(provider, format, args);
        }


        public enum ContextEnum
        {
            Global,
            Thread,
            LogicalThread
        }


        public void FlushBuffers()
        {
            ILog log = this.InternalLog;// LogManager.GetLogger("whatever");
            var logger = log.Logger as Logger;
            if (logger != null)
            {
                foreach (IAppender appender in logger.Appenders)
                {
                    var buffered = appender as BufferingAppenderSkeleton;
                    if (buffered != null)
                    {
                        buffered.Flush();
                    }
                }
            }
        }
    }
}
