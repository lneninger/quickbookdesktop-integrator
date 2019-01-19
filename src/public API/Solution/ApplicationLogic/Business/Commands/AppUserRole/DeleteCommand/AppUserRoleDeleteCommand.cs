using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.AppUserRole.DeleteCommand.Models;
using Microsoft.AspNetCore.Identity;
using Framework.Core.Messages;

namespace ApplicationLogic.Business.Commands.AppUserRole.DeleteCommand
{
    public class AppUserRoleDeleteCommand : AbstractDBCommand<IdentityRole, IAppUserRoleDBRepository>, IAppUserRoleDeleteCommand
    {
        public AppUserRoleDeleteCommand(IDbContextScopeFactory dbContextScopeFactory, IAppUserRoleDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<AppUserRoleDeleteCommandOutputDTO> Execute(string id)
        {
            return this.Repository.Delete(id);
        }
    }
}
