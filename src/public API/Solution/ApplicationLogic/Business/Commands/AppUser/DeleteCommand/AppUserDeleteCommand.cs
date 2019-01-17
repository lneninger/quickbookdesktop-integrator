using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.AppUser.DeleteCommand.Models;
using Framework.Core.Messages;

namespace ApplicationLogic.Business.Commands.AppUser.DeleteCommand
{
    public class AppUserDeleteCommand : AbstractDBCommand<DomainModel.Identity.AppUser, IInventoryItemDBRepository>, IAppUserDeleteCommand
    {
        public AppUserDeleteCommand(IDbContextScopeFactory dbContextScopeFactory, IInventoryItemDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<AppUserDeleteCommandOutputDTO> Execute(string id)
        {
            return this.Repository.Delete(id);
        }
    }
}
