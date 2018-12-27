using System.Threading.Tasks;
using AzureTraining.DeviceEmulators.Abstractions;
using AzureTraining.DeviceEmulators.Abstractions.Factory;
using AzureTraining.DeviceEmulators.Abstractions.ServiceInterfaces;
using AzureTraining.DeviceEmulators.Devices.Model;
using AzureTraining.DeviceEmulators.Factory;
using AzureTraining.DeviceEmulators.Repositories;
using AzureTraining.DeviceEmulators.ServiceImplementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebJobs.Extensions.RabbitMQ.Config;

namespace AzureTraining.WebJob.Hub
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                .UseEnvironment("Development")
                .ConfigureWebJobs(b =>
                {
                    //b.AddCosmosDB();
                    b.AddRabbitMq();;
                })
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IRepository<HubItem>, HubRepository<HubItem>>();
                    services.AddSingleton<IHubManager, HubManager>();
                    services.AddSingleton<IDeviceFactory, DeviceFactory>();
                    services.AddSingleton<ILogger, ConsoleLogger>();
                })
                .UseConsoleLifetime();

            var host = builder.Build();

            using (host)
            {
                await host.RunAsync();
            }
        }
    }
}
