using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLogic.Business.Commands.InventoryItem.GetByIdCommand.Models
{
    public class InventoryItemGetByIdCommandOutputRelatedProductItemDTO
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int RelatedProductId { get; set; }

        public int Stems { get; set; }

        public string RelatedProductName { get; set; }

        public string RelatedProductTypeName { get; set; }

        public string RelatedProductTypeDescription { get; set; }

        public int RelatedProductPictureId { get; set; }
    }
}
