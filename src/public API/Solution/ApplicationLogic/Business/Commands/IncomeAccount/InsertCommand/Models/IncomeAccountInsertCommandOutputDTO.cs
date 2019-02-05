using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.IncomeAccount.InsertCommand.Models
{
    public class IncomeAccountInsertCommandOutputDTO
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public string ERPId { get; set; }
    }
}