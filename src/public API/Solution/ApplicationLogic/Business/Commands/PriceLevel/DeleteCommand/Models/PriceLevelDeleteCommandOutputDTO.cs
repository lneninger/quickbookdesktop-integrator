using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.PriceLevel.DeleteCommand.Models
{
    public class PriceLevelDeleteCommandOutputDTO
    {

        public PriceLevelDeleteCommandOutputDTO()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}