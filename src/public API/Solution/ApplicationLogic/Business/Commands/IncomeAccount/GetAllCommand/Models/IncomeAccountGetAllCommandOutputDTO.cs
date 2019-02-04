using ApplicationLogic.Business.Commons.DTOs;
using System;

namespace ApplicationLogic.Business.Commands.IncomeAccount.GetAllCommand.Models
{
    public class IncomeAccountGetAllCommandOutputDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}