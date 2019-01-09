using System;
using System.Collections.Generic;
using AzureTraining.DeviceEmulators.Abstractions.Command;
using AzureTraining.DeviceEmulators.Abstractions.ServiceInterfaces;
using AzureTraining.DeviceEmulators.Devices.Model;
using AzureTraining.DeviceEmulators.Enum;

namespace AzureTraining.DeviceEmulators.Abstractions.Devices
{
    public abstract class BaseDevice : IBaseDevice
    {
        private readonly DeviceItem _deviceItem;

        protected ILogger Logger;

        /// <inheritdoc />
        public string HubId => _deviceItem.HubId;

        /// <inheritdoc />
        public string Id => _deviceItem.DeviceId;

        /// <inheritdoc />
        public string Name
        {
            get => _deviceItem.DeviceName;
            set => _deviceItem.DeviceName = value;
        }

        protected BaseDevice(string name, 
            string id, 
            string deviceParams, 
            DeviceState deviceState, 
            string hubId, 
            ILogger logger)
        {
            _deviceItem = new DeviceItem
            {
                DeviceId = id,
                DeviceName = name,
                Params = deviceParams,
                HubId = hubId,
                State = deviceState
            };
            
            Logger = logger;
        }

        protected BaseDevice(string name, ILogger logger)
            :this(name, Guid.NewGuid().ToString(), string.Empty, DeviceState.None, default(Guid).ToString(), logger)
        {}

        protected BaseDevice(DeviceItem deviceItem, ILogger logger)
            :this(deviceItem.DeviceName, 
                deviceItem.DeviceId, 
                deviceItem.Params, 
                deviceItem.State, 
                deviceItem.HubId, 
                logger)
        {}

        /// <inheritdoc />
        public abstract IEnumerable<ISpecialDeviceCommand> GetSpecialDeviceCommands();

        /// <inheritdoc />
        public void Register(string hubId)
        {
            _deviceItem.State = DeviceState.Registered;
            _deviceItem.HubId = hubId;
            LogMessage("Registered in the hub");
        }

        /// <inheritdoc />
        public DeviceState GetDeviceState() => _deviceItem.State;

        /// <inheritdoc />
        public void SetDeviceState(DeviceState deviceState)
        {
            LogMessage($"Device state was changed from {_deviceItem.State} to {deviceState}");
            _deviceItem.State = deviceState;
        }

        /// <inheritdoc />
        public void Reboot()
        {
            _deviceItem.State = DeviceState.Rebooted;
            LogMessage("Device was rebooted");
        }

        /// <inheritdoc />
        public void UpdateParams(params string[] arr)
        {
            _deviceItem.Params = arr.ToString();
        }

        /// <inheritdoc />
        public string GetParams(params string[] arr) => _deviceItem.Params;

        /// <inheritdoc />
        public DeviceItem GetDeviceItem() => _deviceItem;

        protected void LogCommand(string methodName)
        {
            LogMessage($"{methodName} command executed");
        }

        private void LogMessage(string message)
        {
            Logger.Write($"Device {_deviceItem.DeviceName}. {message}");
        }
    }
}

