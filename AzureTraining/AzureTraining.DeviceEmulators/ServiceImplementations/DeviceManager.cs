using System.Collections.Generic;
using System.Threading.Tasks;
using AzureTraining.DeviceEmulators.Abstractions;
using AzureTraining.DeviceEmulators.Devices.Model;
using AzureTraining.DeviceEmulators.Enum;

namespace AzureTraining.DeviceEmulators.ServiceImplementations
{
    public class DeviceManager : IDeviceManager
    {
        private readonly IRepository<DeviceItem> _deviceRepository;

        public DeviceManager(IRepository<DeviceItem> deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }
        public async Task<IEnumerable<DeviceItem>> GetRegisteredDevicesAsync(string hubId)
        {

            var result = await _deviceRepository.GetItemsAsync(
                device => device.HubId == hubId && device.State == DeviceState.Registered);

            return result;
        }

        public async Task RegisterDeviceAsync(DeviceItem deviceItem)
        {
            await _deviceRepository.CreateItemAsync(deviceItem);
        }

        public async Task<DeviceItem> GetDeviceItemAsync(string deviceId)
        {
            return await _deviceRepository.GetItemAsync(deviceId);
        }

        public async Task<DeviceState> GetDeviceStateAsync(string deviceId)
        {
            var result = await GetDeviceItemAsync(deviceId);

            return result.State;
        }

        public async Task UpdateDeviceAsync(DeviceItem deviceItem)
        {
            await _deviceRepository.UpdateItemAsync(deviceItem.DeviceId, deviceItem);
        }
    }
}
