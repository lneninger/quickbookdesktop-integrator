using ApplicationLogic.Interfaces.Repositories.Database;
using Framework.Autofac;
using Framework.Logging.Log4Net;

namespace ApplicationLogic.Business.Commands
{
    public class AbstractDBCommand<TEntity, TRepository>: BaseIoCDisposable where TEntity: class, new() where TRepository : IDbRepository
    {
        /// <summary>
        /// 
        /// </summary>
        public LoggerCustom Logger = Framework.Logging.Log4Net.LoggerFactory.Create(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public AbstractDBCommand(TRepository repository)
        {
            this.Repository = repository;
        }

        public TRepository Repository { get; }

        protected override void Dispose(bool disposing)
        {
            //ReleaseBuffer(buffer); // release unmanaged memory  
            if (disposing)
            { // release other disposable objects  
                IoCGlobal.MarkInstanceForDisposal(this);
                
                //if (resource != null) resource.Dispose();
            }
        }
        
    }
}
