using System;
using System.Collections.Generic;
using Asparagus.DeviceEmulators.Abstractions.Command;
using Asparagus.DeviceEmulators.Abstractions.Devices;
using Asparagus.DeviceEmulators.Abstractions.ServiceInterfaces;

namespace Asparagus.DeviceEmulators.Devices.ClimateControlDevice
{
    public class ClimateControlDevice : BaseDevice
    {
        public ClimateControlDevice(string name, ILogger logger)
            : base(name, logger)
        {}

        /// <inheritdoc />
        public override void UpdateParams(params string[] arr)
        {
            throw new NotImplementedException();
        }

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
            LogCommand(nameof(Command1));
        }

        /// <summary>
        /// Device special command2
        /// </summary>
        public void Command2()
        {
            LogCommand(nameof(Command2));
        }
    }
}
