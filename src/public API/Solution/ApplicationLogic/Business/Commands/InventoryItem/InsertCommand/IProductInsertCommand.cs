using ApplicationLogic.Business.Commands.Product.InsertCommand.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.Product.InsertCommand
{
    public interface IProductInsertCommand: ICommandFunc<ProductInsertCommandInputDTO, OperationResponse<ProductInsertCommandOutputDTO>>
    {
    }
}