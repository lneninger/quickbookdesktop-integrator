using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Autofac
{
    public class BaseIoCDisposable : IDisposable
    {
        protected virtual void Dispose(bool disposing)
        {
            //ReleaseBuffer(buffer); // release unmanaged memory  
            if (disposing)
            { // release other disposable objects  
              // if (resource != null) resource.Dispose();
            }
        }

        ~BaseIoCDisposable()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
