using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.PriceLevelInventoryItem.UpdateCommand.Models
{
    public class PriceLevelInventoryItemUpdateCommandInputDTO
    {
        public int Id { get; set; }

        public decimal? CustomPrice { get; set; }

        public string ProductColorTypeId { get; set; }
    }
}