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
        private const string QuickbooksApplicationIdKey = "QBAppID";
        private const string QuickbooksApplicationNameKey = "QBAppName";

        public AppConfig()
        {
            if (!ConfigurationManager.AppSettings.AllKeys.Contains(QuickbooksApplicationIdKey))
            {
                throw new ConfigurationErrorsException($"Application configuration key doesn't exists: {QuickbooksApplicationIdKey}. Expected keys: {QuickbooksApplicationIdKey}: Quickbooks client application id, {QuickbooksApplicationNameKey}: Quickbooks client application name");
            }

            if (!ConfigurationManager.AppSettings.AllKeys.Contains(QuickbooksApplicationNameKey))
            {
                throw new ConfigurationErrorsException($"Application configuration key doesn't exists: {QuickbooksApplicationNameKey}");
            }

            this.QuickbooksApplicationID = ConfigurationManager.AppSettings[QuickbooksApplicationIdKey];
            this.QuickbooksApplicationName = ConfigurationManager.AppSettings[QuickbooksApplicationNameKey];
        }

        public string QuickbooksApplicationID { get; }
        public string QuickbooksApplicationName { get; }
    }
}
