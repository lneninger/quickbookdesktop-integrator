using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.AppConfiguration
{
    public class AppConfig
    {
        public AppConfig()
        {
            this.UserName = ConfigurationManager.AppSettings["userName"];
            this.Password = ConfigurationManager.AppSettings["password"];
        }

        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
