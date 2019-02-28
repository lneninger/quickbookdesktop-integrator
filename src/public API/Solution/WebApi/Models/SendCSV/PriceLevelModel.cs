using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickbooksIntegratorAPI.Models.SendCSV
{
    public class PriceLevelModel
    {
        public int branchID { get; set; }
        public string InPriceLevelId { get; set; }
        public string PriceLevel { get; set; }
    }
}
