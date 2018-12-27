using System.Collections.Generic;
using System.Threading.Tasks;
using AzureTraining.DeviceEmulators.Devices.Model;
using AzureTraining.DeviceEmulators.Enum;

namespace AzureTraining.DeviceEmulators.Abstractions
{
    public interface IDeviceManager
    {
        Task<IEnumerable<DeviceItem>> GetRegisteredDevicesAsync(string hubId);
        Task RegisterDeviceAsync(DeviceItem deviceItem);
        Task<DeviceItem> GetDeviceItemAsync(string deviceId);
        Task<DeviceState> GetDeviceStateAsync(string deviceId);
        Task UpdateDeviceAsync(DeviceItem deviceItem);
    }
}
