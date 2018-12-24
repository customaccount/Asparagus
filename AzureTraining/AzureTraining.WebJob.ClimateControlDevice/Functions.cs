using System;
using WebJobs.Extensions.RabbitMQ.Attributes;

namespace AzureTraining.WebJob.ClimateControlDevice
{
    public class Functions
    {
        [return: RabbitMessage("", "hello2")]
        public static string SendQueueMessage([RabbitQueueTrigger("hello")] string message)
        {
            Console.WriteLine(message);
            return "Test";
        }
    }
}
