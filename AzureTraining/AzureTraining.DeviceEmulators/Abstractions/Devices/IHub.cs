using AzureTraining.DeviceEmulators.Abstractions.Devices;
using AzureTraining.DeviceEmulators.Enum;

namespace AzureTraining.DeviceEmulators.Abstractions
{
    public interface IHub
    {
        /// <summary>
        /// Registers new device
        /// </summary>
        void RegisterDevice(string deviceId);

        /// <summary>
        /// Executes registered device's special commands
        /// </summary>
        void ExecuteSpecificDeviceCommands(string id);

        /// <summary>
        /// Reboots registered device
        /// </summary>
        void RebootDevice(string id);

        /// <summary>
        /// Updates registered device's parameters
        /// </summary>
        void UpdateParams(string id, params string[] arr);

        /// <summary>
        /// Gets registered device status
        /// </summary>
        DeviceState GetDeviceState(string id);
    }
}
