using System.Collections.Generic;
using AzureTraining.DeviceEmulators.Abstractions.Command;
using AzureTraining.DeviceEmulators.Abstractions.Devices;
using AzureTraining.DeviceEmulators.Abstractions.ServiceInterfaces;

namespace AzureTraining.DeviceEmulators.Devices.HumidifierControlDevice
{
    public class HumidifierControlDevice : BaseDevice
    {
        public HumidifierControlDevice(string name, ILogger logger)
            : base(name, logger)
        {}

        /// <inheritdoc />
        public override IEnumerable<ISpecialDeviceCommand> GetSpecialDeviceCommands()
        {
            var commands = new List<ISpecialDeviceCommand>
            {
                new HumidifierControlDeviceSpecialCommand1(this)
            };

            return commands;
        }

        /// <summary>
        /// Device special command1
        /// </summary>
        public void Command1()
        {
            LogCommand($"{nameof(Command1)} executed");
        }
    }
}
