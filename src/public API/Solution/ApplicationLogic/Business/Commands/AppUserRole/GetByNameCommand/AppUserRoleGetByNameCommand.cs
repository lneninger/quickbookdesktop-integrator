using System;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.AppUserRole.GetByNameCommand.Models;
using Microsoft.AspNetCore.Identity;
using Framework.Core.Messages;

namespace ApplicationLogic.Business.Commands.AppUserRole.GetByNameCommand
{
    public class AppUserRoleGetByNameCommand : AbstractDBCommand<IdentityRole, IAppUserRoleDBRepository>, IAppUserRoleGetByNameCommand
    {

        public AppUserRoleGetByNameCommand(IDbContextScopeFactory dbContextScopeFactory, IAppUserRoleDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<AppUserRoleGetByNameCommandOutputDTO> Execute(string name)
        {
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                return this.Repository.GetByName(name);
            }
        }
    }
}
