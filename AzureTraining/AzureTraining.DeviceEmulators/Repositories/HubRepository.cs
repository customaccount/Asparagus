using AzureTraining.DeviceEmulators.Abstractions;
using AzureTraining.DeviceEmulators.Devices.Model;

namespace AzureTraining.DeviceEmulators.Repositories
{
    public class HubRepository<T> : BaseRepository<T> where T : HubItem
    {
        private const string CollectionId = "Hubs";

        public HubRepository() : base(CollectionId)
        {}
    }
}
