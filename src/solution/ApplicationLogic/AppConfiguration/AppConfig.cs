﻿using System;
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
        private const string APIBaseURLKey = "APIBaseURL";
        private const string QuartzSchedulerKey = "QuartzScheduler";

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

            if (!ConfigurationManager.AppSettings.AllKeys.Contains(QuartzSchedulerKey))
            {
                throw new ConfigurationErrorsException($"Application configuration key doesn't exists: {QuartzSchedulerKey}");
            }

            this.QuickbooksApplicationID = ConfigurationManager.AppSettings[QuickbooksApplicationIdKey];
            this.QuickbooksApplicationName = ConfigurationManager.AppSettings[QuickbooksApplicationNameKey];
            this.APIBaseURL = ConfigurationManager.AppSettings[APIBaseURLKey];
            this.QuartzScheduler = ConfigurationManager.AppSettings[QuartzSchedulerKey];
        }

        public string QuickbooksApplicationID { get; }
        public string QuickbooksApplicationName { get; }
        public string APIBaseURL { get; }
        public string QuartzScheduler { get; }
    }
}
