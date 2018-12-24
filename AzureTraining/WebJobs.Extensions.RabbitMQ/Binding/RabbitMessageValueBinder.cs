using System;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace WebJobs.Extensions.RabbitMQ.Binding
{
    internal class RabbitMessageValueBinder : IValueBinder
    {
        private readonly IConnection _connection;
        private readonly string _exchange;
        private readonly string _routingKey;
        private readonly bool _mandatory;
        private readonly ParameterInfo _parameter;

        public Type Type => _parameter.ParameterType;

        public RabbitMessageValueBinder(IConnection connection, string exchange,string routingKey, bool mandatory, ParameterInfo parameter)// : base(parameter.ParameterType)
        {
            _connection = connection;
            _exchange = exchange;
            _routingKey = routingKey;
            _mandatory = mandatory;
            _parameter = parameter;
        }

        public Task<object> GetValueAsync()
        {
            if (Type.IsAbstract || Type.IsInterface || _parameter.IsOut)
            {
                return null;
            }

            return new Task<object>(() =>  Activator.CreateInstance(Type));
        }

        public string ToInvokeString()
        {
            return _parameter.Name;
        }

        public Task SetValueAsync(object value, CancellationToken cancellationToken)
        {
            if (value == null || !_parameter.IsOut)
            {
                return Task.FromResult(0);
            }

            using (var channel = _connection.CreateModel())
            {
                channel.BasicReturn += (sender, args) =>
                {
                    if (_mandatory)
                    {
                        if (args.ReplyCode == 312)
                        {
                            throw new Exception("No Queue found to accept this message");
                        }
                    }
                };

                if (value is string)
                {
                    PublishMessage(channel, (string)value);
                }
                else if (value is IEnumerable)
                {
                    foreach (var message in (IEnumerable) value)
                    {
                        PublishMessage(channel, JsonMessage(message));
                    }
                }
                else
                {
                    PublishMessage(channel, JsonMessage(value));
                }
            }
            
            return Task.FromResult(true);
        }

        private string JsonMessage(object value)
        {
            return JsonConvert.SerializeObject(value,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                        TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full,
                    });
        }

        private void PublishMessage(IModel channel, string message)
        {
            channel.BasicPublish(_exchange, _routingKey,_mandatory, null, Encoding.UTF8.GetBytes(message));
        }
    }
}