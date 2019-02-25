using ApplicationLogic.Commands.QuickbooksIntegrator.GetInventoryItems.Models;
using ApplicationLogic.Interfaces.Repositories.Quickbooks;
using Framework.Autofac;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationLogic.Commands.QuickbooksIntegrator.GetInventoryItems
{
    public class GetInventoryItemsCommand : BaseIoCDisposable, IGetInventoryItemsCommand
    {
        public GetInventoryItemsCommand(IGeneralRepository repository)
        {
            this.Repository = repository;
        }

        public IGeneralRepository Repository { get; }

        public IEnumerable<GetInventoryItemsOutputIventoryItemDTO> Execute()
        {
            var items = this.Repository.InventoryItemGetAll();

            

            return items;
        }
    }
}
