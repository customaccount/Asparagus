using AzureTraining.DeviceEmulators.Abstractions.Devices;
using AzureTraining.DeviceEmulators.Abstractions.ServiceInterfaces;

namespace AzureTraining.DeviceEmulators.Devices
{
    public class Hub : BaseHub
    {
        public Hub(ILogger logger) : base(logger)
        {}
    }
}