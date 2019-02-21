using ApplicationLogic.Business.Commons.DTOs;
using System;

namespace ApplicationLogic.Business.Commands.InventoryItem.GetAllCommand.Models
{
    public class InventoryItemGetAllCommandOutputDTO
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public string InventoryAccountExternalId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public decimal? Price { get; set; }
        public decimal? Stock { get; set; }
        public string SaleDescription { get; set; }
    }
}