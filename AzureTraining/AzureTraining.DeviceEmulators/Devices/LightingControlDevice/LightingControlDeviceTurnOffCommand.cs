﻿using AzureTraining.DeviceEmulators.Abstractions.Command;

namespace AzureTraining.DeviceEmulators.Devices.LightingControlDevice
{
    public class LightingControlDeviceTurnOffCommand : ISpecialDeviceCommand
    {
        private readonly LightingControlDevice _device;

        public LightingControlDeviceTurnOffCommand(LightingControlDevice device)
        {
            _device = device;
        }

        /// <inheritdoc />
        public void Execute() => _device.TurnOff();
    }
}
