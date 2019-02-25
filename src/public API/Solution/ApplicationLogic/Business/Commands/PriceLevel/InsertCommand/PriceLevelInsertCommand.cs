using ApplicationLogic.Business.Commands.PriceLevel.InsertCommand.Models;
using ApplicationLogic.Repositories.DB;
using EntityFrameworkCore.DbContextScope;
using Framework.Core.Messages;
using System;

namespace ApplicationLogic.Business.Commands.PriceLevel.InsertCommand
{
    public class PriceLevelInsertCommand : AbstractDBCommand<DomainModel.PriceLevel, IPriceLevelDBRepository>, IPriceLevelInsertCommand
    {
        public PriceLevelInsertCommand(IDbContextScopeFactory dbContextScopeFactory, IPriceLevelDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<PriceLevelInsertCommandOutputDTO> Execute(PriceLevelInsertCommandInputDTO input)
        {
            var result = new OperationResponse<PriceLevelInsertCommandOutputDTO>();
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                var entity = new DomainModel.PriceLevel
                {
                        Name = input.Name,
                    };
                

                try
                {
                    var insertResult = this.Repository.Insert(entity);
                    result.AddResponse(insertResult);
                    if (result.IsSucceed)
                    {
                        dbContextScope.SaveChanges();

                    }
                }
                catch (Exception ex)
                {
                    result.AddError("Error Adding Product", ex);
                }

                if (result.IsSucceed)
                {
                    var getByIdResult = this.Repository.GetById(entity.Id);
                    result.AddResponse(getByIdResult);
                    if (result.IsSucceed)
                    {
                        result.Bag = new PriceLevelInsertCommandOutputDTO
                        {
                            Id = getByIdResult.Bag.Id,
                            Name = getByIdResult.Bag.Name
                        };
                    }

                }
            }

            return result;
        }
    }
}
