using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.PriceLevel.InsertCommand.Models
{
    public class PriceLevelInsertCommandOutputDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ERPId { get; set; }
    }
}