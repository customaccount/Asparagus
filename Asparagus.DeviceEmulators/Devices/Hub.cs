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
        public void RegisterDevice(IBaseDevice device)
        {
            if (IsDeviceRegistered(device.Id))
            {
                var message = $"Device {device.Name} has already registered";
                _logger.Write(message);

                throw new ArgumentException(message);
            }

            _devices.Add(device);
            device.Register();
        }

        /// <inheritdoc />
        public void ExecuteSpecificDeviceCommands(Guid id)
        {
            var commands = GetRegisteredDevice(id).GetSpecialDeviceCommands().ToList();

            if (!commands.Any())
            {
                _logger.Write($"Device with Id: {id}. Special commands were not presented");

                return;
            }

            foreach (var command in commands)
            {
                command.Execute();
            }
        }

        /// <inheritdoc />
        public void RebootDevice(Guid id)
        {
            GetRegisteredDevice(id).Reboot();
        }

        /// <inheritdoc />
        public void UpdateParams(Guid id, params string[] arr)
        {
            GetRegisteredDevice(id).UpdateParams(arr);
        }

        /// <inheritdoc />
        public DeviceState GetDeviceState(Guid id) 
        {
            return GetRegisteredDevice(id).GetDeviceState();
        }

        private bool IsDeviceRegistered(Guid id) => _devices.Exists(x => x.Id == id);

        private IBaseDevice GetRegisteredDevice(Guid id)
        {
            if (!IsDeviceRegistered(id))
            {
                var message = $"Device with Id: {id} has not registered yet";
                _logger.Write(message);

                throw new ArgumentException(message);
            }

            return _devices.Find(x => x.Id == id);
        }
    }
}