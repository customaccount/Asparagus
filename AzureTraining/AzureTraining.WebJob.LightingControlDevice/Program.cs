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

namespace AzureTraining.WebJob.LightingControlDevice
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                .UseEnvironment("Development")
                .ConfigureWebJobs(b =>
                {
                    //b.AddCosmosDB();
                    b.AddRabbitMq();
                })
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IRepository<DeviceItem>, DeviceRepository<DeviceItem>>();
                    services.AddSingleton<IDeviceManager, DeviceManager>();
                    services.AddSingleton<IDeviceFactory, DeviceFactory>();
                    services.AddSingleton<ILogger, ConsoleLogger>();
                })
                .UseConsoleLifetime();

            using (var host = builder.Build())
            {
                await host.RunAsync();
            }
        }
    }
}
