using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StudentCart.Repository.Business;
using StudentCart.Repository.Business.Contracts;
using StudentCart.Repository.Data;
using StudentCart.Repository.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace StudentCart.WebServices
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connString = Configuration.GetSection("ConnectionStrings").GetSection("connectionString").Value;

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.ConsentCookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            //services.AddControllers().AddControllersAsServices().ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);


            services.AddSingleton<IDatabaseCollection, DatabaseManager>(options => new DatabaseManager(connString));
            //services.AddControllersWithViews();
            services.AddTransient<IStudentsCartManager, StudentsCartManager>();
            services.AddTransient<IStudentsCartRepository, StudentsCartRepository>();

            services.AddMvc().AddMvcOptions(options =>
                            {
                                options.MaxModelValidationErrors = 999999;
                                options.MaxValidationDepth = 999;
                            });
            services.AddApiVersioning(ver =>
            {
                ver.ReportApiVersions = true;
                ver.AssumeDefaultVersionWhenUnspecified = true;           // Will throw an expception if versioning is not mentioned
                ver.DefaultApiVersion = new ApiVersion(1, 0);            // always take default version.
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
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
            //loggerFactory.AddLog4Net();
            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("spa-fallback", "{*url}", new { controller = "Home", action = "Index" });
            });
        }
    }
}
