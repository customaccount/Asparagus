using System.Threading.Tasks;
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
                });

            var host = builder.Build();

            using (host)
            {
                await host.RunAsync();
            }
        }
    }
}
