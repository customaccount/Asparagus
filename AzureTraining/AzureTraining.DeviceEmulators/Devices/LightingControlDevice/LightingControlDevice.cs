using System.Collections.Generic;
using AzureTraining.DeviceEmulators.Abstractions.Command;
using AzureTraining.DeviceEmulators.Abstractions.Devices;
using AzureTraining.DeviceEmulators.Abstractions.ServiceInterfaces;

namespace AzureTraining.DeviceEmulators.Devices.LightingControlDevice
{
    public class LightingControlDevice :  BaseDevice
    {
        public LightingControlDevice(string name, ILogger logger)
            : base(name, logger)
        {}

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

        /// <summary>
        /// Turn on device
        /// </summary>
        public void TurnOn()
        {
            LogCommand(nameof(TurnOn));
        }

        /// <summary>
        /// Turn off device
        /// </summary>
        public void TurnOff()
        {
            LogCommand(nameof(TurnOff));
        }
    }
}