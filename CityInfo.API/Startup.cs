﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace CityInfo.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //for json capital letter setting serialize 
            //services.AddMvc()
            //    .AddJsonOptions( o =>
            //        {
            //            if(o.SerializerSettings.ContractResolver != null)
            //            {
            //                var castedResolver = o.SerializerSettings.ContractResolver
            //                    as DefaultContractResolver;
            //                castedResolver.NamingStrategy = null;
            //            }

            //        }
            //    );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }
            app.UseStatusCodePages();

            app.UseMvc();
            

            //app.Run(async (context) =>
            //{
            //    //throw new Exception("Exception Ocuu");
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
