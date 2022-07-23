using FinLib.Models.Configs;
using FinLib.Providers.Configuration;
using FinLib.Services.Base;
using FinLib.Web.Helpers;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using System.IdentityModel.Tokens.Jwt;

namespace FinLib.Web
{
    public class Startup
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _environment = webHostEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAppSettings(_configuration);

            services.AddLogger();

            services.AddCommonDependencies();

            services.AddDbContext(_configuration, _environment);

            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            services.AddIdentity(_configuration);

            services.AddAuthentication().AddCookie(options =>
            {
                options.Cookie.IsEssential = true;
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.None;
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Login";
            });

            services.AddCookiePolicies(_environment);

            // Add detection services (UserAgent, Browser name, version, ...) container and device resolver service.
            services.AddDetection();

            services.AddSession(_environment);

            services.AddMappers();

            services.AddCommonServices();

            services.AddDataProtection(_environment, _configuration);

            services.AddControllersAndViews();

            services.AddIpRateLimitting(_configuration);

            services.AddResponseCaching();

            services.AddCompression();

            services.AddProviders(_configuration);

            services.AddDistributedCache(_configuration);
        }

        public void Configure(IApplicationBuilder app
                                , IWebHostEnvironment env
                                , IAntiforgery antiforgery
                                , IAppSettingsProvider<GlobalSettings> globalSettingsProvider
                                , ICommonServicesProvider<GlobalSettings> commonServicesProvider)
        {
            IdentityModelEventSource.ShowPII = true;

            app.UseResponseCompression();

            // CSP (Registered before static files to always set header)
            app.UseSecurityConsiderations();
            app.UseStaticFilesWithSecurityConsiderations();

            app.UseHttpsRedirection();

            app.UseCookiePolicy();

            // Use detection services (UserAgent, Browser name, version, ...) container and device resolver service.
            app.UseDetection();
            
            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseExceptionHandling(env);

            app.UseAntiforgeryTokenForAngularjs(antiforgery);

            app.SeedDatabase(globalSettingsProvider, commonServicesProvider);

            app.UseEndpoints(endpoints =>
            {
                // ASP.NET Core MVC
                endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Account}/{action=Login}/{id?}");

                // Area
                endpoints.MapAreaControllerRoute(
                        name: "ControlPanel",
                        areaName: "ControlPanel",
                        pattern: "{area:exists}/{controller=HomController}/{action=Index}/{id?}");
            });
        }
    }

}
