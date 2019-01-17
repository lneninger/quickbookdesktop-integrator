using Framework.Storage.FileStorage.TemporaryStorage;
using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.Product.UpdateCommand.Models
{
    public class ProductUpdateCommandInputDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ProductColorTypeId { get; set; }
    }
}