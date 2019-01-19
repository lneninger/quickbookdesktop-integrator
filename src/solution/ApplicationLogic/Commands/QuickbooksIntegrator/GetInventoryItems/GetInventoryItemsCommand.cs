using ApplicationLogic.Commands.QuickbooksIntegrator.GetInventoryItems.Models;
using ApplicationLogic.Interfaces.Repositories.Quickbooks;
using Framework.Autofac;
using System.Collections.Generic;

namespace ApplicationLogic.Commands.QuickbooksIntegrator.GetInventoryItems
{
    public class GetInventoryItemsCommand : BaseIoCDisposable, IGetInventoryItemsCommand
    {
        public GetInventoryItemsCommand(IInventoryItemRepository repository)
        {
            this.Repository = repository;
        }

        public IInventoryItemRepository Repository { get; }

        public IEnumerable<GetInventoryItemsOutputIventoryItemDTO> Execute()
        {
            return this.Repository.GetAll();
        }
    }
}
