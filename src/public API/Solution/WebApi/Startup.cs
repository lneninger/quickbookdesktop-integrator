using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainDatabaseMapping;
using RiverdaleMainApp2_0.IoC;
using Framework.Autofac;
using Framework.Storage.FileStorage.TemporaryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using ApplicationLogic.SignalR;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Identity;
using DomainModel.Identity;
using RiverdaleMainApp2_0.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RiverdaleMainApp2_0.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using ElmahCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Framework.Web.Security;
using Framework.EF.DbContextImpl;
using Framework.Logging.Log4Net;
using RiverdaleMainApp2_0.AppSettings;
using RiverdaleMainApp2_0.ErrorHandling;
//using Microsoft.AspNet.SignalR;

namespace RiverdaleMainApp2_0
{
    /// <summary>
    /// Application Setup class
    /// </summary>
    public class Startup
    {
        static LoggerCustom Logger = Framework.Logging.Log4Net.LoggerFactory.Create(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            this.ConnectionString = Configuration.GetConnectionString("RiverdaleModel");
            Logger.Info($"Main Database Connection - RiverdaleModel: {this.ConnectionString}");

        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Auth Secret Key
        /// </summary>
        public const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        /// <summary>
        /// Global Connection String
        /// </summary>
        public string ConnectionString { get; }

        // This method gets called by the runtime. Use this method to add services to the container.        
        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //var logger = Framework.Logging.Log4Net.LoggerFactory.Create(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            Logger.Info("Application startup - Initializing the Infra Services Configuration");
            try
            {

                //services.Configure<CookiePolicyOptions>(options =>
                //{
                //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                //    options.CheckConsentNeeded = context => true;
                //    options.MinimumSameSitePolicy = SameSiteMode.None;
                //});

                var customSettings = this.Configuration.GetSection("CustomSettings").Get<CustomSettings>();
                Logger.Info($"Application startup - Allowed origins: {string.Join(",", customSettings.AllowedOrigins)}");

                services.AddCors();

                //services.AddCors(options => {
                //    options.AddPolicy("Development", builder => builder
                //    .WithOrigins(customSettings.AllowedOrigins)
                //    .AllowAnyHeader()
                //    .AllowAnyMethod()
                //    .AllowCredentials());
                //});

                this.ConfigureAuthenticationServices(services);


                services.AddElmah(options =>
                {
                    //services.AddElmah(options => option.Path = "you_path_here")
                    //options.CheckPermissionAction = context => context.User.Identity.IsAuthenticated;
                });

                services.AddSignalR(config => {
                    config.EnableDetailedErrors = true;
                });

                services.AddDbContext<IdentityDBContext>(options => options.UseSqlServer(this.ConnectionString));
                services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());


                services.AddSingleton<IAuthorizationHandler, PolicyPermissionRequiredHandler>();

                var autofacServiceProvider = IoCConfig.Init(Configuration, services);

                return autofacServiceProvider;
            }
            catch (Exception ex)
            {
                Logger.Fatal("Application startup - Execption on Infra Services Configuration", ex);
                throw;
            }
            finally
            {
                Logger.Info("Application startup - Ending the Infra Services Configuration");
                Logger.FlushBuffers();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.        
        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            try
            {
                Logger.Info("Application startup - Initializing the Infra Application Configuration");

                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }

                app.ConfigureExceptionHandler();

                app.UseElmah();

                // Shows UseCors with CorsPolicyBuilder.
                //app.UseCors("Development");
                app.UseCors(builder => builder
                   .AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials()
                );

                app.UseSignalR(routes =>
                {
                    routes.MapHub<GlobalHub>("/globalhub", configureOptions => {
                    });
                });

                //app.Use(async (context, next) =>
                //{
                //    var hubContext = context.RequestServices
                //                            .GetRequiredService<IHubContext<GlobalHub>>();
                //});

                app.UseTempFileMiddleware();

                app.UseHttpsRedirection();
                app.UseStaticFiles();

                app.UseAuthentication();
                app.UseMvc();


                var securitySeedAsync = this.SecuritySeed().ConfigureAwait(false);
                securitySeedAsync.GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Logger.Fatal("Application startup - Execption on Infra Application Configuration", ex);
            }
            finally
            {
                Logger.Info("Application startup - Ending the Infra Application Configuration");
                Logger.FlushBuffers();
            }
        }


