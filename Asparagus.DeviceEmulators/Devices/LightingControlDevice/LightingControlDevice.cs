using System;
using System.Collections.Generic;
using Asparagus.DeviceEmulators.Abstractions.Command;
using Asparagus.DeviceEmulators.Abstractions.Devices;
using Asparagus.DeviceEmulators.Abstractions.ServiceInterfaces;

namespace Asparagus.DeviceEmulators.Devices.LightingControlDevice
{
    public class LightingControlDevice :  BaseDevice
    {
        public LightingControlDevice(string name, ILogger logger)
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
                new LightingControlDeviceTurnOnCommand(this),
                new LightingControlDeviceTurnOffCommand(this),
            };

            return commands;
        }

        public void TurnOn()
        {
            LogCommand(nameof(TurnOn));
        }

        public void TurnOff()
        {
            LogCommand(nameof(TurnOff));
        }
    }
}