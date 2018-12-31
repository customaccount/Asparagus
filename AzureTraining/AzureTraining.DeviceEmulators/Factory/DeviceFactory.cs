using AzureTraining.DeviceEmulators.Abstractions.Devices;
using AzureTraining.DeviceEmulators.Abstractions.Factory;
using AzureTraining.DeviceEmulators.Abstractions.ServiceInterfaces;
using AzureTraining.DeviceEmulators.Devices;
using AzureTraining.DeviceEmulators.Devices.ClimateControlDevice;
using AzureTraining.DeviceEmulators.Devices.HumidifierControlDevice;
using AzureTraining.DeviceEmulators.Devices.LightingControlDevice;
using AzureTraining.DeviceEmulators.Devices.Model;

namespace AzureTraining.DeviceEmulators.Factory
{
    public class DeviceFactory : IDeviceFactory
    {
        /// <inheritdoc />
        public BaseDevice CreateClimateDevice(string name, ILogger logger)
            => new ClimateControlDevice(name, logger);

        /// <inheritdoc />
        public BaseDevice CreateClimateDevice(DeviceItem deviceItem, ILogger logger)
            => new ClimateControlDevice(deviceItem, logger);

        /// <inheritdoc />
        public BaseDevice CreateHumidifierDevice(string name, ILogger logger)
            => new HumidifierControlDevice(name, logger);

        /// <inheritdoc />
        public BaseDevice CreateLightingDevice(string name, ILogger logger)
            => new LightingControlDevice(name, logger);

        /// <inheritdoc />
        public BaseHub CreateHub(string id, ILogger logger)
            => new Hub(id, logger);
    }
}
