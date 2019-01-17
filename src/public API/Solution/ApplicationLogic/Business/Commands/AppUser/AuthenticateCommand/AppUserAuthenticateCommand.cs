using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.AppUser.AuthenticateCommand.Models;
using Framework.Core.Crypto;
using Framework.Core.Messages;

namespace ApplicationLogic.Business.Commands.AppUser.AuthenticateCommand
{
    public class AppUserAuthenticateCommand : AbstractDBCommand<DomainModel.Identity.AppUser, IAppUserDBRepository>, IAppUserAuthenticateCommand
    {
        public AppUserAuthenticateCommand(IDbContextScopeFactory dbContextScopeFactory, IAppUserDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<AppUserAuthenticateCommandOutputDTO> Execute(AppUserAuthenticateCommandInputDTO input)
        {
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                return this.Repository.Authenticate(input);
            }
        }
    }
}
