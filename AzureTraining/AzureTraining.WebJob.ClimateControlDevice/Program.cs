using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using WebJobs.Extensions.RabbitMQ.Config;

namespace AzureTraining.WebJob.ClimateControlDevice
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
                    b.AddRabbitMq();
                });

            using (var host = builder.Build())
            {
                await host.RunAsync();
            }
        }
    }
}
