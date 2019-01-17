using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiverdaleMainApp2_0.AppSettings
{
    /// <summary>
    /// Application Custom Settings entire section representation. Come from appSettings.json
    /// </summary>
    public class CustomSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether [active execution trace interceptor].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [active execution trace interceptor]; otherwise, <c>false</c>.
        /// </value>
        public bool ActiveExecutionTraceInterceptor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string[] AllowedOrigins { get; set; }
    }
}
