using System.Collections.Generic;
using Asparagus.DeviceEmulators.Abstractions.Command;
using Asparagus.DeviceEmulators.Abstractions.Devices;
using Asparagus.DeviceEmulators.Abstractions.ServiceInterfaces;

namespace Asparagus.DeviceEmulators.Devices.HumidifierControlDevice
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
            LogCommand(nameof(Command1));
        }
    }
}
