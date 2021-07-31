using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartApp.DotNetCore.Services.CleintConfig;
using SmartApp.DotNetCore.Services.Implementation;
using SmartApp.DotNetCore.Services.Interfaces;
using System;
using System.Net.Http;

namespace SmartApp.DotNetCore
{
    public class Startup
    {
        private static SmartAppService smartAppService;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddTransient<ISmartAppartmentService, SmartAppartmentService>();
            services.Configure<SmartAppService>(Configuration.GetSection(nameof(SmartAppService)));

            smartAppService = Configuration.GetSection(nameof(SmartAppService)).Get<SmartAppService>();
            services.AddHttpClient(smartAppService.ClientName, cfg =>
            {
                cfg.BaseAddress = new Uri(smartAppService.BaseUrl);
                cfg.Timeout = TimeSpan.FromMinutes(double.Parse(smartAppService.ClientTimeOut));
            });
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
