using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLogic.Business.Commands.Sync.SyncFromDesktopCommand.Models
{
    public class SyncFromDesktopCommandInputPriceLevelInventoryItemDTO
    {
        public string ItemId { get; set; }
        public string ItemFullName { get; set; }
        public short? Type { get; set; }
        public decimal? CustomPrice { get; set; }
        public decimal? CustomPricePercent { get; set; }
    }
}
