using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using ResourceDownloads.Models;

namespace ResourceDownloads
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
            services.AddControllers().AddNewtonsoftJson();

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.KnownNetworks.Add(new IPNetwork(System.Net.IPAddress.Parse("::ffff:" + appSettings.ProxyIp), 104));
            });

            services.AddDbContextPool<ResourcesContext>(
                dbContextOptions => dbContextOptions
                    .UseLazyLoadingProxies()
                    .UseMySql(
                        // Replace with your connection string.
                        Configuration.GetConnectionString("DefaultConnection"),
                        // Replace with your server version and type.
                        mySqlOptions => mySqlOptions
                            .ServerVersion(new System.Version(10, 5, 6), ServerType.MariaDb)
                            .CharSetBehavior(CharSetBehavior.NeverAppend))
                                            // Everything from this point on is optional but helps with debugging.
#if DEBUG
                                            .EnableSensitiveDataLogging()
#endif
                                            .EnableDetailedErrors()
                                            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
