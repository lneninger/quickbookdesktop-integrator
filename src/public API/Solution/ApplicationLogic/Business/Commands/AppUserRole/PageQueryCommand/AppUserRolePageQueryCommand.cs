using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.AppUserRole.PageQueryCommand.Models;
using Framework.EF.DbContextImpl.Persistance.Paging.Models;
using Microsoft.AspNetCore.Identity;
using Framework.Core.Messages;

namespace ApplicationLogic.Business.Commands.AppUserRole.PageQueryCommand
{
    public class AppUserRolePageQueryCommand : AbstractDBCommand<IdentityRole, IAppUserRoleDBRepository>, IAppUserRolePageQueryCommand
    {
        public AppUserRolePageQueryCommand(IDbContextScopeFactory dbContextScopeFactory, IAppUserRoleDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<PageResult<AppUserRolePageQueryCommandOutputDTO>> Execute(PageQuery<AppUserRolePageQueryCommandInputDTO> input)
        {
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                return this.Repository.PageQuery(input);
            }
        }
    }
}
