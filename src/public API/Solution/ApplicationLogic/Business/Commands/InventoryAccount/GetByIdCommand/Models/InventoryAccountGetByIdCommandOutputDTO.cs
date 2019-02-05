using ApplicationLogic.Business.Commons.DTOs;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.InventoryAccount.GetByIdCommand.Models
{
    public class InventoryAccountGetByIdCommandOutputDTO
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
    }
}