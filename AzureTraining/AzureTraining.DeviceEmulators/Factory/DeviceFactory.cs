using AzureTraining.DeviceEmulators.Abstractions;
using AzureTraining.DeviceEmulators.Abstractions.Devices;
using AzureTraining.DeviceEmulators.Abstractions.Factory;
using AzureTraining.DeviceEmulators.Abstractions.ServiceInterfaces;
using AzureTraining.DeviceEmulators.Devices;
using AzureTraining.DeviceEmulators.Devices.ClimateControlDevice;
using AzureTraining.DeviceEmulators.Devices.HumidifierControlDevice;
using AzureTraining.DeviceEmulators.Devices.LightingControlDevice;

namespace AzureTraining.DeviceEmulators.Factory
{
    public class DeviceFactory : IDeviceFactory
    {
        public IBaseDevice CreateClimateDevice(string name, ILogger logger)
            => new ClimateControlDevice(name, logger);

        public IBaseDevice CreateHumidifierDevice(string name, ILogger logger)
            => new HumidifierControlDevice(name, logger);

        public IBaseDevice CreateLightingDevice(string name, ILogger logger)
            => new LightingControlDevice(name, logger);

        public IHub CreateHub(ILogger logger)
            => new Hub(logger);
    }
}
