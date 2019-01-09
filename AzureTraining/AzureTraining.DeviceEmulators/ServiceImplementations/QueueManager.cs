using System;
using System.Text;
using System.Threading;
using AzureTraining.DeviceEmulators.Abstractions.ServiceInterfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AzureTraining.DeviceEmulators.ServiceImplementations
{
    public class QueueManager : IQueueManager
    {
        private const string HostName = "localhost"; // TODO gets from config.
        private readonly ILogger _logger;
        private readonly ConnectionFactory _connectionFactory;
        private JsonSerializerSettings _jsonSerializerSettings;

        private JsonSerializerSettings JsonSerializerSettings
        {
            get
            {
                if (_jsonSerializerSettings == null)
                {
                    _jsonSerializerSettings = new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                        TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full,
                    };
                }

                return _jsonSerializerSettings;
            }
        }

        public QueueManager(ILogger logger)
        {
            _logger = logger;
            _connectionFactory = new ConnectionFactory { HostName = HostName };
        }

        /// <inheritdoc/>
        public T QueueMessage<T>(string inputQueue, string outputQueue, string exchange, string routingKey, T message)
        {
            PublishMessage(inputQueue, exchange, routingKey, message);
            var result = string.Empty;
            ReceiveMessage(outputQueue, exchange, outputQueue, (model, ea) =>
            {
                var body = ea.Body;
                result = Encoding.UTF8.GetString(body);
                _logger.Write($"Received '{routingKey}':'{result}'");
            });

            return JsonDeserializeMessage<T>(result);
        }

        private void PublishMessage(string queue, string exchange, string routingKey, object message)
        { 
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
                var body = Encoding.UTF8.GetBytes(JsonSerializeMessage(message));
                channel.BasicPublish(exchange, routingKey, null, body);
            }
        }

        private void ReceiveMessage
            (string queue, string exchange, string routingKey, EventHandler<BasicDeliverEventArgs> eventHandler)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(routingKey, durable: true, exclusive: false, autoDelete: false, arguments: null);
                    channel.QueueBind(queue, exchange, routingKey);
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += eventHandler;
                    channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);
                    Thread.Sleep(5000);
                }
            }
        }

        private string JsonSerializeMessage(object value)
            => JsonConvert.SerializeObject(value, JsonSerializerSettings);

        private TOutput JsonDeserializeMessage<TOutput>(string value) 
            => JsonConvert.DeserializeObject<TOutput>(value, JsonSerializerSettings);
    }
}