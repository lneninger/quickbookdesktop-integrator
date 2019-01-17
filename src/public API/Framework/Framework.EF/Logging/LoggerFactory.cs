using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.EF.Logging
{
    public class CustomLoggerFactory
    {
        public static readonly LoggerFactory LoggerFactoryImpl
        = new LoggerFactory(new[]
        {
        new ConsoleLoggerProvider((category, level)
            => category == DbLoggerCategory.Database.Command.Name
               && level == LogLevel.Information, true)
        });
    }
}
