using System;
using AzureTraining.DeviceEmulators.Abstractions.ServiceInterfaces;
//using WebJobs.Extensions.RabbitMQ.Attributes;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace AzureTraining.WebJob.Hub
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an RabbitMq Queue.
        public static void ProcessQueueMessage(
            //[RabbitQueueBinder("", "hello10")]
            //[RabbitQueueTrigger("hello10")]
            string message/*, ILogger logger*/)
        {
            //log.WriteLine(message);
            Console.WriteLine(message);
            //logger.Write(message);
        }
    }
}
