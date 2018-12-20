using System;
using System.IO;
using WebJobs.Extensions.RabbitMQ.Attributes;

namespace AzureTraining.WebJob.Hub
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an RabbitMq Queue.
        public static void ProcessQueueMessage(
            //[RabbitQueueBinder("", "test")]
            [RabbitQueueTrigger("test")]
            string message, TextWriter log)
        {
            //log.WriteLine(message);
            Console.WriteLine(message);
        }
    }
}
