using System;
using Microsoft.Azure.WebJobs.Description;

namespace WebJobs.Extensions.RabbitMQ.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    [Binding]
    public sealed class RabbitQueueTriggerAttribute : Attribute
    {
        public RabbitQueueTriggerAttribute(string queueName)
        {
            QueueName = queueName;
        }

        public string QueueName { get; set; }
    }
}
