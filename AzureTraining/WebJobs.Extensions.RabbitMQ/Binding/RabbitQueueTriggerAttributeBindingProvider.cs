using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Triggers;
using RabbitMQ.Client;
using WebJobs.Extensions.RabbitMQ.Attributes;

namespace WebJobs.Extensions.RabbitMQ.Binding
{
    internal class RabbitQueueTriggerAttributeBindingProvider : ITriggerBindingProvider
    {
        private readonly IConnection _connection;

        public RabbitQueueTriggerAttributeBindingProvider(IConnection connection)
        {
            _connection = connection;
        }
        
        public Task<ITriggerBinding> TryCreateAsync(TriggerBindingProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var parameter = context.Parameter;
            var attribute = parameter.GetCustomAttribute<RabbitQueueTriggerAttribute>(inherit: false);

            if (attribute == null)
            {
                return Task.FromResult<ITriggerBinding>(null);
            }

            var queueBinderAttribute = parameter.GetCustomAttribute<RabbitQueueBinderAttribute>(inherit: false);
            
            return Task.FromResult<ITriggerBinding>(new RabbitQueueTriggerBinding(_connection,attribute.QueueName, queueBinderAttribute, context.Parameter));
        }
    }
}
