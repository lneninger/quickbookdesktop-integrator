using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickbooksIntegratorAPI.Models.SendCSV
{
    public class InventoryItemModel
    {
        public int BranchID { get; set; }
        public string InItemId { get; set; }
        public string ItemId { get; set; }
        public string ExCategoryId { get; set; }
        public decimal? Price1 { get; set; }
        public decimal? QtyStock { get; set; }
    }
}
