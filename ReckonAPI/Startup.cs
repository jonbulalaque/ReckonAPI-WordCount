using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Polly.Extensions.Http;
using ReckonAPI.Service.Interfaces;
using ReckonAPI.Service.Services;
using ReckonAPI.Shared.Helpers;
using System;

namespace ReckonAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            ConfigHelper.PostResultUrl = Configuration["ReckonInformation:PostResultUrl"].ToString();
            ConfigHelper.RetryCount = Convert.ToInt32(Configuration["ReckonInformation:RetryCount"]);
            ConfigHelper.RetryWaitMs = Convert.ToInt32(Configuration["ReckonInformation:RetryWaitMs"]);
            ConfigHelper.SubTextUrl = Configuration["ReckonInformation:SubTextUrl"].ToString();
            ConfigHelper.TextToSearchUrl = Configuration["ReckonInformation:TextToSearchUrl"].ToString();            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddControllers();
            services.AddScoped<ITestService, TestService>();
            services.AddScoped<IWordCountService, WordCountService>();
            services.AddScoped<IReckonTextService, ReckonTextService>();

            var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
                             .WaitAndRetryAsync(ConfigHelper.RetryCount, retryAttempt => TimeSpan.FromMilliseconds(ConfigHelper.RetryWaitMs));

            services.AddHttpClient<IReckonTextService, ReckonTextService>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(retryPolicy);
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
