using ApplicationLogic.Business.Commons.DTOs;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.PriceLevel.GetByIdCommand.Models
{
    public class PriceLevelGetByIdCommandOutputDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductColorTypeId { get; set; }

        public IEnumerable<FileItemRefOutputDTO> Medias { get; set; }
        public string ProductTypeId { get; set; }
        
    }
}