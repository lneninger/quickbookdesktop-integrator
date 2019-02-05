using ApplicationLogic.Business.Commons.DTOs;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.IncomeAccount.GetByIdCommand.Models
{
    public class IncomeAccountGetByIdCommandOutputDTO
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public string ProductColorTypeId { get; set; }

        public IEnumerable<FileItemRefOutputDTO> Medias { get; set; }
        public string ProductTypeId { get; set; }
        
    }
}