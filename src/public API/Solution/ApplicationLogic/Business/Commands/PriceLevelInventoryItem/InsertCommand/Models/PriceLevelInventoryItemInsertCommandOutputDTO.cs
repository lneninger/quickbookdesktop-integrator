using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.PriceLevelInventoryItem.InsertCommand.Models
{
    public class PriceLevelInventoryItemInsertCommandOutputDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ERPId { get; set; }
    }
}