using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using Framework.Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Framework.Logging.Log4Net;

namespace ApplicationLogic.Business.Commands
{
    public class AbstractDBCommand<TEntity, TRepository>: BaseIoCDisposable where TEntity: class, new() where TRepository : IDBRepository
    {
        /// <summary>
        /// 
        /// </summary>
        public LoggerCustom Logger = Framework.Logging.Log4Net.LoggerFactory.Create(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public AbstractDBCommand(IDbContextScopeFactory dbContextScopeFactory, TRepository repository, FluentValidation.IValidator validator = null)
        {
            this.DbContextScopeFactory = dbContextScopeFactory;
            this.Repository = repository;
            this.Validator = validator;
        }

        public IDbContextScopeFactory DbContextScopeFactory { get; }
        public TRepository Repository { get; }
        public IValidator Validator { get; }

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
