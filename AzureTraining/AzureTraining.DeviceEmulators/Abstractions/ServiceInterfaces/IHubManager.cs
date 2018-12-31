using System.Threading.Tasks;
using AzureTraining.DeviceEmulators.Devices.Model;


namespace AzureTraining.DeviceEmulators.Abstractions.ServiceInterfaces
{
    public interface IHubManager
    {
        /// <summary>
        /// Create hub async
        /// </summary>
        Task CreateHubAsync(HubItem hubItem);

        /// <summary>
        /// Gets hub by hubId
        /// </summary>
        Task<HubItem> GetHubItemAsync(string hubId);

        /// <summary>
        /// Updates hub async
        /// </summary>
        Task UpdateHubAsync(HubItem hubItem);
    }
}
