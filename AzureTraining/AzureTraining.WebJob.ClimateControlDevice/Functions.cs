using WebJobs.Extensions.RabbitMQ.Attributes;

namespace AzureTraining.WebJob.ClimateControlDevice
{
    public class Functions
    {
        //public static void ProcessQueueMessage(
        //    [RabbitQueueTrigger("hello")] string message)
        //{}

        public static void SendQueueMessage([RabbitMessage("", "hello")] out string message)
        {
            message = "message from climate device";
        }
    }
}
