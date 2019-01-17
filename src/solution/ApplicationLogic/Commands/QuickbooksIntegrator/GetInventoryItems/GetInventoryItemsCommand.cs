using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationLogic.Commands.QuickbooksIntegrator.GetInventoryItems.Models;
using ApplicationLogic.Interfaces.Repositories.Quickbooks;
using Framework.Autofac;

namespace ApplicationLogic.Commands.QuickbooksIntegrator.GetInventoryItems
{
    public class GetInventoryItemsCommand : BaseIoCDisposable, IGetInventoryItemsCommand
    {
        public GetInventoryItemsCommand(ApplicationLogic.Interfaces.Repositories.Quickbooks.IInventoryRepository repository)
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
