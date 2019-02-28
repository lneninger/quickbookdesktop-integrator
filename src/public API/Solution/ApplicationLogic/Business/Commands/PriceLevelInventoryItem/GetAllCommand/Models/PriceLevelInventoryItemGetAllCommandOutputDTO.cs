using ApplicationLogic.Business.Commons.DTOs;
using System;

namespace ApplicationLogic.Business.Commands.PriceLevelInventoryItem.GetAllCommand.Models
{
    public class PriceLevelInventoryItemGetAllCommandOutputDTO
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public decimal? PriceLevelInventoryItemPercentage { get; set; }
        public string PriceLevelInventoryItemType { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        
    }
}