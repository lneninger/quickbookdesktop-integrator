﻿using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.IncomeAccount.DeleteCommand.Models
{
    public class IncomeAccountDeleteCommandOutputDTO
    {

        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
    }
}