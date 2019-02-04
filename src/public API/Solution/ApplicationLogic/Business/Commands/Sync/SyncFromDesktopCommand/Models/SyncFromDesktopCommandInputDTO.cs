using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.Sync.SyncFromDesktopCommand.Models
{
    public class SyncFromDesktopCommandInputDTO
    {
        public List<SyncFromDesktopCommandInputInventoryItemDTO> InventoryItems { get; set; }
        public List<SyncFromDesktopCommandInputAccountDTO> Accounts { get; set; }
    }
}