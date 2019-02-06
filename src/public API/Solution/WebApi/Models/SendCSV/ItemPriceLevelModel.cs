using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickbooksIntegratorAPI.Models.SendCSV
{
    public class ItemPriceLevelModel
    {
        public int branchID { get; set; }
        public int ExPriceLeverlId { get; set; }
        public string ExItemId { get; set; }
        public decimal? Price { get; set; }
    }
}
