using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.Product.InsertCommand.Models
{
    public class ProductInsertCommandOutputDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ERPId { get; set; }
    }
}