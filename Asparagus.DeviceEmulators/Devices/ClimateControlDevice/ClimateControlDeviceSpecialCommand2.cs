using Asparagus.DeviceEmulators.Abstractions.Command;

namespace Asparagus.DeviceEmulators.Devices.ClimateControlDevice
{
    class ClimateControlDeviceSpecialCommand2 : ISpecialDeviceCommand
    {
        private readonly ClimateControlDevice _device;

        public ClimateControlDeviceSpecialCommand2(ClimateControlDevice device)
        {
            _device = device;
        }

        /// <inheritdoc />
        public void Execute() => _device.Command2();
    }
}
