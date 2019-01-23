using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.InventoryItem.SyncCommand.Models
{
    public class InventoryItemSyncCommandInputItemDTO
    {
        public string FullName { get; set; }
        public string Name { get; set; }
        public double? Stock { get; set; }
        public double? Cost { get; set; }
        public string SaleDescription { get; set; }
        public double? SalePrice { get; set; }
    }
}