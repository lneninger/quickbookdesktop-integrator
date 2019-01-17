using Framework.Autofac;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace Framework.Commons
{
    public class AppConfig : BaseIoCDisposable
    {

        static AppConfig instance = null;
        public static AppConfig Instance
        {
            get
            {
                if (instance == null)
                {
                    AppConfig.instance = IoCGlobal.Resolve<AppConfig>();
                }

                return AppConfig.instance;

            }
        }

        public AppConfig(IConfiguration configuration)
        {
            this.Configuration = configuration;

            this.FileStorageSettings = Configuration.GetSection("fileStorage").Get<FileStorageSettings>();
        }

        public IConfiguration Configuration { get; }
        public FileStorageSettings FileStorageSettings { get; private set; }


        //public string TemporaryFileFolder { get => this.FileStorageSettings.TemporaryFolderPath; }
    }
}
