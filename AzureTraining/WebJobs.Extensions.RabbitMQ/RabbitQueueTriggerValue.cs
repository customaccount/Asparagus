using System.Collections.Generic;

namespace WebJobs.Extensions.RabbitMQ
{
    public class RabbitQueueTriggerValue
    {
        public string MessageId { get; set; }
        public string ApplicationId { get; set; }
        public string ContentType { get; set; }
        public string CorrelationId { get; set; }
        public IDictionary<string, object> Headers { get; set; }
        public byte[] MessageBytes { get; set; }
    }
}
