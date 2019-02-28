using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.PriceLevelInventoryItem.PageQueryCommand.Models;
using Framework.EF.DbContextImpl.Persistance.Paging.Models;
using Framework.Core.Messages;

namespace ApplicationLogic.Business.Commands.PriceLevelInventoryItem.PageQueryCommand
{
    public class PriceLevelInventoryItemPageQueryCommand : AbstractDBCommand<DomainModel.PriceLevelInventoryItem, IPriceLevelInventoryItemDBRepository>, IPriceLevelInventoryItemPageQueryCommand
    {
        public PriceLevelInventoryItemPageQueryCommand(IDbContextScopeFactory dbContextScopeFactory, IPriceLevelInventoryItemDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<PageResult<PriceLevelInventoryItemPageQueryCommandOutputDTO>> Execute(PageQuery<PriceLevelInventoryItemPageQueryCommandInputDTO> input)
        {
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                return this.Repository.PageQuery(input);
            }
        }
    }
}
