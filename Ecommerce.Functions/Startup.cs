using System.IO;
using Ecommerce.Application;
using Ecommerce.EmailService;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
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
             
            var enableKeyVault = config["keyVaultName"] != null;

            if (enableKeyVault)
            {
                var azureServiceTokenProvider = new AzureServiceTokenProvider();
                var keyVaultClient = new KeyVaultClient(
                    new KeyVaultClient.AuthenticationCallback(
                        azureServiceTokenProvider.KeyVaultTokenCallback));

                config = configBuilder
                    .AddAzureKeyVault($"https://{config["keyVaultName"]}.vault.azure.net", keyVaultClient,
                        new DefaultKeyVaultSecretManager())
                    .Build();
            }

            services.AddSingleton<IEmailService, SendGridService>();
            services.AddSingleton<IOrderService, OrderService>();

             var appSettings = config.Get<Settings>();
             services.AddSingleton(appSettings);
        }
    }
}