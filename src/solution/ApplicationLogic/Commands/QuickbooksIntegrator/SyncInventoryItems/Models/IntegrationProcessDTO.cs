using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Commands.QuickbooksIntegrator.SyncInventoryItems.Models
{
    public class IntegrationProcessDTO
    {
        public int Id { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
