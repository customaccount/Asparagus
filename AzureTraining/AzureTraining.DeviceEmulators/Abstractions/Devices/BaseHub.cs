using System;
using System.Linq;
using AzureTraining.DeviceEmulators.Abstractions.ServiceInterfaces;
using AzureTraining.DeviceEmulators.Devices.Model;
using AzureTraining.DeviceEmulators.Enum;

namespace AzureTraining.DeviceEmulators.Abstractions.Devices
{
    public abstract class BaseHub : IHub
    {
        private readonly ILogger _logger;
        private readonly HubItem _hubItem;

        public string Id => _hubItem.HubId;

        protected BaseHub(ILogger logger)
        {
            _logger = logger;
            _hubItem = new HubItem
            {
                HubId = Guid.NewGuid().ToString()
            };
        }
        /// <inheritdoc />
        public void RegisterDevice(string deviceId)
        {
            if (IsDeviceRegistered(deviceId))
            {
                var message = $"Device with id = {deviceId} has already registered";
                _logger.Write(message);

                throw new ArgumentException(message);
            }

            _hubItem.RegisteredDevicesId.Add(deviceId);
        }

        /// <inheritdoc />
        public void ExecuteSpecificDeviceCommands(string id)
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
        public void RebootDevice(string id)
        {
            GetRegisteredDevice(id).Reboot();
        }

        /// <inheritdoc />
        public void UpdateParams(string id, params string[] arr)
        {
            GetRegisteredDevice(id).UpdateParams(arr);
        }

        /// <inheritdoc />
        public DeviceState GetDeviceState(string id) => GetRegisteredDevice(id).GetDeviceState();

        public HubItem GetHubItem() => _hubItem;

        private bool IsDeviceRegistered(string id) 
            => _hubItem.RegisteredDevicesId.Exists(devicesId => devicesId == id);

        private IBaseDevice GetRegisteredDevice(string id)
        {
            if (!IsDeviceRegistered(id))
            {
                var message = $"Device with Id: {id} has not registered yet";
                _logger.Write(message);

                throw new ArgumentException(message);
            }

            throw new NotImplementedException(); // TODO replace with the queueing command logic?
        }
    }
}
