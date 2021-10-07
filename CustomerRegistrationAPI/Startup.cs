using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
 




namespace CustomerRegistrationAPI
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
            // services.AddControllers();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                    .WithOrigins(
                        "https://*.kkfnets.com", 
                        "http://*.kkfnets.com",
                        "http://191.20.20.248:8080",
                         "http://localhost:8080"
                        )
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithMethods("GET", "PUT", "POST", "DELETE");
                });
                options.AddPolicy("AllowCorsDev", builder =>
                {
                    builder
                    .WithOrigins("http://localhost:8080",
                    "http://191.20.20.248:8080"
                          
                    )
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithMethods("GET", "PUT", "POST", "DELETE");
                });
                 
            });

           // services.AddDirectoryBrowser

            // services.AddWebSocketManager();
            //   services.AddHostedService<JobService>();

            services.AddControllersWithViews().AddNewtonsoftJson();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env. IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //  app.UseCors("AllowCorsDev");
                app.UseHsts();
            }

            if (env.IsProduction())
            {
                app.UseHsts();
            }
            else {
                 app.UseCors("AllowCorsDev");
               
            }


            CustomerRegistrationADO.Connect.Mssql.CRMDB.CRMDBBase.conString     = Configuration["crmdb"];
            CustomerRegistrationADO.Connect.Mssql.Xerox.XeroxBase.conString = Configuration["xerox"];
            CustomerRegistrationADO.Connect.Mssql.Centraldb.CentraldbBase.conString = Configuration["centraldb"];
            CustomerRegistrationADO.Connect.Oracle.HrmsV10.HrmsV10Base.conString = Configuration["HrmsV10"];


            var wsOptions = new WebSocketOptions
            {
                KeepAliveInterval = TimeSpan.FromSeconds(60),
                ReceiveBufferSize = 6 * 1024
            };


            app.UseStaticFiles();

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseWebSockets(wsOptions);


            //  app.MapWebSocketManager("/socket", serviceProvider.GetService<SocketHandler>());



            //  app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
