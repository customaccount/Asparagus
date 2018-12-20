namespace WebJobs.Extensions.RabbitMQ
{
    public class RabbitMqOptions
    {
        /// <summary>
        /// Gets or sets the RabbitMq server endpoint.
        /// </summary>
        public string ServerEndpoint { get; set; }
        public string VirtualHost { get; set; }
        public int RequestedHeartbeat { get; set; }
        /// <summary>
        /// Gets or sets the RabbitMq AutoConnectionRecovery option state.
        /// </summary>
        public bool IsAutoConnectionRecoveryEnabled { get; set; } = true;
    }
}
