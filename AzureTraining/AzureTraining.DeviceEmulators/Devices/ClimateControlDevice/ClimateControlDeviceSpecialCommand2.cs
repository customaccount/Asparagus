using AzureTraining.DeviceEmulators.Abstractions.Command;

namespace AzureTraining.DeviceEmulators.Devices.ClimateControlDevice
{
    class ClimateControlDeviceSpecialCommand2 : ISpecialDeviceCommand
    {
        private readonly AzureTraining.DeviceEmulators.Devices.ClimateControlDevice.ClimateControlDevice _device;

        public ClimateControlDeviceSpecialCommand2(AzureTraining.DeviceEmulators.Devices.ClimateControlDevice.ClimateControlDevice device)
        {
            _device = device;
        }

        /// <inheritdoc />
        public void Execute() => _device.Command2();
    }
}
