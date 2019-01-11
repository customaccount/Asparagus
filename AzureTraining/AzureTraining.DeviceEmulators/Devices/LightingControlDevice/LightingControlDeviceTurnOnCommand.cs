using AzureTraining.DeviceEmulators.Abstractions.Command;

namespace AzureTraining.DeviceEmulators.Devices.LightingControlDevice
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
