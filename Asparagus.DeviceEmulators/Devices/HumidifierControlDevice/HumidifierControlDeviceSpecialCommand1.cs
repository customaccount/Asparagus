using Asparagus.DeviceEmulators.Abstractions.Command;

namespace Asparagus.DeviceEmulators.Devices.HumidifierControlDevice
{
    public class HumidifierControlDeviceSpecialCommand1 : ISpecialDeviceCommand
    {
        private readonly HumidifierControlDevice _device;
        public HumidifierControlDeviceSpecialCommand1(HumidifierControlDevice device)
        {
            _device = device;
        }

        /// <inheritdoc />
        public void Execute() => _device.Command1();
    }
}
