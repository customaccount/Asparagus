using Asparagus.DeviceEmulators.Abstractions.Command;

namespace Asparagus.DeviceEmulators.Devices.LightingControlDevice
{
    class LightingControlDeviceTurnOffCommand : ISpecialDeviceCommand
    {
        private readonly LightingControlDevice _device;

        public LightingControlDeviceTurnOffCommand(LightingControlDevice device)
        {
            _device = device;
        }

        /// <inheritdoc />
        public void Execute() => _device.TurnOff();
    }
}
