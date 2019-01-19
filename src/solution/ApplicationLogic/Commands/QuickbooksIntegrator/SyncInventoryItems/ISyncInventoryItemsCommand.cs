using ApplicationLogic.Commands.QuickbooksIntegrator.GetInventoryItems.Models;
using Framework.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Commands.QuickbooksIntegrator.SyncInventoryItems
{
    public interface ISyncInventoryItemsCommand : ICommandFunc<IEnumerable<SyncInventoryItemsOutputIventoryItemDTO>, OperationResponse>
    {
    }
}
