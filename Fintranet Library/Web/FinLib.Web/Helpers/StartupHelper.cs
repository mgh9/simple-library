using FinLib.Common.Extensions;
using FinLib.Common.Helpers.Json;
using FinLib.DataLayer.Context;
using FinLib.DomainClasses.SEC;
using FinLib.Models.Configs;
using FinLib.Models.Constants.Database;
using FinLib.Models.Enums;
using FinLib.Providers.Configuration;
using FinLib.Providers.Database;
using FinLib.Providers.Identity;
using FinLib.Providers.Identity.Stores;
using FinLib.Providers.Logging;
using FinLib.Services.Base;
using FinLib.Web.Api.Helpers;
using FinLib.Web.Extensions;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using static FinLib.Web.Shared.Models.Constants.Security;

namespace FinLib.Web.Helpers
{
    internal static class StartupHelper
    {
        /// <summary>
        /// injecting appSettings provider
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        internal static void AddAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GlobalSettings>(configuration.GetSection("GlobalSettings"));
            services.AddSingleton<IAppSettingsProvider<GlobalSettings>, AppSettingsProvider<GlobalSettings>>();

            // needed to load configuration from appsettings.json
            services.AddOptions();

            // needed to store rate limit counters and ip rules
            services.AddMemoryCache();
        }

        /// <summary>
        /// Injecting common dependancies
        /// </summary>
        /// <param name="services"></param>
        internal static void AddCommonDependencies(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
        }

        /// <summary>
        /// injecting cookie policies
        /// </summary>
        /// <param name="services"></param>
        internal static void AddCookiePolicies(this IServiceCollection services, IWebHostEnvironment environment)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.Secure = CookieSecurePolicy.Always;

                // age Strict bezarim~> baraye login beine crossSites (a.domain.com , b.domain.com), moshkel mikhorim
                // google: IdentityService4 sameSite cookie
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Lax;

                options.OnAppendCookie = cookieContext => checkSameSite(cookieContext.Context, cookieContext.CookieOptions);
                options.OnDeleteCookie = cookieContext => checkSameSite(cookieContext.Context, cookieContext.CookieOptions);
            });

            // one cookie for ASP.NET Core MVC (Login, Registration, ...), one cookie for angularjs---WebApi
            // this is the first one. (the second one configured in app.UserAntiforgeryForAngularjs)
            services.AddAntiforgery(options =>
            {
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Strict; // SOC
                options.Cookie.Name = CookieNames.Antiforgery;
                options.HeaderName = "X-XSRF-TOKEN";
            });

            // Identity Cookie (Authentication cookie)
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = CookieNames.ApplicationAuthentication;
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.SlidingExpiration = false;

                // TODO: check the stack and git for the issue
                options.Cookie.SameSite = SameSiteMode.Lax;  // SOC // issue here, should be set to Lax

                // without expire time ~> session cookie. destroy after closing the browser
                // options.ExpireTimeSpan = TimeSpan.FromSeconds(10000);
                // options.Cookie.Expiration = TimeSpan.FromSeconds(1000);
            });
        }

        internal static void AddIpRateLimitting(this IServiceCollection services, IConfiguration configuration)
        {
          // TODO: add ip-rate limitting
        }

        internal static void AddControllersAndViews(this IServiceCollection services)
        {
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); // no need globally. bcz of WEB API.
                // just seperate them in each BaseController with [antiforgeryattribute]
            })
                .AddRazorRuntimeCompilation()
                .AddNewtonsoftJson(options =>
                {
                    options.UseCamelCasing(true);

                    // Use the default property (Pascal) casing
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();

                    // Configure a custom converter
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.Converters.Add(new TimeSpanJsonConverter());
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.MaxDepth = 6;
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.Formatting = Formatting.None;
                    //options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                    options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
                });
        }

        /// <summary>
        /// Register our global Logger
        /// </summary>
        /// <param name="services"></param>
        internal static void AddLogger(this IServiceCollection services)
        {
            services.AddSingleton<IAppLogger, AppLogger>();
        }

        internal static void AddCompression(this IServiceCollection services)
        {
            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
            });
        }

        /// <summary>
        /// Register EntityFrameworkCore (DbContexts : Main, Auditing)
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="environment"></param>
        internal static void AddDbContext(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddTransient<IUnitOfWork, AppDbContext>();

            var mainDatabaseConnectionString = configuration.GetConnectionString(ConnectionStringNames.MainDatabase);
            var auditingDatabaseConnectionString = configuration.GetConnectionString(ConnectionStringNames.AuditsDatabase);

#if DEBUG
            // main database
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(mainDatabaseConnectionString);
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            }, ServiceLifetime.Transient);
#else
            // main database
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(mainDatabaseConnectionString);
                // disable DetailedErrors and disable SensitiveDataLogging in release (publish)
            }, ServiceLifetime.Transient);
