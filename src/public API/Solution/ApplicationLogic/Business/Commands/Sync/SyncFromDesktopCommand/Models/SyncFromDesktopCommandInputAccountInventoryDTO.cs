using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.Sync.SyncFromDesktopCommand.Models
{
    public class SyncFromDesktopCommandInputAccountInventoryDTO
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public bool? IsActive { get; set; }

    }
}