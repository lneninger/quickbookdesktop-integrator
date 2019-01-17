using ApplicationLogic.Business.Commands.Product.GetAllCommand.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.Product.GetAllCommand
{
    public interface IProductGetAllCommand: ICommandAction<OperationResponse<IEnumerable<ProductGetAllCommandOutputDTO>>>
    {
    }
}