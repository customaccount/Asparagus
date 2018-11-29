using Asparagus.DeviceEmulators.Abstractions.Command;

namespace Asparagus.DeviceEmulators.Devices.LightingControlDevice
{
    public class LightingControlDeviceTurnOnCommand : ISpecialDeviceCommand
    {
        private readonly LightingControlDevice _device;

        public LightingControlDeviceTurnOnCommand(LightingControlDevice device)
        {
            _device = device;
        }

        /// <inheritdoc />
        public void Execute() => _device.TurnOn();
    }
}
