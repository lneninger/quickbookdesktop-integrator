using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLogic.Business.Commands.AppUserRole.GetByIdCommand.Models
{
    public class AppUserRoleGetByIdCommandOutputFreightoutDTO
    {
        public int Id { get; set; }

        public decimal Cost { get; set; }

        public decimal WProtect { get; set; }

        public decimal SecondLeg { get; set; }

        public decimal? SurchargeHourly { get; set; }

        public decimal? SurchargeYearly { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }
    }
}
