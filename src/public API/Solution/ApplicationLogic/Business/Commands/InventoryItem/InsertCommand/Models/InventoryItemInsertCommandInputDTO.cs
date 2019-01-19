using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.InventoryItem.InsertCommand.Models
{
    public class InventoryItemInsertCommandInputDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductTypeId { get; set; }
    }
}