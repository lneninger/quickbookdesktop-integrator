using ApplicationLogic.Business.Commons.DTOs;
using System;

namespace ApplicationLogic.Business.Commands.InventoryAccount.PageQueryCommand.Models
{
    public class InventoryAccountPageQueryCommandOutputDTO
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}