#endif

            services.AddSingleton<Func<DatabaseName
                , IRawDatabaseProvider>>(provider => databaseName =>
            {
                switch (databaseName)
                {
                    case DatabaseName.Main:
                        return new RawDatabaseProvider(mainDatabaseConnectionString);

                    case DatabaseName.Auditing:
                        return new RawDatabaseProvider(auditingDatabaseConnectionString);

                    default:
                        throw new ArgumentOutOfRangeException(nameof(databaseName), databaseName, null);
                }
            });
        }

        /// <summary>
        /// Register AspNetIdentity Entities (Users/Roles/UserManager/SignInManager/UserStore/...)
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        internal static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            var appDbContext = services.BuildServiceProvider().GetRequiredService<IUnitOfWork>();
            var theGlobalSettings = getGlobalSettings(configuration);
            //var lockoutPolicyConfigs = appDbContext.Set<Config>().Where(x => x.Category == Lockout.CategoryKey).ToList();

            services.AddIdentity<User, Role>(config =>
            {
                config.User.RequireUniqueEmail = true;

                // TODO: add Identity Policies from the config (database or appsettings)

                config.Password.RequireDigit = theGlobalSettings.Identity.PasswordPolicy.RequireDigit;
                config.Password.RequiredLength = theGlobalSettings.Identity.PasswordPolicy.RequiredLength;
                config.Password.RequireNonAlphanumeric = theGlobalSettings.Identity.PasswordPolicy.RequireNonAlphanumeric;
                config.Password.RequireUppercase = theGlobalSettings.Identity.PasswordPolicy.RequireUppercase;
                config.Password.RequireLowercase = theGlobalSettings.Identity.PasswordPolicy.RequireLowercase;

                config.Lockout.AllowedForNewUsers = theGlobalSettings.Identity.Lockout.AllowedForNewUsers;
                config.Lockout.MaxFailedAccessAttempts = theGlobalSettings.Identity.Lockout.MaxFailedAccessAttempts;
                config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(theGlobalSettings.Identity.Lockout.LockoutTimeInMinutes);
            })
                .AddEntityFrameworkStores<AppDbContext>()
                //.AddErrorDescriber<AppErrorDescriber>()
                .AddUserManager<AppUserManager>()
                .AddSignInManager<AppSignInManager>()
                .AddUserStore<AppUserStore>()
                //.AddIdentityOptions()
                .AddDefaultTokenProviders();
        }

        internal static void AddMappers(this IServiceCollection services)
        {
            services.AddSingleton(x => Mappings.MapperHelper.Mapper);
        }

        /// <summary>
        /// Injecting shared/common services (contains LoggedInUser info and ...) and all the business services
        /// </summary>
        /// <param name="services"></param>
        internal static void AddCommonServices(this IServiceCollection services)
        {
            services.AddScoped<ICommonServicesProvider<FinLib.Models.Configs.GlobalSettings>>(factoryProvider =>
            {
                var serviceProvider = factoryProvider.GetRequiredService<IServiceProvider>();
                var currentHttpContextAccessor = factoryProvider.GetRequiredService<IHttpContextAccessor>();
                var theGlobalSettingsProvider = factoryProvider.GetRequiredService<IAppSettingsProvider<GlobalSettings>>();
                var appDbContext = factoryProvider.GetRequiredService<IUnitOfWork>();
                var distributedCacheProvider = factoryProvider.GetRequiredService<IDistributedCache>();
                var dataProtectionProvider = factoryProvider.GetRequiredService<IDataProtectionProvider>();
                var appLogger = factoryProvider.GetRequiredService<IAppLogger>();
                var currentContext = currentHttpContextAccessor.HttpContext;

                if (currentContext?.User?.Identity != null && currentContext.User.Identity.IsAuthenticated)
                {
                    var userId = currentContext.User.GetLoggedInUserId<int>();
                    var loggedInUserRoleId = UserManagerHelper.GetLoggedInUserRoleIdAsync(currentHttpContextAccessor, true).Result;

                    return new CommonServicesProvider<FinLib.Models.Configs.GlobalSettings>(serviceProvider: serviceProvider,
                                                                                            httpContextAccessor: currentHttpContextAccessor,
                                                                                            appLogger: appLogger,
                                                                                            dbContext: appDbContext,
                                                                                            dataProtectionProvider: dataProtectionProvider,
                                                                                            appSettingsProvider: theGlobalSettingsProvider,
                                                                                            distributedCacheProvider: distributedCacheProvider,
                                                                                            isAuthenticated: true,
                                                                                            loggedInUserId: userId,
                                                                                            loggedInUserRoleId: loggedInUserRoleId);
                }

                else
                {
                    return new CommonServicesProvider<FinLib.Models.Configs.GlobalSettings>(serviceProvider: serviceProvider,
                                                                                            httpContextAccessor: currentHttpContextAccessor,
                                                                                            appLogger: appLogger,
                                                                                            dbContext: appDbContext,
                                                                                            dataProtectionProvider: dataProtectionProvider,
                                                                                            appSettingsProvider: theGlobalSettingsProvider,
                                                                                            distributedCacheProvider: distributedCacheProvider,
                                                                                            isAuthenticated: false);
                }
            });

            // inject all the business services
            services.RegisterAllTypesOf<BaseService>(new[] { typeof(BaseService).Assembly });
        }

        internal static void AddDistributedCache(this IServiceCollection services, IConfiguration configuration)
        {
            var theGlobalSettings = getGlobalSettings(configuration);

            // TODO: add Redis here
        }


        #region Security Considerations

        /// <summary>
        /// adjusting some CSP headers
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        internal static void UseSecurityConsiderations(this IApplicationBuilder app)
        {
            app.UseHsts(hsts =>
            {
                hsts.MaxAge(365);
                hsts.IncludeSubdomains();
            });
            app.UseXfo(options => options.SameOrigin());
            app.UseXContentTypeOptions();
            app.UseReferrerPolicy(opts => opts.NoReferrer());
            app.UseXXssProtection(options => options.EnabledWithBlockMode());

            app.UseCsp(opts => opts
                                .BlockAllMixedContent()
                                .StyleSources(s => s.Self())
                                .StyleSources(s => s.UnsafeInline())
                                .FontSources(s => s.Self())
                                .FrameAncestors(s => s.Self())
                                .ImageSources(imageSrc => imageSrc.Self())
                                .ImageSources(imageSrc => imageSrc.CustomSources("data:"))

                                .ScriptSources(s => s.Self())
                                .ScriptSources(s => s.UnsafeInline())
                                .ScriptSources(x => x.UnsafeEval())

                //.ReportUris(x => x.Uris("/home/cspreport"))
                );
        }

        internal static void UseStaticFilesWithSecurityConsiderations(this IApplicationBuilder app)
        {
            app.UseStaticFiles(new StaticFileOptions()
            {

                OnPrepareResponse = context =>
                {
                    if (context.Context.Response.Headers["feature-policy"].Count == 0)
                    {
                        var featurePolicy = "accelerometer 'none'; camera 'none'; geolocation 'none'; gyroscope 'none'; magnetometer 'none'; microphone 'none'; payment 'none'; usb 'none'";

                        context.Context.Response.Headers["feature-policy"] = featurePolicy;
                    }

                    if (context.Context.Response.Headers["X-Content-Security-Policy"].Count == 0)
                    {
                        var csp = "script-src 'self';style-src 'self';img-src 'self' data:;font-src 'self';form-action 'self';frame-ancestors 'self';block-all-mixed-content";
                        // IE
                        context.Context.Response.Headers["X-Content-Security-Policy"] = csp;
                    }

                    const int durationInSeconds = 60 * 60 * 24 * 365; // 1year
                    context.Context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.CacheControl] = "public,max-age=" + durationInSeconds;
                }
            });
        }

        #endregion

        internal static void UseExceptionHandling(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 
            app.UseExceptionHandler("/Error/HandleUnhandledException");

            // staus codes handling
            app.UseStatusCodePagesWithReExecute("/Error/HandleStatusCodedError", "?statusCode={0}");

            // API ExceptionHandling
            app.ConfigureApiExceptionsHandler();
        }

        internal static void UseAntiforgeryTokenForAngularjs(this IApplicationBuilder app, IAntiforgery antiforgeryService)
        {
            app.Use(next => context =>
            {
                string path = context.Request.Path.Value;

                if (path.Contains("/api"))
                {
                    // The request token can be sent as a JavaScript-readable cookie, 
                    // and Angular uses it by default.
                    var tokens = antiforgeryService.GetAndStoreTokens(context);
                    context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken,
                        new CookieOptions() { HttpOnly = false, Secure = true, SameSite = SameSiteMode.Strict });
                }

                return next(context);
            });
        }

        private static void checkSameSite(HttpContext httpContext, CookieOptions options)
        {
            if (options.SameSite == SameSiteMode.None)
            {
                var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
                if (disallowsSameSiteNone(userAgent))
                {
                    // For .NET Core < 3.1 set SameSite = (SameSiteMode)(-1)
                    options.SameSite = SameSiteMode.Unspecified;
                }
            }
        }

        private static bool disallowsSameSiteNone(string userAgent)
        {
            // Cover all iOS based browsers here. This includes:
            // - Safari on iOS 12 for iPhone, iPod Touch, iPad
            // - WkWebview on iOS 12 for iPhone, iPod Touch, iPad
            // - Chrome on iOS 12 for iPhone, iPod Touch, iPad
            // All of which are broken by SameSite=None, because they use the iOS networking stack
            if (userAgent.Contains("CPU iPhone OS 12") || userAgent.Contains("iPad; CPU OS 12"))
            {
                return true;
            }

            // Cover Mac OS X based browsers that use the Mac OS networking stack. This includes:
            // - Safari on Mac OS X.
            // This does not include:
            // - Chrome on Mac OS X
            // Because they do not use the Mac OS networking stack.
            if (userAgent.Contains("Macintosh; Intel Mac OS X 10_14") &&
                userAgent.Contains("Version/") && userAgent.Contains("Safari"))
            {
                return true;
            }

            // Cover Chrome 50-69, because some versions are broken by SameSite=None, 
            // and none in this range require it.
            // Note: this covers some pre-Chromium Edge versions, 
            // but pre-Chromium Edge does not require SameSite=None.
            if (userAgent.Contains("Chrome/5") || userAgent.Contains("Chrome/6"))
            {
                return true;
            }

            return false;
        }

        internal static void AddSession(this IServiceCollection services, IWebHostEnvironment environment)
        {
            // Adds a default in-memory implementation of IDistributedCache.
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(2);
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Strict; //SameSiteMode.None; // SOC
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.IsEssential = true;
                options.Cookie.Name = Shared.Models.Constants.Security.CookieNames.Session;
            });
        }

        internal static void AddDataProtection(this IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
        {
            IAppLogger logger;
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                logger = scope.ServiceProvider.GetRequiredService<IAppLogger>();
            }

            var keysFolder = Path.Combine(environment.ContentRootPath, "Security", "_dataProtectionKeys");

            if (environment.IsDevelopment())
            {
                services.AddDataProtection()
                    .SetApplicationName("FinLib") // global purposeString for app's DataProtectionProvider
                    .PersistKeysToFileSystem(new DirectoryInfo(keysFolder))
                    .SetDefaultKeyLifetime(TimeSpan.FromDays(14));
            }
            else
            {
                services.AddDataProtection()
                    .SetApplicationName("FinLib") // global purposeString for app's DataProtectionProvider
                    .PersistKeysToFileSystem(new DirectoryInfo(keysFolder))
                    //.ProtectKeysWithCertificate(loadCertificateFromStore(configuration, environment, logger))
                    .SetDefaultKeyLifetime(TimeSpan.FromDays(14));
            }
        }

        private static GlobalSettings getGlobalSettings(IConfiguration configuration)
        {
            var theGlobalSettings = new GlobalSettings();
            configuration.GetSection(nameof(GlobalSettings)).Bind(theGlobalSettings);
            return theGlobalSettings;
        }

        internal static string GetConnectionStringWithoutPasswordText(IConfiguration configuration, string connectionStringName)
        {
            var theConnectionString = configuration[$"ConnectionStrings:{connectionStringName}"];
            var theConStrSplitted = theConnectionString.Split(';').ToDictionary(x => x.Split('=')[0].Trim());
            theConStrSplitted.Remove("Password");
            return string.Join(";", theConStrSplitted.Select(x => x.Value));
        }
    }
}
