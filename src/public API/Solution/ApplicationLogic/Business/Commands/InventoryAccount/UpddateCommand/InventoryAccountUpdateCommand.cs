using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.InventoryAccount.UpdateCommand.Models;
using Framework.Core.Messages;

namespace ApplicationLogic.Business.Commands.InventoryAccount.UpdateCommand
{
    public class InventoryAccountUpdateCommand : AbstractDBCommand<DomainModel.InventoryAccount, IInventoryAccountDBRepository>, IInventoryAccountUpdateCommand
    {
        public InventoryAccountUpdateCommand(IDbContextScopeFactory dbContextScopeFactory, IInventoryAccountDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<InventoryAccountUpdateCommandOutputDTO> Execute(InventoryAccountUpdateCommandInputDTO input)
        {
            var result = new OperationResponse<InventoryAccountUpdateCommandOutputDTO>();
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                var getByIdResult = this.Repository.GetById(input.Id);
                result.AddResponse(getByIdResult);
                if (result.IsSucceed)
                {
                    getByIdResult.Bag.Name = input.Name;
                    
                    try
                    {
                        dbContextScope.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        result.AddError("Error updating Inventory Item", ex);
                    }

                    getByIdResult = this.Repository.GetById(input.Id);
                    result.AddResponse(getByIdResult);
                    if (result.IsSucceed)
                    {
                        result.Bag = new InventoryAccountUpdateCommandOutputDTO
                        {
                            Id = getByIdResult.Bag.Id,
                            ExternalId = getByIdResult.Bag.ExternalId,
                            Name = getByIdResult.Bag.Name,
                            FullName = getByIdResult.Bag.FullName
                        };
                    }

                }
            }

            return result;
        }
    }
}
