using AzureTraining.DeviceEmulators.Devices.Model;

namespace AzureTraining.DeviceEmulators.Repositories
{
    public class DeviceRepository<T> : BaseRepository<T> where T: DeviceItem
    {
        private const string CollectionId = "Devices";

        public DeviceRepository() : base(CollectionId)
        {}
    }
}
