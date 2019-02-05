using System;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.InventoryAccount.GetByIdCommand.Models;
using Framework.Core.Messages;
using System.Linq;
using ApplicationLogic.Business.Commons.DTOs;

namespace ApplicationLogic.Business.Commands.InventoryAccount.GetByIdCommand
{
    public class InventoryAccountGetByIdCommand : AbstractDBCommand<DomainModel.InventoryAccount, IInventoryAccountDBRepository>, IInventoryAccountGetByIdCommand
    {

        public InventoryAccountGetByIdCommand(IDbContextScopeFactory dbContextScopeFactory, IInventoryAccountDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<InventoryAccountGetByIdCommandOutputDTO> Execute(int id)
        {
            var result = new OperationResponse<InventoryAccountGetByIdCommandOutputDTO>();
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                var getByIdResult = this.Repository.GetById(id);
                result.AddResponse(getByIdResult);
                if (result.IsSucceed)
                {
                    
                    result.Bag = new InventoryAccountGetByIdCommandOutputDTO
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
