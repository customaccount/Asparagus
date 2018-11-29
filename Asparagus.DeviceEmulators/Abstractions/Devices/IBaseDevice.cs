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
        /// Reboots device
        /// </summary>
        void Reboot();

        /// <summary>
        /// Updates device parameters
        /// </summary>
        void UpdateParams(params string[] arr);
    }
}
