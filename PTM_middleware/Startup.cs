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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PTM_middleware.BLL;
using PTM_middleware.DAL;
using PTM_middleware.DAL.Manager;
using PTM_middleware.DAL.Repository;
using PTM_middleware.Models.DB;
using RSCD.MQTT;

namespace PTM_middleware
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
            services.AddHostedService<MqttClient>();
            services.AddScoped<MongoContext>();
            services.AddScoped<Traffic_BL>();
            services.AddScoped<IPedestrianLog,PedestrianLog_CM>();
            services.AddScoped<ISignalSwitchLog, SignalSwitch_CM>();
            services.AddScoped<IDataRepository<PedestrianCrossingLog>, PedestrianCrossingLog_CM>();
            services.AddScoped<IDataRepository<TrafficDensityLog>, TrafficDensityLog_CM>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
