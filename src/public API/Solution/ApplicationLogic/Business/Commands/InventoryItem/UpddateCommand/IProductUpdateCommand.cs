using ApplicationLogic.Business.Commands.Product.UpdateCommand.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.Product.UpdateCommand
{
    public interface IProductUpdateCommand: ICommandFunc<ProductUpdateCommandInputDTO, OperationResponse<ProductUpdateCommandOutputDTO>>
    {
    }
}