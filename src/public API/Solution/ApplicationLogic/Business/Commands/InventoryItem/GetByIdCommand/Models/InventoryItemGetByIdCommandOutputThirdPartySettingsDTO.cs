using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLogic.Business.Commands.InventoryItem.GetByIdCommand.Models
{
    public class InventoryItemGetByIdCommandOutputThirdPartySettingsDTO
    {
        public int Id { get; set; }
        public string ThirdPartyAppTypeId { get; set; }
        public string ThirdPartyProductId { get; set; }
    }
}
