using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.Product.DeleteCommand.Models
{
    public class ProductDeleteCommandOutputDTO
    {

        public ProductDeleteCommandOutputDTO()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}