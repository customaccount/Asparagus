using System;
using Microsoft.Azure.WebJobs.Description;

namespace WebJobs.Extensions.RabbitMQ.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    [Binding]
    public class RabbitMessageAttribute : Attribute
    {
        public string Exchange { get; set; }
        public string RoutingKey { get; set; }
        public bool Mandatory { get; set; }

        public RabbitMessageAttribute(string exchange, string routingKey, bool mandatory = false)
        {
            Exchange = exchange;
            RoutingKey = routingKey;
            Mandatory = mandatory;
        }
    }
}
