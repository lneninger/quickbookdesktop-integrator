using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.PriceLevel.UpdateCommand.Models
{
    public class PriceLevelUpdateCommandInputDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ProductColorTypeId { get; set; }
    }
}