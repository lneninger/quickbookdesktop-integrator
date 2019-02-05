using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.InventoryAccount.UpdateCommand.Models
{
    public class InventoryAccountUpdateCommandInputDTO
    {
        public int Id { get; set; }

        public string ExternalId { get; set; }

        public string Name { get; set; }
    }
}