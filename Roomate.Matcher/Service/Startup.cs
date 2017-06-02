using System;
using System.Text;
using Domain.Infrastructure;
using Domain.Persistence.Providers.MongoDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization;
using ProfilesMatcherContext.Domain.Model.Profiles;
using ProfilesMatcherContext.Domain.Model.Profiles.Repositories;
using ProfilesMatcherContext.Service.DataTransferObjects;
using RawRabbit.vNext;

namespace ProfilesMatcherContext.Service
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddRawRabbit();

            services.AddTransient<MongoDbRepository<MatchedProfile>, MatchedProfilesRepository>();
            services.AddSingleton(new MongoDataContext("mongodb://localhost", "profilesMatcher"));
            services.AddSingleton(new ProfilesAdapter(new Uri("https://roomate-production.herokuapp.com/")));
            services.AddTransient<IProfilesMatcher, ProfilesMatcher>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        var ex = error.Error;

                        await context.Response.WriteAsync(new ErrorDto()
                        {
                            Code = 1,
                            Message = ex.Message
                        }.ToString(), Encoding.UTF8);
                    }
                });
            });
            app.UseMvc();
        }
    }
}