        private void ConfigureAuthenticationServices(IServiceCollection services)
        {
            try
            {
                Logger.Info("Application startup - Initializing the Infra Authentication/Authorization Configuration");

                services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();
                services.AddSingleton<IJwtFactory, JwtFactory>();

                // jwt wire up
                // Get options from app settings
                var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

                // Configure JwtIssuerOptions
                services.Configure<JwtIssuerOptions>(options =>
                {
                    options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                    options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                    options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
                });

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                    ValidateAudience = true,
                    ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = _signingKey,

                    RequireExpirationTime = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
                services.AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, configureOptions =>
                {
                    configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                    configureOptions.TokenValidationParameters = tokenValidationParameters;
                    configureOptions.RequireHttpsMetadata = false;
                    configureOptions.SaveToken = true;
                    configureOptions.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            // Add the access_token as a claim, as we may actually need it
                            var accessToken = context.SecurityToken as JwtSecurityToken;
                            if (accessToken != null)
                            {
                                ClaimsIdentity identity = context.Principal.Identity as ClaimsIdentity;
                                if (identity != null)
                                {
                                    identity.AddClaim(new Claim("access_token", accessToken.RawData));
                                }

                                // Set Current User
                                var idClaim = identity.FindFirst("id");
                                if (idClaim != null)
                                {
                                    // get claim containing user id
                                    var id = idClaim.Value;
                                    using (var currentUserService = IoCGlobal.Resolve<ICurrentUserService>())
                                    {
                                        currentUserService.CurrentUserId = id;
                                    }
                                }
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

                // api user claim policy
                services.AddAuthorization(options =>
                {
                    options.AddPolicy("ApiUser", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess));
                    options.AddPolicy(nameof(Constants.Strings.JwtClaims.Administrator), policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.Administrator));

                    var permissionNames = Enum.GetNames(typeof(PermissionsEnum.Enum));
                    PolicyPermissionRequired.BuildPolicies(options, permissionNames);

                });

                // add identity
                var builder = services.AddIdentityCore<AppUser>
                    (o =>
                    {
                        // configure identity options
                        o.Password.RequireDigit = false;
                        o.Password.RequireLowercase = false;
                        o.Password.RequireUppercase = false;
                        o.Password.RequireNonAlphanumeric = false;
                        o.Password.RequiredLength = 6;
                    });

                builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);

                builder.AddRoleValidator<RoleValidator<IdentityRole>>();
                builder.AddRoleManager<RoleManager<IdentityRole>>();
                builder.AddSignInManager<SignInManager<AppUser>>();

                builder.AddEntityFrameworkStores<IdentityDBContext>()
                .AddDefaultTokenProviders();
            }
            catch (Exception ex)
            {
                Logger.Fatal("Application startup - Execption on Infra Authentication/Authorization Configuration", ex);
            }
            finally
            {
                Logger.Info("Application startup - Ending the Infra Authentication/Authorization Configuration");
                Logger.FlushBuffers();
            }
        }



        private async Task SecuritySeed()
        {
            try
            {
                Logger.Info("Application startup - Initializing the Authentication Seeding for Administration user");

                var adminUserName = "admin";
                var adminFirstName = "Administrator";
                var adminLastName = "Administrator";
                var adminUserEmail = "admin@riverdale.com";
                var adminUserPassword = "r1v3rdal3";

                var adminRoleName = "Administrator";



                using (var userManager = IoCGlobal.Resolve<UserManager<AppUser>>())
                {
                    using (var roleManager = IoCGlobal.Resolve<RoleManager<IdentityRole>>())
                    {

                        var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                        if (adminUser == null)
                        {
                            var newAdminUser = new AppUser
                            {
                                UserName = adminUserName,
                                FirstName = adminFirstName,
                                LastName = adminLastName,
                                Email = adminUserEmail,
                            };
                            await userManager.CreateAsync(newAdminUser, adminUserPassword);
                            adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                        }

                        var adminRole = await roleManager.FindByNameAsync(adminRoleName);
                        if (adminRole == null)
                        {
                            var newAdminRole = new IdentityRole
                            {
                                Name = adminRoleName,
                            };

                            await roleManager.CreateAsync(newAdminRole);
                            adminRole = await roleManager.FindByNameAsync(adminRoleName);
                        }

                        await userManager.AddToRoleAsync(adminUser, adminRoleName);
                        var roleClaims = (await roleManager.GetClaimsAsync(adminRole)).Select(o => o.Value);
                        var claimNames = Enum.GetNames(typeof(PermissionsEnum.Enum)).Except(roleClaims);
                        foreach (var permission in claimNames)
                        {
                            await roleManager.AddClaimAsync(adminRole, new Claim(RiverdaleMainApp2_0.Auth.Constants.Strings.JwtClaimIdentifiers.Permissions, permission));
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                Logger.Fatal("Application startup - Execption on Authentication Seeding for Administration user", ex);
            }
            finally
            {
                Logger.Info("Application startup - Ending the Authentication Seeding for Administration user");
                Logger.FlushBuffers();
            }
        }
    }
}
