using ApplicationLogic.Commands.QuickbooksIntegrator.GetInventoryItems.Models;
using ApplicationLogic.Interfaces.Repositories.Quickbooks;
using Framework.Autofac;
using System.Collections.Generic;

namespace ApplicationLogic.Commands.QuickbooksIntegrator.GetInventoryItems
{
    public class GetInventoryItemsCommand : BaseIoCDisposable, IGetInventoryItemsCommand
    {
        public GetInventoryItemsCommand(IInventoryRepository repository)
        {
            this.Repository = repository;
        }

        public IInventoryRepository Repository { get; }

        public IEnumerable<GetInventoryItemsOutputIventoryItemDTO> Execute()
        {
            return this.Repository.GetAll();
        }
    }
}
