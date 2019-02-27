using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.Sync.SyncFromDesktopCommand.Models
{
    public class SyncFromDesktopCommandInputPriceLevelDTO
    {
        public SyncFromDesktopCommandInputPriceLevelDTO()
        {
            this.InventoryItems = new List<SyncFromDesktopCommandInputPriceLevelInventoryItemDTO>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// FixedPercentage,
        /// PerItem
        /// </summary>
        public string PriceLevelType { get; set; }
        public bool IsActive { get; set; }
        public decimal? PriceLevelPercentage { get; set; }

        public List<SyncFromDesktopCommandInputPriceLevelInventoryItemDTO> InventoryItems { get; set; }
    }
}