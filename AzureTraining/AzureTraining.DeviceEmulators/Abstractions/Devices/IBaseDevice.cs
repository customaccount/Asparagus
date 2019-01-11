﻿using System.Collections.Generic;
using AzureTraining.DeviceEmulators.Abstractions.Command;
using AzureTraining.DeviceEmulators.Devices.Model;
using AzureTraining.DeviceEmulators.Enum;

namespace AzureTraining.DeviceEmulators.Abstractions.Devices
{
    public interface IBaseDevice
    {
        /// <summary>
        /// Gets device id
        /// </summary>
        string Id { get; }
        
        /// <summary>
        /// Gets/sets device name;
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets hub id
        /// </summary>
        string HubId { get; }

        /// <summary>
        /// Registers device in the hub
        /// </summary>
        void Register(string hubId);

        /// <summary>
        /// Returns device state
        /// </summary>
        DeviceState GetDeviceState();

        /// <summary>
        /// Sets device state
        /// </summary>
        void SetDeviceState(DeviceState deviceState);

        /// <summary>
        /// Reboots device
        /// </summary>
        void Reboot();

        /// <summary>
        /// Updates device parameters
        /// </summary>
        void UpdateParams(params string[] arr);

        /// <summary>
        /// Returns device parameters
        /// </summary>
        string GetParams(params string[] arr);

        /// <summary>
        /// Returns list of special device's commands
        /// </summary>
        IEnumerable<ISpecialDeviceCommand> GetSpecialDeviceCommands();

        /// <summary>
        /// Returns device item entity with device state
        /// </summary>
        DeviceItem GetDeviceItem();
    }
}
