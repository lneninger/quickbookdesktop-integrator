using ApplicationLogic.Business.Commons.DTOs;
using System;

namespace ApplicationLogic.Business.Commands.IncomeAccount.PageQueryCommand.Models
{
    public class IncomeAccountPageQueryCommandOutputDTO
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}