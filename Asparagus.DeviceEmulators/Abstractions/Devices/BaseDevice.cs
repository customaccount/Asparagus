using System;
using System.Collections.Generic;
using Asparagus.DeviceEmulators.Abstractions.Command;
using Asparagus.DeviceEmulators.Abstractions.ServiceInterfaces;
using Asparagus.DeviceEmulators.Enum;

namespace Asparagus.DeviceEmulators.Abstractions.Devices
{
    public abstract class BaseDevice : IBaseDevice
    {
        private DeviceState _deviceState = DeviceState.None;
        protected ILogger Logger;

        /// <inheritdoc />
        public Guid Id { get; }

        /// <inheritdoc />
        public string Name { get; set; }

        protected BaseDevice(string name, Guid id, ILogger logger)
        {
            Id = id;
            Name = name;
            Logger = logger;
        }

        protected BaseDevice(string name, ILogger logger)
            :this(name, Guid.NewGuid(), logger)
        {}

        /// <inheritdoc />
        public void Register()
        {
            _deviceState = DeviceState.Registered;
            LogMessage("Registered in the hub");
        }

        /// <inheritdoc />
        public DeviceState GetDeviceState() => _deviceState;

        /// <inheritdoc />
        public void SetDeviceState(DeviceState deviceState)
        {
            LogMessage($"Device state was changed from {_deviceState} to {deviceState}");
            _deviceState = deviceState;
        }

        /// <inheritdoc />
        public void Reboot()
        {
            _deviceState = DeviceState.Rebooted;
            LogMessage("Device was rebooted");
        }

        /// <inheritdoc />
        public abstract void UpdateParams(params string[] arr);

        /// <inheritdoc />
        public abstract IEnumerable<ISpecialDeviceCommand> GetSpecialDeviceCommands();

        protected void LogCommand(string methodName)
        {
            LogMessage($"{methodName} command executed");
        }

        private void LogMessage(string message)
        {
            Logger.Write($"Device {Name}. {message}");
        }
    }
}
