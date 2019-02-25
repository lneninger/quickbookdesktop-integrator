using ApplicationLogic.Business.Commons.DTOs;
using System;

namespace ApplicationLogic.Business.Commands.PriceLevel.GetAllCommand.Models
{
    public class PriceLevelGetAllCommandOutputDTO
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public decimal? PriceLevelPercentage { get; set; }
        public string PriceLevelType { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        
    }
}