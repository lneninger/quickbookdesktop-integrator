using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.InventoryAccount.GetAllCommand.Models;
using Framework.Core.Messages;
using System.Linq;

namespace ApplicationLogic.Business.Commands.InventoryAccount.GetAllCommand
{
    public class InventoryAccountGetAllCommand : AbstractDBCommand<DomainModel.InventoryAccount, IInventoryAccountDBRepository>, IInventoryAccountGetAllCommand
    {
        public InventoryAccountGetAllCommand(IDbContextScopeFactory dbContextScopeFactory, IInventoryAccountDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<IEnumerable<InventoryAccountGetAllCommandOutputDTO>> Execute()
        {
            var result = new OperationResponse<IEnumerable<InventoryAccountGetAllCommandOutputDTO>>();
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                var getAllResult = this.Repository.GetAll();
                result.AddResponse(getAllResult);
                if (result.IsSucceed)
                {
                    result.Bag = getAllResult.Bag.Select(entityItem => new InventoryAccountGetAllCommandOutputDTO
                    {
                        Id = entityItem.Id,
                        ExternalId = entityItem.ExternalId,
                        Name = entityItem.Name,
                        CreatedAt = entityItem.CreatedAt

                    }).ToList();
                }
            }

            return result;
        }
    }
}
