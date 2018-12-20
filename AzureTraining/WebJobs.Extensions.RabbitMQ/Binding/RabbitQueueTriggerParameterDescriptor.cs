using System;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs.Host.Protocols;

namespace WebJobs.Extensions.RabbitMQ.Binding
{
    internal class RabbitQueueTriggerParameterDescriptor : TriggerParameterDescriptor
    {
        public override string GetTriggerReason(IDictionary<string, string> arguments) 
            => $"RabbitQueue trigger fired at {DateTime.UtcNow:o}";
    }
}
