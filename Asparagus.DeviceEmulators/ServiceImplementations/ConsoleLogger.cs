using Asparagus.DeviceEmulators.Abstractions.ServiceInterfaces;
using System;

namespace Asparagus.DeviceEmulators.ServiceImplementations
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
