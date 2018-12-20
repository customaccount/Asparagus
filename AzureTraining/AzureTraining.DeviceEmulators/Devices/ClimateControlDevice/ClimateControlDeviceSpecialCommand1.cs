using AzureTraining.DeviceEmulators.Abstractions.Command;

namespace AzureTraining.DeviceEmulators.Devices.ClimateControlDevice
{
    public class ClimateControlDeviceSpecialCommand1 : ISpecialDeviceCommand
    {
        private readonly AzureTraining.DeviceEmulators.Devices.ClimateControlDevice.ClimateControlDevice _device;

        public ClimateControlDeviceSpecialCommand1(AzureTraining.DeviceEmulators.Devices.ClimateControlDevice.ClimateControlDevice device)
        {
            _device = device;
        }

        /// <inheritdoc />
        public void Execute() => _device.Command1();
    }
}
