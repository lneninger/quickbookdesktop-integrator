using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.InventoryAccount.PageQueryCommand.Models;
using Framework.EF.DbContextImpl.Persistance.Paging.Models;
using Framework.Core.Messages;

namespace ApplicationLogic.Business.Commands.InventoryAccount.PageQueryCommand
{
    public class InventoryAccountPageQueryCommand : AbstractDBCommand<DomainModel.InventoryAccount, IInventoryAccountDBRepository>, IInventoryAccountPageQueryCommand
    {
        public InventoryAccountPageQueryCommand(IDbContextScopeFactory dbContextScopeFactory, IInventoryAccountDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<PageResult<InventoryAccountPageQueryCommandOutputDTO>> Execute(PageQuery<InventoryAccountPageQueryCommandInputDTO> input)
        {
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                return this.Repository.PageQuery(input);
            }
        }
    }
}
