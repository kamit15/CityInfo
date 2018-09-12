using System.Reflection;
using CityInfo.API.Entities;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NJsonSchema;
using NLog.Extensions.Logging;
using NSwag.AspNetCore;

namespace CityInfo.API
{
    public class Startup
    {
        //For ASP.net core 1
        //public static IConfigurationRoot Configuration;
        //Create Default Builder already implement IConfigurationRoot
        public static IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration config)
        //public Startup(IHostingEnvironment env)
        {
            //For ASP.net core 1
            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(env.ContentRootPath)
            //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //    .AddJsonFile($"appsettings.{env.EnvironmentName}.json",optional:true,reloadOnChange:true);

            //Configuration = builder.Build();
            Configuration = config;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // clear all formatter and then add specific one or remove specific one
            services.AddMvc()
                .AddMvcOptions(o => o.OutputFormatters
                .Add(new XmlDataContractSerializerOutputFormatter()));

            //best for lightweight stateless services
#if DEBUG
            services.AddTransient<IMailService, LocalMailService>();
#else
            services.AddTransient<IMailService, CloudMailService>();
#endif
            var connectionString = @"Server=(localdb)\mssqllocaldb;Database=CityInfoDB;Trusted_Connection=True";
            services.AddDbContext<CityInfoContext>(o => o.UseSqlServer(connectionString));
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

        // This method gets called by the runtime. Ufse this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            loggerFactory.AddDebug();

            //loggerFactory.AddProvider(new NLog.Extensions.Logging.NLogLoggerProvider());
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }
            

            app.UseStatusCodePages();
            app.UseSwaggerUi(typeof(Startup).GetTypeInfo().Assembly, settings =>
            {
                settings.GeneratorSettings.DefaultPropertyNameHandling =
                PropertyNameHandling.CamelCase;
                settings.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "ToDo API";
                    document.Info.Description = "A simple ASP.NET Core web API";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.SwaggerContact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = "https://twitter.com/spboyer"
                    };
                    document.Info.License = new NSwag.SwaggerLicense
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    };
                };
            });

            app.UseMvc();


            //app.Run(async (context) =>
            //{
            //    //throw new Exception("Exception Ocuu");
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
