using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.InventoryAccount.DeleteCommand.Models
{
    public class InventoryAccountDeleteCommandOutputDTO
    {

        public InventoryAccountDeleteCommandOutputDTO()
        {
        }

        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
    }
}