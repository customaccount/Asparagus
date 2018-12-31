using AzureTraining.DeviceEmulators.Abstractions.Devices;
using AzureTraining.DeviceEmulators.Abstractions.Factory;
using AzureTraining.DeviceEmulators.Abstractions.ServiceInterfaces;
using AzureTraining.DeviceEmulators.Constants;
using AzureTraining.DeviceEmulators.Devices.Model;
using AzureTraining.DTO;
using WebJobs.Extensions.RabbitMQ.Attributes;

namespace AzureTraining.WebJob.Hub
{
    public class Functions
    {
        private readonly IHubManager _hubManager;
        private readonly IDeviceFactory _deviceFactory;
        private readonly ILogger _logger;
        private BaseHub _hub;

        public Functions(ILogger logger, IHubManager hubManager, IDeviceFactory deviceFactory)
        {
            _logger = logger;
            _hubManager = hubManager;
            _deviceFactory = deviceFactory;
            
            //TODO gets this values from the config
            var hubItem = new HubItem
            {
                HubId = "c4974901-0519-40e0-afb5-e836c77c88B9",
                Params = string.Empty
            };

            CreateHub(hubItem);
        }

        /// <summary>
        /// Triggered when received the message for register new device in the hub register queue.
        /// If deviceId hasn't registered yet, pushes new message to the device register queue,
        /// </summary>
        /// <returns>if the conditions don't match, returns null and doesn't push new message to the queue. </returns>
        [return: RabbitMessage(QueueConstants.ExchangeDirect, QueueConstants.Device.QueueRegister)]
        public RegisterDeviceDto RegisterDevice(
            [RabbitQueueBinder(QueueConstants.ExchangeDirect, QueueConstants.Hub.QueueRegister)]
            [RabbitQueueTrigger(QueueConstants.Hub.QueueRegister)]
            RegisterDeviceDto registerDeviceDto)
        {
            if (registerDeviceDto.HubId == _hub.Id && !IsDeviceRegistered(registerDeviceDto.DeviceId))
            {
                _hub.RegisterDevice(registerDeviceDto.DeviceId);
                _hubManager.UpdateHubAsync(_hub.GetHubItem());
                _logger.Write(
                    $"Device with {_hub.Id} registered in the hub with id = {registerDeviceDto.HubId}");

                return registerDeviceDto;
            }

            _logger.Write($"Triggered {nameof(RegisterDevice)}. Conditions didn't match");

            return null;
        }

        /// <summary>
        /// Triggered when received request for getting device state in the hub queue
        /// </summary>
        /// <returns>if the conditions don't match, returns null and doesn't push new message to the queue. </returns>
        [return: RabbitMessage(QueueConstants.ExchangeDirect, QueueConstants.Device.QueueDeviceState)]
        public string GetDeviceState(
            [RabbitQueueBinder(QueueConstants.ExchangeDirect, QueueConstants.Hub.QueueDeviceState)]
            [RabbitQueueTrigger(QueueConstants.Hub.QueueDeviceState)]
            DeviceStateDto deviceStateDto)
        {
            if (deviceStateDto.HubId == _hub.Id && IsDeviceRegistered(deviceStateDto.DeviceId))
            {
                _logger.Write($"WebJob Hub. Triggered {nameof(GetDeviceState)}");

                return deviceStateDto.DeviceId;
            }

            _logger.Write($"Triggered {nameof(GetDeviceState)}. Conditions didn't match");

            return null;
        }

        /// <summary>
        /// Triggered when received response with device state. Queues response to the web api queue
        /// </summary>
        /// <returns>if the conditions don't match, returns null and doesn't push new message to the queue. </returns>
        [return: RabbitMessage(QueueConstants.ExchangeDirect, QueueConstants.WebApi.DeviceState)]
        public DeviceStateDto SendDeviceState(
            [RabbitQueueBinder(QueueConstants.ExchangeDirect, QueueConstants.Hub.QueueDeviceState)]
            [RabbitQueueTrigger(QueueConstants.Hub.QueueDeviceState)]
            DeviceStateDto deviceStateDto)
        {
            if (deviceStateDto.HubId == _hub.Id && IsDeviceRegistered(deviceStateDto.DeviceId))
            {
                _logger.Write($"WebJob Hub. Triggered {nameof(SendDeviceState)}");

                return deviceStateDto;
            }

            _logger.Write($"Triggered {nameof(SendDeviceState)}. Conditions didn't match");

            return null;
        }

        private bool IsDeviceRegistered(string id) => _hub.GetHubItem().RegisteredDevicesId.Contains(id);

        private void CreateHub(HubItem hubItem)
        {
            //gets state of hub from the storage(CosmosDB);
            var result = _hubManager.GetHubItemAsync(hubItem.HubId).Result;

            if (result == null)
            {
                //creates a new hub
                _hubManager.CreateHubAsync(hubItem).Wait();
                _hub = _deviceFactory.CreateHub(hubItem, _logger);
            }
            else
            {
                //restores hub
                _hub = _deviceFactory.CreateHub(result, _logger);
            }
        }
    }
}
