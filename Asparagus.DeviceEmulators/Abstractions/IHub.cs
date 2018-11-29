using Asparagus.DeviceEmulators.Abstractions.Devices;
using Asparagus.DeviceEmulators.Enum;

namespace Asparagus.DeviceEmulators.Abstractions
{
    public interface IHub
    {
        /// <summary>
        /// Gets/sets hub name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Registers new device
        /// </summary>
        void RegisterDevice(IBaseDevice device);

        /// <summary>
        /// Executes registered device's special commands
        /// </summary>
        void ExecuteSpecificDeviceCommands(IBaseDevice device);

        /// <summary>
        /// Reboots registered device
        /// </summary>
        void RebootDevice(IBaseDevice device);

        /// <summary>
        /// Updates registered device's parameters
        /// </summary>
        void UpdateParams(IBaseDevice device, params string[] arr);

        /// <summary>
        /// Gets registered device status
        /// </summary>
        DeviceState GetDeviceState(IBaseDevice device);
    }
}
