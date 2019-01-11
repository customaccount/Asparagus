using System.Collections.Generic;
using System.Threading.Tasks;
using AzureTraining.DeviceEmulators.Devices.Model;
using AzureTraining.DeviceEmulators.Enum;

namespace AzureTraining.DeviceEmulators.Abstractions.ServiceInterfaces
{
    public interface IDeviceManager
    {
        /// <summary>
        /// Gets registered device state async
        /// </summary>
        Task<IEnumerable<DeviceItem>> GetRegisteredDevicesAsync(string hubId);

        /// <summary>
        /// Creates device async
        /// </summary>
        Task CreateDeviceAsync(DeviceItem deviceItem);

        /// <summary>
        /// Gets device item with device item async
        /// </summary>
        Task<DeviceItem> GetDeviceItemAsync(string deviceId);

        /// <summary>
        /// Gets device state async
        /// </summary>
        Task<DeviceState> GetDeviceStateAsync(string deviceId);

        /// <summary>
        ///  Updates device async
        /// </summary>
        Task UpdateDeviceAsync(DeviceItem deviceItem);
    }
}
