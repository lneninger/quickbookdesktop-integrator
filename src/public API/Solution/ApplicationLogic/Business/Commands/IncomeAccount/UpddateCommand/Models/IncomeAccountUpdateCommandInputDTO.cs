using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.IncomeAccount.UpdateCommand.Models
{
    public class IncomeAccountUpdateCommandInputDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }
    }
}