using AzureTraining.DeviceEmulators.Abstractions.Devices;
using AzureTraining.DeviceEmulators.Abstractions.ServiceInterfaces;
using AzureTraining.DeviceEmulators.Devices.Model;

namespace AzureTraining.DeviceEmulators.Abstractions.Factory
{
    public interface IDeviceFactory
    {
        /// <summary>
        /// Creates Climate device
        /// </summary>
        BaseDevice CreateClimateDevice(string name, ILogger logger);

        /// <summary>
        /// Creates Climate device 
        /// </summary>
        BaseDevice CreateClimateDevice(DeviceItem deviceItem, ILogger logger);

        /// <summary>
        /// Creates Humidifier device
        /// </summary>
        BaseDevice CreateHumidifierDevice(string name, ILogger logger);

        /// <summary>
        /// Creates Lighting device
        /// </summary>
        BaseDevice CreateLightingDevice(string name, ILogger logger);
        
        /// <summary>
        /// Creates Hub device
        /// </summary>
        BaseHub CreateHub(string id, ILogger logger);
    }
}
