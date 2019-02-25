using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.PriceLevel.PageQueryCommand.Models;
using Framework.EF.DbContextImpl.Persistance.Paging.Models;
using Framework.Core.Messages;

namespace ApplicationLogic.Business.Commands.PriceLevel.PageQueryCommand
{
    public class PriceLevelPageQueryCommand : AbstractDBCommand<DomainModel.PriceLevel, IPriceLevelDBRepository>, IPriceLevelPageQueryCommand
    {
        public PriceLevelPageQueryCommand(IDbContextScopeFactory dbContextScopeFactory, IPriceLevelDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<PageResult<PriceLevelPageQueryCommandOutputDTO>> Execute(PageQuery<PriceLevelPageQueryCommandInputDTO> input)
        {
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                return this.Repository.PageQuery(input);
            }
        }
    }
}
