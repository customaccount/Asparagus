using AzureTraining.DeviceEmulators.Abstractions.Devices;
using AzureTraining.DeviceEmulators.Abstractions.ServiceInterfaces;

namespace AzureTraining.DeviceEmulators.Abstractions.Factory
{
    public interface IDeviceFactory
    {
        /// <summary>
        /// Creates Climate device
        /// </summary>
        IBaseDevice CreateClimateDevice(string name, ILogger logger);

        /// <summary>
        /// Creates Humidifier device
        /// </summary>
        IBaseDevice CreateHumidifierDevice(string name, ILogger logger);

        /// <summary>
        /// Creates Lighting device
        /// </summary>
        IBaseDevice CreateLightingDevice(string name, ILogger logger);
        
        /// <summary>
        /// Creates Hub device
        /// </summary>
        IHub CreateHub(ILogger logger);
    }
}
