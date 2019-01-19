using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.InventoryItem.DeleteCommand.Models
{
    public class InventoryItemDeleteCommandOutputDTO
    {

        public InventoryItemDeleteCommandOutputDTO()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}