using System.Collections.Generic;
using Asparagus.DeviceEmulators.Abstractions.Command;
using Asparagus.DeviceEmulators.Enum;

namespace Asparagus.DeviceEmulators.Abstractions.Devices
{
    public interface IBaseDevice
    {
        /// <summary>
        /// Gets/sets device name;
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Registers device in the hub
        /// </summary>
        void Register();

        /// <summary>
        /// Returns device state
        /// </summary>
        DeviceState GetDeviceState();

        /// <summary>
        /// Sets device state
        /// </summary>
        void SetDeviceState(DeviceState deviceState);

        /// <summary>
        /// Reboots device
        /// </summary>
        void Reboot();

        /// <summary>
        /// Updates device parameters
        /// </summary>
        void UpdateParams(params string[] arr);

        /// <summary>
        /// Returns list of special device's commands
        /// </summary>
        IEnumerable<ISpecialDeviceCommand> GetSpecialDeviceCommands();
    }
}
