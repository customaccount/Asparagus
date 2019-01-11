using System;
using Microsoft.Azure.WebJobs.Description;

namespace WebJobs.Extensions.RabbitMQ.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    [Binding]
    public class RabbitQueueBinderAttribute : Attribute
    {
        public RabbitQueueBinderAttribute(string exchange, string routingKey, string errorExchange = "", 
            bool autoDelete = false, bool durable = true, bool exclusive = false)
        {
            Exchange = exchange;
            RoutingKey = routingKey;
            ErrorExchange = errorExchange;
            AutoDelete = autoDelete;
            Durable = durable;
            Exclusive = exclusive;
        }

        public bool AutoDelete { get; set; }
        public bool Durable { get; set; }
        public bool Exclusive { get; set; }
        public string Exchange { get; set; }
        public string RoutingKey { get; set; }
        public string ErrorExchange { get; set; }
    }
}
