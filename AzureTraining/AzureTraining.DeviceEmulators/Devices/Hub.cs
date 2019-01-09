using AzureTraining.DeviceEmulators.Abstractions.Devices;
using AzureTraining.DeviceEmulators.Abstractions.ServiceInterfaces;
using AzureTraining.DeviceEmulators.Devices.Model;

namespace AzureTraining.DeviceEmulators.Devices
{
    public class Hub : BaseHub
    {
        public Hub(string id, ILogger logger) : base(id, logger)
        {}

        public Hub(HubItem hubItem, ILogger logger) : base(hubItem, logger)
        {}
    }
}