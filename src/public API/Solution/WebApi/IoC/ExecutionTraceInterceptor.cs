using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RiverdaleMainApp2_0.AppSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiverdaleMainApp2_0.IoC
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Castle.DynamicProxy.IInterceptor" />
    public class ExecutionTraceInterceptor : IInterceptor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionTraceInterceptor"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public ExecutionTraceInterceptor(CustomSettings settings)
        {
            this.CustomSettings = settings;
        }

        /// <summary>
        /// Gets the custom settings.
        /// </summary>
        /// <value>
        /// The custom settings.
        /// </value>
        public CustomSettings CustomSettings { get; }

        /// <summary>
        /// Intercepts the specified invocation.
        /// </summary>
        /// <param name="invocation">The invocation.</param>
        public void Intercept(IInvocation invocation)
        {

            if (this.CustomSettings.ActiveExecutionTraceInterceptor != true)
            {
                invocation.Proceed();
            }
            else
            {


                var loggerPath = invocation.Method.DeclaringType;
                var logger = Framework.Logging.Log4Net.LoggerFactory.Create(loggerPath);
                var @params = new List<string>();
                string jsonParam = null;
                JsonSerializerSettings jsonSettings = new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.All,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                };

                object invParam = null;
                for (var i = 0; i < invocation.Arguments.Length; i++)
                {
                    try
                    {
                        invParam = invocation.Arguments.ElementAt(i);
                        jsonParam = JsonConvert.SerializeObject(invParam, jsonSettings);
                        @params.Add(jsonParam);
                    }
                    catch (Exception)
                    {

                    }
                }

                if (invocation.GenericArguments != null)
                {
                    for (var i = 0; i < invocation.GenericArguments.Length; i++)
                    {
                        try
                        {
                            invParam = invocation.Arguments.ElementAt(i);
                            jsonParam = JsonConvert.SerializeObject(invParam, jsonSettings);
                            @params.Add(jsonParam);
                        }
                        catch (Exception)
                        {

                        }
                    }
                }

                var name = $"{invocation.Method.DeclaringType}.{invocation.Method.Name}";
                var mainMessage = $"Invoking method {name} \n " + $"Parameters: \n {string.Join("\n", @params)} \n";
                logger.Info(mainMessage);

                try
                {
                    invocation.Proceed();

                    var endMessage = $"End - {mainMessage} ";
                    logger.Info(endMessage);

                }
                catch (Exception ex)
                {
                   logger.Error(mainMessage, ex);
                    throw;
                }
            }
        }
    }
}
