using AzureTraining.DeviceEmulators.Enum;
using Newtonsoft.Json;

namespace AzureTraining.DeviceEmulators.Devices.Model
{
    public class DeviceItem
    {
        [JsonProperty(PropertyName = "id")]
        public string DeviceId { get; set; }

        [JsonProperty(PropertyName = "deviceName")]
        public string DeviceName { get; set; }

        [JsonProperty(PropertyName = "params")]
        public string Params { get; set; }

        [JsonProperty(PropertyName = "state")]
        public DeviceState State { get; set; }

        [JsonProperty(PropertyName = "hubId")]
        public string HubId { get; set; }


    }
}
