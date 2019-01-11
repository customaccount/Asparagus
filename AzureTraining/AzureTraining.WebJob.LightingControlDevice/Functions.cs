using AzureTraining.DeviceEmulators.Abstractions.Devices;
using AzureTraining.DeviceEmulators.Abstractions.Factory;
using AzureTraining.DeviceEmulators.Abstractions.ServiceInterfaces;
using AzureTraining.DeviceEmulators.Constants;
using AzureTraining.DeviceEmulators.Devices.Model;
using AzureTraining.DeviceEmulators.DTO;
using WebJobs.Extensions.RabbitMQ.Attributes;

namespace AzureTraining.WebJob.LightingControlDevice
{
    public class Functions
    {
        private readonly ILogger _logger;
        private readonly IDeviceManager _deviceManager;
        private readonly IDeviceFactory _deviceFactory;
        private BaseDevice _device;

        public Functions(ILogger logger, IDeviceManager deviceManager, IDeviceFactory deviceFactory)
        {
            _logger = logger;
            _deviceManager = deviceManager;
            _deviceFactory = deviceFactory;

            //TODO gets this values from the config
            var deviceItem = new DeviceItem
            {
                DeviceName = "LightingDevice",
                DeviceId = "c4974901-0519-40e0-afb5-e836c77c88C8",
                Params = string.Empty
            };

            CreateDevice(deviceItem);
        }

        /// <summary>
        /// Triggered when received the message for register new device in the device register queue.
        /// </summary>
        public void Register(
            [RabbitQueueBinder(QueueConstants.ExchangeDirect, QueueConstants.Device.QueueRegister)]
            [RabbitQueueTrigger(QueueConstants.Device.QueueRegister)]
            RegisterDeviceDto registerDeviceDto)
        {
            if (registerDeviceDto.DeviceId == _device.Id)
            {
                _device.Register(registerDeviceDto.HubId);
                _deviceManager.UpdateDeviceAsync(_device.GetDeviceItem());
                _logger.Write(
                    $"Device with {_device.Id} registered in the hub with id = {registerDeviceDto.HubId}");
            }

            _logger.Write($"Triggered {nameof(Register)}. Conditions didn't match");
        }

        /// <summary>
        /// Triggered when received the message for the getting device state in the device queue.
        /// Then pushes new message to the hub queue with device state, if deviceId equal <paramref name="deviceId"/>
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns>if the conditions don't match, returns null and doesn't push new message to the queue. </returns>
        [return: RabbitMessage(QueueConstants.ExchangeDirect, QueueConstants.Hub.QueueDeviceState)]
        public DeviceStateDto GetDeviceState(
            [RabbitQueueBinder(QueueConstants.ExchangeDirect, QueueConstants.Device.QueueDeviceState)]
            [RabbitQueueTrigger(QueueConstants.Device.QueueDeviceState)] string deviceId)
        {
            if (deviceId == _device.Id)
            {
                _logger.Write($@"Device with {_device.Id} sent DeviceState({_device.GetDeviceState()}) 
                            to the hub with id = {_device.HubId}");

                return new DeviceStateDto
                {
                    DeviceId = _device.Id,
                    HubId = _device.HubId,
                    State = _device.GetDeviceState()
                };
            }

            _logger.Write($"Triggered {nameof(GetDeviceState)}. Conditions didn't match");
            
            return null;
        }

        private void CreateDevice(DeviceItem deviceItem)
        {
            //gets state of device from the storage(CosmosDB);
            var result = _deviceManager.GetDeviceItemAsync(deviceItem.DeviceId).Result;

            if (result == null)
            {
                //creates a new device
                _deviceManager.CreateDeviceAsync(deviceItem).Wait();
                _device = _deviceFactory.CreateClimateDevice(deviceItem, _logger);
            }
            else
            {
                //restores device
                _device = _deviceFactory.CreateClimateDevice(result, _logger);
            }
        }
    }
}
