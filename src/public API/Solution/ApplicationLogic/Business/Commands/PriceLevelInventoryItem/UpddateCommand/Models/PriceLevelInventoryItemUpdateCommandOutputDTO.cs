using System;

namespace ApplicationLogic.Business.Commands.PriceLevelInventoryItem.UpdateCommand.Models
{
    public class PriceLevelInventoryItemUpdateCommandOutputDTO
    {
        public int Id { get; set; }

        public string ExternalItemId { get; set; }

        public decimal? CustomPrice { get; set; }

        public decimal? CustomPricePercent { get; set; }
    }
}