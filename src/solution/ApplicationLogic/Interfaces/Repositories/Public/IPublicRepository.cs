using ApplicationLogic.Commands.QuickbooksIntegrator.GetInventoryItems.Models;
using DatabaseSchema;
using Framework.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Interfaces.Repositories.Database
{
    public interface IPublicRepository
    {
        OperationResponse SendInventoryItem(IEnumerable<SyncInventoryItemsInputIventoryItemDTO> inventoryItemDTO);
    }
}
