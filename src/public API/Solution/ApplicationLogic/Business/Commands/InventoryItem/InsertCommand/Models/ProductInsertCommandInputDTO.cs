using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.Product.InsertCommand.Models
{
    public class ProductInsertCommandInputDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductTypeId { get; set; }
    }
}