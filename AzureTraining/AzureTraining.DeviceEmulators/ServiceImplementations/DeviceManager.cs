using System.Collections.Generic;
using System.Threading.Tasks;
using AzureTraining.DeviceEmulators.Abstractions;
using AzureTraining.DeviceEmulators.Abstractions.ServiceInterfaces;
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

        /// <inheritdoc />
        public async Task<IEnumerable<DeviceItem>> GetRegisteredDevicesAsync(string hubId)
        {
            var result = await _deviceRepository.GetItemsAsync(
                device => device.HubId == hubId && device.State == DeviceState.Registered);

            return result;
        }

        /// <inheritdoc />
        public async Task CreateDeviceAsync(DeviceItem deviceItem)
        {
            await _deviceRepository.CreateItemAsync(deviceItem);
        }

        /// <inheritdoc />
        public async Task<DeviceItem> GetDeviceItemAsync(string deviceId)
        {
            return await _deviceRepository.GetItemAsync(deviceId);
        }

        /// <inheritdoc />
        public async Task<DeviceState> GetDeviceStateAsync(string deviceId)
        {
            var result = await GetDeviceItemAsync(deviceId);

            return result.State;
        }

        /// <inheritdoc />
        public async Task UpdateDeviceAsync(DeviceItem deviceItem)
        {
            await _deviceRepository.UpdateItemAsync(deviceItem.DeviceId, deviceItem);
        }
    }
}
