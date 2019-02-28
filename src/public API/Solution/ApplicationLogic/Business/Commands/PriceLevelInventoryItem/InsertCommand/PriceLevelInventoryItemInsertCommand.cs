using ApplicationLogic.Business.Commands.PriceLevelInventoryItem.InsertCommand.Models;
using ApplicationLogic.Repositories.DB;
using EntityFrameworkCore.DbContextScope;
using Framework.Core.Messages;
using System;

namespace ApplicationLogic.Business.Commands.PriceLevelInventoryItem.InsertCommand
{
    public class PriceLevelInventoryItemInsertCommand : AbstractDBCommand<DomainModel.PriceLevelInventoryItem, IPriceLevelInventoryItemDBRepository>, IPriceLevelInventoryItemInsertCommand
    {
        public PriceLevelInventoryItemInsertCommand(IDbContextScopeFactory dbContextScopeFactory, IPriceLevelInventoryItemDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<PriceLevelInventoryItemInsertCommandOutputDTO> Execute(PriceLevelInventoryItemInsertCommandInputDTO input)
        {
            var result = new OperationResponse<PriceLevelInventoryItemInsertCommandOutputDTO>();
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                var entity = new DomainModel.PriceLevelInventoryItem
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
                        result.Bag = new PriceLevelInventoryItemInsertCommandOutputDTO
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
