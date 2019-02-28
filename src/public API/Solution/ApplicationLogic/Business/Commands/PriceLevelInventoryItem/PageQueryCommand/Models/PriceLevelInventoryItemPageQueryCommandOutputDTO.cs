using ApplicationLogic.Business.Commons.DTOs;
using System;

namespace ApplicationLogic.Business.Commands.PriceLevelInventoryItem.PageQueryCommand.Models
{
    public class PriceLevelInventoryItemPageQueryCommandOutputDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ERPId { get; set; }
        public string SalesforceId { get; set; }
        public DateTime? CreatedAt { get; set; }
       
        public FileItemRefOutputDTO MainPicture { get; set; }

        public string ColorName { get; set; }
    }
}