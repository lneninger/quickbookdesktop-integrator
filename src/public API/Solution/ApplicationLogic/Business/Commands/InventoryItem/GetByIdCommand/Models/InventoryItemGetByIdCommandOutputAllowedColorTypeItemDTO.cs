using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLogic.Business.Commands.InventoryItem.GetByIdCommand.Models
{
    public class InventoryItemGetByIdCommandOutputAllowedColorTypeItemDTO
    {
        public int Id { get; set; }

        public string ProductColorTypeId { get; set; }
    }
}
