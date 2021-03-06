﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace zmanimapi
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
            //add cors so clients can consume the api with a static page
            services.AddCors();
            services.AddMvc();
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //enable cors for all requests to allow cross domain acsess to the zemanim api
            app.UseCors(builder =>
     builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            //enable static files for the help page that will be routed through the controller
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
               routes.MapRoute("NewZmanimRoute", "api/zmanim",defaults: new { controller = "Zmanim", action = "Index" });
                routes.MapRoute("NewCalendarRoute", "api/calendar", defaults: new { controller = "Calendar", action = "Index" });
                routes.MapRoute("oldZmanimRoute", "api", defaults: new { controller = "Zmanim", action = "Index" });
                routes.MapRoute("MainHelpRoute", "help", defaults: new { controller = "Help", action = "Help" });
                routes.MapRoute("ZmanimHelp", "help/zmanim", defaults: new { controller = "Help", action = "ZmanimHelp" });
                routes.MapRoute("CalendarHelp", "help/calendar", defaults: new { controller = "Help", action = "CalendarHelp" });
                routes.MapRoute("GoogleAssistant", "actions/ga", defaults: new { controller = "Ga", action = "Post" });
                //       routes.MapRoute("default", "{controller}/{action}");
                routes.MapRoute("Spa", "{*url}", defaults: new { controller = "Help", action = "Help" });
            });
        }
    }
}
