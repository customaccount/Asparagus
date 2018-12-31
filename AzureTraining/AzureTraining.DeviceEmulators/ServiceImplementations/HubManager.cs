using System.Threading.Tasks;
using AzureTraining.DeviceEmulators.Abstractions;
using AzureTraining.DeviceEmulators.Abstractions.ServiceInterfaces;
using AzureTraining.DeviceEmulators.Devices.Model;

namespace AzureTraining.DeviceEmulators.ServiceImplementations
{
     public class HubManager : IHubManager
    {
        private readonly IRepository<HubItem> _hubRepository;

        public HubManager(IRepository<HubItem> hubRepository)
        {
            _hubRepository = hubRepository;
        }

        /// <inheritdoc />
        public async Task CreateHubAsync(HubItem hubItem)
        {
            await _hubRepository.CreateItemAsync(hubItem);
        }

        /// <inheritdoc />
        public async Task<HubItem> GetHubItemAsync(string hubId)
        {
            return await _hubRepository.GetItemAsync(hubId);
        }

        /// <inheritdoc />
        public async Task UpdateHubAsync(HubItem hubItem)
        {
            await _hubRepository.UpdateItemAsync(hubItem.HubId, hubItem);
        }
    }
}
