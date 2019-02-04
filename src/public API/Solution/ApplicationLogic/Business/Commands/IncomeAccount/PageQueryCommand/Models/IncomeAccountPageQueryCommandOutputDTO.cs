using ApplicationLogic.Business.Commons.DTOs;
using System;

namespace ApplicationLogic.Business.Commands.IncomeAccount.PageQueryCommand.Models
{
    public class IncomeAccountPageQueryCommandOutputDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ERPId { get; set; }
        public string SalesforceId { get; set; }
        public DateTime? CreatedAt { get; set; }
       
        public FileItemRefOutputDTO MainPicture { get; set; }

        public string ColorName { get; set; }
    }
}