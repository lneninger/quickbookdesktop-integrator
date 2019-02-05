using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.InventoryAccount.InsertCommand.Models
{
    public class InventoryAccountInsertCommandInputDTO
    {
        public string FullName { get; set; }
        public string Name { get; set; }
        public double? Stock { get; set; }
        public double? Cost { get; set; }
        public string SaleDescription { get; set; }
        public double? SalePrice { get; set; }
    }
}