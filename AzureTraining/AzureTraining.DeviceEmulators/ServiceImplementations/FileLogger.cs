using System.IO;
using System.Text;
using AzureTraining.DeviceEmulators.Abstractions.ServiceInterfaces;

namespace AzureTraining.DeviceEmulators.ServiceImplementations
{
    public class FileLogger : ILogger
    {
        private const string _path = @"C:\temp\log.txt";

        public void Write(string message)
        {
            using (var file = new StreamWriter(_path, true, Encoding.Default))
            {
                file.WriteLineAsync(message);
            }
        }
    }
}
