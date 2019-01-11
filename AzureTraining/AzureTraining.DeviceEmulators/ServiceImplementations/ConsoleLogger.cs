using System;
using AzureTraining.DeviceEmulators.Abstractions.ServiceInterfaces;

namespace AzureTraining.DeviceEmulators.ServiceImplementations
{
    public class ConsoleLogger : ILogger
    {
        /// <inheritdoc />
        public void Write(string message)
        {
            Console.WriteLine($"ConsoleLogger : {message}");
        }
    }
}
