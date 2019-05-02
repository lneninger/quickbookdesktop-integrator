using ApplicationLogic.Commands.QuickbooksIntegrator.GetAccountByIds.Models;
using ApplicationLogic.Commands.QuickbooksIntegrator.GetPriceLevels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Commands.QuickbooksIntegrator.GetInventoryItems.Models
{
    public class SyncInventoryItemsInputIventoryItemDTO
    {
        public int IntegrationProcessId { get; set; }
        public IEnumerable<GetInventoryItemsOutputIventoryItemDTO> InventoryItems { get; set; }
        public IEnumerable<GetPriceLevelsOutputPriceLevelItemDTO> PriceLevels { get; set; }
        public IEnumerable<GetAccountByIdsOutputDTO> AccountIncomes { get; set; }
        public IEnumerable<GetAccountByIdsOutputDTO> AccountInventories { get; set; }
    }
}
