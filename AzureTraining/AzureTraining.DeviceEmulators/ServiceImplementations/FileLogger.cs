using System.IO;
using System.Text;
using AzureTraining.DeviceEmulators.Abstractions.ServiceInterfaces;

namespace AzureTraining.DeviceEmulators.ServiceImplementations
{
    public class FileLogger : ILogger
    {
        public void Write(string message)
        {
            using (var file = new StreamWriter("C:\\chamb\\log.txt", true, Encoding.Default))
            {
                file.WriteLineAsync(message);
            }
        }
    }
}
