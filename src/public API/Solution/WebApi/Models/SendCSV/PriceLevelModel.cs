using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickbooksIntegratorAPI.Models.SendCSV
{
    public class PriceLevelModel
    {
        public int branchID { get; set; }
        public int InPriceLevelId { get; set; }
        public decimal PriceLevel { get; set; }
    }
}
