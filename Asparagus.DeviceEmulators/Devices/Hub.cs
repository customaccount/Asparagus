using System;
using System.Collections.Generic;
using System.Linq;
using Asparagus.DeviceEmulators.Abstractions;
using Asparagus.DeviceEmulators.Abstractions.Devices;
using Asparagus.DeviceEmulators.Abstractions.ServiceInterfaces;
using Asparagus.DeviceEmulators.Enum;

namespace Asparagus.DeviceEmulators.Devices
{
    public class Hub : IHub
    {
        private readonly List<IBaseDevice> _devices;
        private readonly ILogger _logger;

        public Hub(ILogger logger)
        {
            _devices = new List<IBaseDevice>();
            _logger = logger;
        }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public void RegisterDevice(IBaseDevice device)
        {
            if (IsDeviceRegistered(device))
            {
                var message = $"Device {device.Name} has already registered";
                _logger.Write(message);

                throw new ArgumentException(message);
            }

            _devices.Add(device);
            device.Register();
        }

        /// <inheritdoc />
        public void ExecuteSpecificDeviceCommands(IBaseDevice device)
        {
            var command = GetRegisteredDevice(device).GetSpecialDeviceCommands().ToList();

            if (!command.Any())
            {
                _logger.Write($"Device {device.Name}. Special commands were not presented");
            }

            foreach (var specialCommand in command)
            {
                specialCommand.Execute();
            }
        }

        /// <inheritdoc />
        public void RebootDevice(IBaseDevice device)
        {
            GetRegisteredDevice(device).Reboot();
        }

        /// <inheritdoc />
        public void UpdateParams(IBaseDevice device, params string[] arr)
        {
            GetRegisteredDevice(device).UpdateParams(arr);
        }

        public DeviceState GetDeviceState(IBaseDevice device) 
        {
            return GetRegisteredDevice(device).GetDeviceState();
        }

        private bool IsDeviceRegistered(IBaseDevice device) => _devices.Contains(device);

        private IBaseDevice GetRegisteredDevice(IBaseDevice device)
        {
            if (!IsDeviceRegistered(device))
            {
                var message = $"Device {device.Name} has not registered yet";
                _logger.Write(message);

                throw new ArgumentException(message);
            }

            return _devices.Find(x => x.Equals(device));
        }
    }
}