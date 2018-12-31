using System;
using System.Text;
using System.Threading.Tasks;
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

        public QueueManager(ILogger logger)
        {
            _logger = logger;
            
        }

        /// <inheritdoc/>
        public T QueueMessage<T>(string inputQueue, string outputQueue, string exchange, string routingKey, T message)
        {
            PublishMessage(inputQueue, exchange, routingKey, message);
            var result = string.Empty;
            ReceiveMessage<T>(outputQueue, exchange, outputQueue, (model, ea) =>
            {
                var body = ea.Body;
                result = Encoding.UTF8.GetString(body);
                _logger.Write($"Received '{routingKey}':'{result}'");
            });

            //Task.Delay(1000);

            return JsonDeserializeMessage<T>(result);
        }

        private void PublishMessage(string queue, string exchange, string routingKey, object message)
        {
            var connectionFactory = new ConnectionFactory() { HostName = HostName };
            using (var connection = connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
                var body = Encoding.UTF8.GetBytes(JsonSerializeMessage(message));
                channel.BasicPublish(exchange, routingKey, null, body);
            }
        }

        private void ReceiveMessage<T>
            (string queue, string exchange, string routingKey, EventHandler<BasicDeliverEventArgs> eventHandler)
        {
            var connectionFactory = new ConnectionFactory() { HostName = HostName };
            using (var connection = connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(routingKey, durable: true, exclusive: false, autoDelete: false, arguments: null);
                channel.QueueBind(queue, exchange, routingKey);
            }

            using (var connection = connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false, arguments: null);

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += eventHandler;

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        _logger.Write($" [x] Received '{routingKey}':'{message}'");
                    };

                    channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);
                }
            }
        }

        private string JsonSerializeMessage(object value)
        {
            return JsonConvert.SerializeObject(value,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full,
                });
        }

        private TOutput JsonDeserializeMessage<TOutput>(string value)
        {
            return JsonConvert.DeserializeObject<TOutput>(value,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full,
                });
        }
    }
}