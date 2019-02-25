using System;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.PriceLevel.GetByIdCommand.Models;
using Framework.Core.Messages;
using System.Linq;
using ApplicationLogic.Business.Commons.DTOs;

namespace ApplicationLogic.Business.Commands.PriceLevel.GetByIdCommand
{
    public class PriceLevelGetByIdCommand : AbstractDBCommand<DomainModel.PriceLevel, IPriceLevelDBRepository>, IPriceLevelGetByIdCommand
    {

        public PriceLevelGetByIdCommand(IDbContextScopeFactory dbContextScopeFactory, IPriceLevelDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<PriceLevelGetByIdCommandOutputDTO> Execute(int id)
        {
            var result = new OperationResponse<PriceLevelGetByIdCommandOutputDTO>();
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                var getByIdResult = this.Repository.GetById(id);
                result.AddResponse(getByIdResult);
                if (result.IsSucceed)
                {
                    
                    result.Bag = new PriceLevelGetByIdCommandOutputDTO
                    {
                        Id = getByIdResult.Bag.Id,
                        Name = getByIdResult.Bag.Name,
                        
                    };

                    
                }
            }

            return result;



        }
    }
}
