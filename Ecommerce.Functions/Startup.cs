using System;
using System.IO;
using Ecommerce.Application;
using Ecommerce.EmailService;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Ecommerce.Functions.Startup))]

namespace Ecommerce.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            ConfigureServices(builder.Services);
        }

        private void ConfigureServices(IServiceCollection services)
        {

            var configBuilder = new ConfigurationBuilder();
            configBuilder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
            var config = configBuilder.AddEnvironmentVariables().Build();

            services.AddSingleton<IEmailService, SendGridService>();
            services.AddSingleton<IOrderService, OrderService>();

             var appSettings = config.Get<Settings>();
             services.AddSingleton(appSettings);
        }
    }
}