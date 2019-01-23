using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.InventoryItem.SyncCommand.Models
{
    public class InventoryItemSyncCommandOutputDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ERPId { get; set; }
    }
}