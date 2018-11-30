using System;
using Asparagus.DeviceEmulators.Abstractions.Devices;
using Asparagus.DeviceEmulators.Enum;

namespace Asparagus.DeviceEmulators.Abstractions
{
    public interface IHub
    {
        /// <summary>
        /// Registers new device
        /// </summary>
        void RegisterDevice(IBaseDevice device);

        /// <summary>
        /// Executes registered device's special commands
        /// </summary>
        void ExecuteSpecificDeviceCommands(Guid id);

        /// <summary>
        /// Reboots registered device
        /// </summary>
        void RebootDevice(Guid id);

        /// <summary>
        /// Updates registered device's parameters
        /// </summary>
        void UpdateParams(Guid id, params string[] arr);

        /// <summary>
        /// Gets registered device status
        /// </summary>
        DeviceState GetDeviceState(Guid id);
    }
}
