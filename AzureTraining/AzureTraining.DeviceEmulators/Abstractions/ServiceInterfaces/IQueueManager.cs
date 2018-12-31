namespace AzureTraining.DeviceEmulators.Abstractions.ServiceInterfaces
{
    public interface IQueueManager
    {
        /// <summary>
        /// Queues message
        /// </summary>
        T QueueMessage<T>(string inputQueue, string  outputQueue, string exchange, string routingKey, T message);
    }
}
