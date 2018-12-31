using AzureTraining.DeviceEmulators.Abstractions.Devices;
using AzureTraining.DeviceEmulators.Abstractions.ServiceInterfaces;

namespace AzureTraining.DeviceEmulators.Devices
{
    public class Hub : BaseHub
    {
        public Hub(string id, ILogger logger) : base(id, logger)
        {}
    }
}