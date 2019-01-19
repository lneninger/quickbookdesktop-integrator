using ApplicationLogic.Commands.QuickbooksIntegrator.GetInventoryItems.Models;
using ApplicationLogic.Interfaces.Repositories.Quickbooks;
using Framework.Autofac;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Commands.QuickbooksIntegrator.SyncInventoryItems
{
    public class SyncInventoryItemsCommand : BaseIoCDisposable, ISyncInventoryItemsCommand
    {
        public SyncInventoryItemsCommand(IInventoryRepository repository)
        {
            this.Repository = repository;
        }

        public IInventoryRepository Repository { get; }

        public OperationResponse Execute(IEnumerable<SyncInventoryItemsOutputIventoryItemDTO> input)
        {
            throw new System.Exception();
        }
    }
}
