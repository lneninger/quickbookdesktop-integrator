using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiverdaleMainApp2_0.IoC
{
    /// <summary>
    /// API Exception Interceptor
    /// </summary>
    /// <seealso cref="Castle.DynamicProxy.IInterceptor" />
    public class ExceptionInterceptor : IInterceptor
    {
        /// <summary>
        /// Intercepts the specified invocation.
        /// </summary>
        /// <param name="invocation">The invocation.</param>
        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
        }
    }
}
