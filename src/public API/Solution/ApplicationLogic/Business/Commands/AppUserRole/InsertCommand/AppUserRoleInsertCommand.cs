using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.AppUserRole.InsertCommand.Models;
using Framework.Core.Crypto;
using Framework.Core.Messages;
using Microsoft.AspNetCore.Identity;

namespace ApplicationLogic.Business.Commands.AppUserRole.InsertCommand
{
    public class AppUserRoleInsertCommand : AbstractDBCommand<IdentityRole, IAppUserRoleDBRepository>, IAppUserRoleInsertCommand
    {
        public AppUserRoleInsertCommand(IDbContextScopeFactory dbContextScopeFactory, IAppUserRoleDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<AppUserRoleInsertCommandOutputDTO> Execute(AppUserRoleInsertCommandInputDTO input)
        {
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                return this.Repository.Insert(input);
            }
        }
    }
}
