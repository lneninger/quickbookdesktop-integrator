using ApplicationLogic.Commands.QuickbooksIntegrator.GetInventoryItems.Models;
using ApplicationLogic.Commands.QuickbooksIntegrator.SyncInventoryItems.Models;
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
        /// <summary>
        /// Requests the integration process.
        /// </summary>
        /// <returns></returns>
        OperationResponse<IntegrationProcessDTO> RequestIntegrationProcess();

        /// <summary>
        /// Sends the inventory item.
        /// </summary>
        /// <param name="inventoryItemDTO">The inventory item dto.</param>
        /// <returns></returns>
        OperationResponse SendInventoryItem(SyncInventoryItemsInputIventoryItemDTO inventoryItemDTO);
    }
}
