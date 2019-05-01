using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.IntegrationProcess.InsertCommand.Models
{
    public class IntegrationProcessInsertCommandOutputDTO
    {
        public int Id { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}