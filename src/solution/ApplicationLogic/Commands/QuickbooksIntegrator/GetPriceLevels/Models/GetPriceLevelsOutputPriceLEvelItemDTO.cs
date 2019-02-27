using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Commands.QuickbooksIntegrator.GetPriceLevels.Models
{
    public class GetPriceLevelsOutputPriceLevelItemDTO
    {
        public GetPriceLevelsOutputPriceLevelItemDTO()
        {
            this.InventoryItems = new List<GetPriceLevelsOutputPriceLevelInventorytemDTO>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// FixedPercentage,
        /// PerItem
        /// </summary>
        public string PriceLevelType { get; set; }
        public bool IsActive { get; set; }
        public double? PriceLevelPercentage { get; set; }
        public List<GetPriceLevelsOutputPriceLevelInventorytemDTO> InventoryItems { get; set; }
    }
}
