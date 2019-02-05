using System;

namespace ApplicationLogic.Business.Commands.IncomeAccount.UpdateCommand.Models
{
    public class IncomeAccountUpdateCommandOutputDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public bool Active { get; set; }
    }
}