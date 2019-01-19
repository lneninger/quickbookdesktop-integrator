using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.InventoryItem.InsertCommand.Models
{
    public class InventoryItemInsertCommandOutputDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ERPId { get; set; }
    }
}