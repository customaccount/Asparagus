using AzureTraining.DeviceEmulators.Abstractions.Devices;
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
            _hub = deviceFactory.CreateHub("c4974901-0519-40e0-afb5-e836c77c88B9", logger);
            _hub.RegisterDevice("c4974901-0519-40e0-afb5-e836c77c88B8");//TODO remove
        }


        /// <summary>
        /// Triggered when received the message for register new device in the WebApi register queue.
        /// If deviceId hasn't registered yet, pushes new message to the device register queue,
        /// </summary>
        /// <returns>if the conditions don't match, returns null and doesn't push new message to the queue. </returns>
        [return: RabbitMessage(QueueConstants.ExchangeDirect, QueueConstants.Device.QueueDeviceState)]
        public RegisterDeviceDto RegisterDevice(
            [RabbitQueueBinder(QueueConstants.ExchangeDirect, QueueConstants.WebApi.QueueRegister)]
            [RabbitQueueTrigger(QueueConstants.WebApi.QueueRegister)] RegisterDeviceDto registerDeviceDto)
        {
            if (registerDeviceDto.HubId == _hub.Id && !IsDeviceRegistered(registerDeviceDto.DeviceId))
            {
                _hub.RegisterDevice(registerDeviceDto.DeviceId);
                _hubManager.UpdateHubAsync(_hub.GetHubItem());
                _logger.Write(
                    $"Device with {_hub.Id} registered in the hub with id = {registerDeviceDto.HubId}");

                return registerDeviceDto;
            }

            return null;
        }

        /// <summary>
        /// Triggered when received request for getting device state in the hub queue
        /// </summary>
        /// <returns>if the conditions don't match, returns null and doesn't push new message to the queue. </returns>
        [return: RabbitMessage(QueueConstants.ExchangeDirect, QueueConstants.Device.QueueDeviceState)]
        public string GetDeviceState(
            [RabbitQueueBinder(QueueConstants.ExchangeDirect, QueueConstants.Hub.QueueDeviceState)]
            [RabbitQueueTrigger(QueueConstants.Hub.QueueDeviceState)] DeviceStateDto deviceStateDto)
        {
            if (deviceStateDto.HubId == _hub.Id && IsDeviceRegistered(deviceStateDto.DeviceId))
            {
                _logger.Write($"Triggered {nameof(GetDeviceState)}");

                return deviceStateDto.DeviceId;
            }

            return null;
        }


        /// <summary>
        /// Triggered when received response with device state. Queues response to the web api queue
        /// </summary>
        /// <returns>if the conditions don't match, returns null and doesn't push new message to the queue. </returns>
        [return: RabbitMessage(QueueConstants.ExchangeDirect, QueueConstants.WebApi.RouteKeyDeviceState)]
        public DeviceStateDto SendDeviceState(
            [RabbitQueueBinder(QueueConstants.ExchangeDirect, QueueConstants.Hub.QueueDeviceState)]
            [RabbitQueueTrigger(QueueConstants.Hub.QueueDeviceState)]
            DeviceStateDto deviceStateDto)
        {
            if (deviceStateDto.HubId == _hub.Id && IsDeviceRegistered(deviceStateDto.DeviceId))
            {
                _logger.Write($"Triggered {nameof(SendDeviceState)}");

                return deviceStateDto;
            }

            return null;
        }

        private bool IsDeviceRegistered(string id) => _hub.GetHubItem().RegisteredDevicesId.Contains(id);
    }
}
