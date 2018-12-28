﻿using AzureTraining.DeviceEmulators.Abstractions.Devices;
using AzureTraining.DeviceEmulators.Abstractions.Factory;
using AzureTraining.DeviceEmulators.Abstractions.ServiceInterfaces;
using AzureTraining.DeviceEmulators.Constants;
using AzureTraining.DTO;
using WebJobs.Extensions.RabbitMQ.Attributes;

namespace AzureTraining.WebJob.Hub
{
    public class Functions
    {
        private readonly IHubManager _hubManager;
        private readonly ILogger _logger;
        private readonly BaseHub _hub;

        public Functions(ILogger logger, IHubManager hubManager, IDeviceFactory deviceFactory)
        {
            _logger = logger;
            _hubManager = hubManager;
            _hub = deviceFactory.CreateHub(logger);
        }

        public void Register(
            [RabbitQueueBinder(QueueConstants.ExchangeDirect, QueueConstants.Device.QueueRegister)]
            [RabbitQueueTrigger(QueueConstants.Device.QueueRegister)] RegisterDeviceDto registerDeviceDto)
        {
            if (registerDeviceDto.HubId == _hub.Id)
            {
                _hub.RegisterDevice(registerDeviceDto.DeviceId);
                _hubManager.UpdateHubAsync(_hub.GetHubItem());
                _logger.Write(
                    $"Device with {_hub.Id} registered in the hub with id = {registerDeviceDto.HubId}");
            }
        }

        public void GetDeviceState(
            [RabbitQueueBinder(QueueConstants.ExchangeDirect, QueueConstants.Device.QueueDeviceState)]
            [RabbitQueueTrigger(QueueConstants.Device.QueueDeviceState)] string deviceId)
        {
            if (deviceId == _hub.Id)
            {
                SendDeviceState();
            }
        }

        //add queue bind attribute? 
        [return: RabbitMessage(QueueConstants.ExchangeDirect, QueueConstants.WebApi.RouteKeyDeviceState)]
        public DeviceStateDto SendDeviceState()
        {
            _logger.Write($"Device with {_hub.Id} sent");

            return new DeviceStateDto
            {
                DeviceId = _hub.Id,
                HubId = _hub.Id,
            };
        }
    }
}
