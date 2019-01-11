using AzureTraining.DeviceEmulators.Abstractions.Command;

namespace AzureTraining.DeviceEmulators.Devices.ClimateControlDevice
{
    public class ClimateControlDeviceSpecialCommand2 : ISpecialDeviceCommand
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
