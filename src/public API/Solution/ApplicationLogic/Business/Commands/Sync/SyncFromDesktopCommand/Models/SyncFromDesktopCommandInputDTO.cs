using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.Sync.SyncFromDesktopCommand.Models
{
    public class SyncFromDesktopCommandInputDTO
    {
        public List<SyncFromDesktopCommandInputInventoryItemDTO> InventoryItems { get; set; }
        public List<SyncFromDesktopCommandInputAccountIncomeDTO> AccountIncomes { get; set; }
        public List<SyncFromDesktopCommandInputAccountInventoryDTO> AccountInventories { get; set; }
        public List<SyncFromDesktopCommandInputPriceLevelDTO> PriceLevels { get; set; }
    }
}