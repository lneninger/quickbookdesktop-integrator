using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.IncomeAccount.UpdateCommand.Models
{
    public class IncomeAccountUpdateCommandInputDTO
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ProductColorTypeId { get; set; }
    }
}