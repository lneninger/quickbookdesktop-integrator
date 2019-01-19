using ApplicationLogic.AppSettings;
using ApplicationLogic.Business.Commands.InventoryItem.GetAllCommand;
using ApplicationLogic.Business.Commands.InventoryItem.InsertCommand;
using ApplicationLogic.SignalR;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Autofac.Features.AttributeFilters;
using DatabaseRepositories.DB;
using DomainDatabaseMapping;
using EntityFrameworkCore.DbContextScope;
using Framework.Autofac;
using Framework.Commons;
using Framework.Core.ReflectionHelpers;
using Framework.EF.DbContextImpl;
using Framework.Storage.FileStorage.interfaces;
using Framework.Storage.FileStorage.StorageImplementations;
using Framework.Storage.FileStorage.TemporaryStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuickbooksIntegratorAPI.AppSettings;
using QuickbooksIntegratorAPI.Auth;
using System;
using System.Linq;
using System.Reflection;
//using Microsoft.AspNet.SignalR;
//using Autofac.Integration.SignalR;
//using Microsoft.AspNet.SignalR.Infrastructure;

namespace QuickbooksIntegratorAPI.IoC
{
    /// <summary>
    /// IoC Containner configuration
    /// </summary>
    public class IoCConfig
    {
        /// <summary>
        /// Initializes the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static AutofacServiceProvider Init(Microsoft.Extensions.Configuration.IConfiguration configuration, IServiceCollection services)
        {
            var container = IoCGlobal.Config(builder =>
            {
                builder.Populate(services);

                builder
                .RegisterInstance(configuration.GetSection("CustomSettings").Get<CustomSettings>())
                .As<CustomSettings>();

                builder
                .RegisterInstance(configuration.GetSection("FunzaSettings").Get<FunzaSettings>())
                .As<FunzaSettings>();

                // File Mechanism
                builder
                .RegisterInstance<FileStorageSettings>(configuration.GetSection("fileStorage").Get<FileStorageSettings>());

                builder.RegisterType<TemporaryStorage>().AsSelf();

                builder.RegisterSignalRHubs(typeof(Startup).GetTypeInfo().Assembly, typeof(GlobalHub).GetTypeInfo().Assembly);

                //AOP Interceptors
                // ExecutionTraceInterceptor. Trace all methods executions
                builder.RegisterType<ExecutionTraceInterceptor>();

                builder.RegisterType<AppConfig>().AsSelf().WithParameter(new TypedParameter(typeof(IConfiguration), configuration))
                .SingleInstance();

                builder.RegisterType<DbContextScopeFactory>().As<IDbContextScopeFactory>()
                .AsImplementedInterfaces();

                builder.RegisterType<AmbientDbContextLocator>().As<IAmbientDbContextLocator>()
                .AsImplementedInterfaces();


                builder.RegisterType<IdentityDBContext>().AsSelf()
                .TrackInstanceEvents();

                builder.RegisterType<ApplicationDBContext>().AsSelf()
                .TrackInstanceEvents();

                builder.RegisterType<CurrentUserService>().As<ICurrentUserService>()
                .InstancePerLifetimeScope()
                //.InstancePerMatchingLifetimeScope("CurrentUserService")
                .TrackInstanceEvents();

                // SignalR Context
                //builder.Register(ctx => ctx.GetHubContext<GlobalHub>());
                //builder.RegisterType<Autofac.Integration.SignalR.AutofacDependencyResolver>()
                //    .As<IDependencyResolver>()
                //    .SingleInstance();
                //builder.Register((context, p) =>
                //        context.Resolve<IDependencyResolver>()
                //            .Resolve<Microsoft.AspNet.SignalR.Infrastructure.IConnectionManager>()
                //            .GetConnectionContext<SignalRConnection>());

    //            builder.Register(ctx =>
    //ctx.Resolve<IDependencyResolver>()
    //   .Resolve<IConnectionManager>()
    //   .GetHubContext())
    //   .Named<IHubContext>("EventHub");

                var targetAssembly = Assembly.GetExecutingAssembly();

                var controllerTypes = targetAssembly.GetTypes().Where(type => type.IsClass && type.Name.EndsWith("Controller", StringComparison.InvariantCultureIgnoreCase));
                builder.RegisterTypes(controllerTypes.ToArray())
               .AsSelf()
               .EnableInterfaceInterceptors()
               .InterceptedBy(typeof(ExceptionInterceptor));

                var serviceAssembly = typeof(InventoryItemGetAllCommand).Assembly;
                var serviceTypes = serviceAssembly.GetTypes().Where(type => type.IsClass && type.Name.EndsWith("Command", StringComparison.InvariantCultureIgnoreCase));
                builder.RegisterTypes(serviceTypes.ToArray())
                .AsImplementedInterfaces()
                .TrackInstanceEvents();


                var validatorAssembly = typeof(InventoryItemInsertValidator).Assembly;
                var validatorTypes = validatorAssembly.GetTypes().Where(type => type.IsClass && type.Name.EndsWith("Validator", StringComparison.InvariantCultureIgnoreCase));
                builder.RegisterTypes(validatorTypes.ToArray())
                .AsImplementedInterfaces()
                .TrackInstanceEvents();

                var repositoryAssembly = typeof(InventoryItemDBRepository).Assembly;
                var repositoryTypes = repositoryAssembly.GetTypes().Where(type => type.IsClass && type.Name.EndsWith("Repository", StringComparison.InvariantCultureIgnoreCase));
                builder.RegisterTypes(repositoryTypes.ToArray())
                .AsImplementedInterfaces()
                .TrackInstanceEvents();

                // File Storage implementations injections
                var FileSystemStorageNamespace = typeof(FileSystemStorage).Namespace;
                var storageTypes = typeof(FileSystemStorage).Assembly.GetTypes().Where(o => o.Name.EndsWith("Storage") && o.Namespace.Equals(FileSystemStorageNamespace) && o.IsClass);
                foreach (var storageType in storageTypes)
                {
                    var fileSourceEnum = storageType.GetStaticPropertyValue(nameof(FileSystemStorage.Identifier));

                    builder.RegisterType(storageType).Keyed<IFileStorageService>(fileSourceEnum)
                       .AsImplementedInterfaces()
                       .WithAttributeFiltering()
                        .TrackInstanceEvents();
                }

                // Authentication
                builder
                .RegisterType<JwtFactory>()
                .As<IJwtFactory>()
                .TrackInstanceEvents();

                //var firebaseAssembly = typeof(BaseRepository).Assembly;
                //var firebaseRepositoryTypes = firebaseAssembly.GetTypes().Where(type => type.IsClass && type.Name.EndsWith("Repository", StringComparison.InvariantCultureIgnoreCase));
                //builder.RegisterTypes(firebaseRepositoryTypes.ToArray())
                //.AsImplementedInterfaces()
                //.TrackInstanceEvents();
            });

            // SignalR access to DI container
            //GlobalHost.DependencyResolver = new AutofacDependencyResolver(container);

            //SignalR OWIN configuration
            //var signalRConfiguration = new HubConfiguration();
            //signalRConfiguration.Resolver = new AutofacDependencyResolver(container);

            return new AutofacServiceProvider(container);
        }
    }
}
