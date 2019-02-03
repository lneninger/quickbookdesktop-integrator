using ApplicationLogic.Commands.QuickbooksIntegrator.GetInventoryItems.Models;
using DatabaseSchema;
using Framework.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Interfaces.Repositories.Remote
{
    public interface IPublicRepository
    {
        OperationResponse SendInventoryItem(SyncInventoryItemsInputIventoryItemDTO inventoryItemDTO);
    }
}
