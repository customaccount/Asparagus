using System.Threading.Tasks;
using AzureTraining.DeviceEmulators.Devices.Model;


namespace AzureTraining.DeviceEmulators.Abstractions.ServiceInterfaces
{
    public interface IHubManager
    {
        Task UpdateHubAsync(HubItem hubItem);

        //Task<IEnumerable<HubItem>> GetRegisteredDevicesAsync(string hubId);
        //Task RegisterDeviceAsync(HubItem deviceItem);
        //Task<HubItem> GetDeviceItemAsync(string deviceId);
        //Task<DeviceState> GetDeviceStateAsync(string deviceId);
    }
}
