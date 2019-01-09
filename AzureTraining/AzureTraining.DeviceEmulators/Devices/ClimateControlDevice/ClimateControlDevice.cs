using System.Collections.Generic;
using AzureTraining.DeviceEmulators.Abstractions.Command;
using AzureTraining.DeviceEmulators.Abstractions.Devices;
using AzureTraining.DeviceEmulators.Abstractions.ServiceInterfaces;
using AzureTraining.DeviceEmulators.Devices.Model;

namespace AzureTraining.DeviceEmulators.Devices.ClimateControlDevice
{
    public class ClimateControlDevice : BaseDevice
    {
        public ClimateControlDevice(string name, ILogger logger)
            : base(name, logger)
        {}

        public ClimateControlDevice(DeviceItem deviceItem, ILogger logger)
            : base(deviceItem, logger)
        {}

        /// <inheritdoc />
        public override IEnumerable<ISpecialDeviceCommand> GetSpecialDeviceCommands()
        {
            var commands = new List<ISpecialDeviceCommand>
            {
                new ClimateControlDeviceSpecialCommand1(this),
                new ClimateControlDeviceSpecialCommand2(this)
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

        /// <summary>
        /// Device special command2
        /// </summary>
        public void Command2()
        {
            LogCommand($"{nameof(Command2)} executed");
        }
    }
}
