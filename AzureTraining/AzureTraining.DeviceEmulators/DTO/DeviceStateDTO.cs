using AzureTraining.DeviceEmulators.Enum;

namespace AzureTraining.DeviceEmulators.DTO
{
    public class DeviceStateDto
    {
        public string DeviceId { get; set; }
        public string HubId { get; set; }
        public DeviceState State { get; set; }
    }
}
