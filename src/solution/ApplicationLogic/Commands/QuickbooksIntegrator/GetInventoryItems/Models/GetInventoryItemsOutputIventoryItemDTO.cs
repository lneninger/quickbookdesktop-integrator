﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Commands.QuickbooksIntegrator.GetInventoryItems.Models
{
    public class GetInventoryItemsOutputIventoryItemDTO
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public double? Stock { get; set; }
        public double? Cost { get; set; }
        public string SaleDescription { get; set; }
        public double? SalePrice { get; set; }
        public string IncomeAccountId { get; set; }
        public string AssetAccountId { get; set; }
    }
}
