using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using WebJobs.Extensions.RabbitMQ.Attributes;
using WebJobs.Extensions.RabbitMQ.Binding;

namespace WebJobs.Extensions.RabbitMQ.Config
{
    [Extension("RabbitMqExtensionConfigProvider")]
    internal class RabbitMqExtensionConfigProvider : IExtensionConfigProvider
    {
        private readonly IConnection _connection;
        private readonly RabbitMqOptions _options;
        private readonly INameResolver _nameResolver;
        private readonly IConfiguration _configuration;

        public RabbitMqExtensionConfigProvider(IOptions<RabbitMqOptions> options, IConfiguration configuration, INameResolver nameResolver, ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _nameResolver = nameResolver;
            _options = options.Value;

            var factory = new ConnectionFactory()
            {
                HostName = options.Value.ServerEndpoint,
                AutomaticRecoveryEnabled = options.Value.IsAutoConnectionRecoveryEnabled

            };

            _connection = factory.CreateConnection();
        }

        public RabbitMqExtensionConfigProvider(string serverEndpoint, bool automaticRecovery = true)
        {
            
            var factory = new ConnectionFactory()
            {
                HostName = serverEndpoint,
                AutomaticRecoveryEnabled = automaticRecovery

            };

            _connection = factory.CreateConnection();
        }

        public void Initialize(ExtensionConfigContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var trigger = new RabbitQueueTriggerAttributeBindingProvider(_connection);
            // Register our extension binding providers
            var rule = context.AddBindingRule<RabbitQueueBinderAttribute>();
            rule.BindToTrigger(trigger);

            var rule1 = context.AddBindingRule<RabbitQueueTriggerAttribute>();
            rule1.BindToTrigger(trigger);

            var rule2  = context.AddBindingRule<RabbitMessageAttribute>();
            rule2.Bind(new RabbitMessageAttributeBindingProvider(_connection));
        }
    }
}