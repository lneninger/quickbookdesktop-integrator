using System;

namespace ApplicationLogic.Business.Commands.InventoryAccount.UpdateCommand.Models
{
    public class InventoryAccountUpdateCommandOutputDTO
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
    }
}