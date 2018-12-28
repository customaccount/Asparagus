using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AzureTraining.DeviceEmulators.Abstractions;
using AzureTraining.DeviceEmulators.Abstractions.ServiceInterfaces;
using AzureTraining.DeviceEmulators.Devices.Model;

namespace AzureTraining.DeviceEmulators.ServiceImplementations
{
     public class HubManager : IHubManager
    {
        private readonly IRepository<IHub> _hubRepository;

        public Task UpdateHubAsync(HubItem hubItem)
        {
            throw new NotImplementedException();
        }
    }
}
