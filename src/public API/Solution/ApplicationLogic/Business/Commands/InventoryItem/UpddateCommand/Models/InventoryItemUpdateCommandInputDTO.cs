using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.InventoryItem.UpdateCommand.Models
{
    public class InventoryItemUpdateCommandInputDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ProductColorTypeId { get; set; }
    }
}