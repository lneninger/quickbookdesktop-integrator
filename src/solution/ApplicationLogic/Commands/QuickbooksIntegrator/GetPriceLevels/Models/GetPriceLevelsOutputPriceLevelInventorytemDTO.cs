using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Commands.QuickbooksIntegrator.GetPriceLevels.Models
{
    public class GetPriceLevelsOutputPriceLevelInventorytemDTO
    {
        public string ItemId { get; set; }
        public string ItemFullName { get; set; }
        public short? Type { get; set; }
        public double? CustomPrice { get; set; }
        public double? CustomPricePercent { get; set; }
    }
}
