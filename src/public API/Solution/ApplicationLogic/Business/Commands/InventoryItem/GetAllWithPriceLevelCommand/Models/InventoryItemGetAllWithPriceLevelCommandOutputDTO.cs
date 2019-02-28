using ApplicationLogic.Business.Commons.DTOs;
using System;

namespace ApplicationLogic.Business.Commands.InventoryItem.GetAllWithPriceLevelCommand.Models
{
    public class InventoryItemGetAllWithPriceLevelCommandOutputDTO
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public decimal? Price { get; set; }
        public decimal? Stock { get; set; }
        public string SaleDescription { get; set; }
        public int? AssetAccountId { get; set; }
        public string PriceLevelExternalId { get; set; }
        public decimal? PriceLevelCustomPrice { get; set; }
    }
}