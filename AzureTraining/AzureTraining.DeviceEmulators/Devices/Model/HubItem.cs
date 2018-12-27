using System.Collections.Generic;
using Newtonsoft.Json;

namespace AzureTraining.DeviceEmulators.Devices.Model
{
    public class HubItem
    {
        [JsonProperty(PropertyName = "id")]
        public string HubId { get; set; }

        [JsonProperty(PropertyName = "params")]
        public string Params { get; set; }

        [JsonProperty(PropertyName = "registeredDevicesId")]
        public List<string> RegisteredDevicesId { get; set; } = new List<string>();
    }
}
