using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.PriceLevelInventoryItem.DeleteCommand.Models
{
    public class PriceLevelInventoryItemDeleteCommandOutputDTO
    {

        public PriceLevelInventoryItemDeleteCommandOutputDTO()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}