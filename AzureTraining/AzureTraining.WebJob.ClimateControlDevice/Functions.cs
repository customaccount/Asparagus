using AzureTraining.DeviceEmulators.Abstractions;
using AzureTraining.DeviceEmulators.Abstractions.Devices;
using AzureTraining.DeviceEmulators.Abstractions.Factory;
using AzureTraining.DeviceEmulators.Abstractions.ServiceInterfaces;
using AzureTraining.DeviceEmulators.Constants;
using AzureTraining.DeviceEmulators.Devices.Model;
using AzureTraining.DeviceEmulators.Enum;
using AzureTraining.DTO;
using WebJobs.Extensions.RabbitMQ.Attributes;

namespace AzureTraining.WebJob.ClimateControlDevice
{
    public class Functions
    {
        private readonly ILogger _logger;
        private readonly IDeviceManager _deviceManager;
        private readonly BaseDevice _device;

        public Functions(ILogger logger, IDeviceManager deviceManager, IDeviceFactory deviceFactory)
        {
            _logger = logger;
            _deviceManager = deviceManager;
            var deviceItem = new DeviceItem
            {
                DeviceName = "climate",
                DeviceId = "c4974901-0519-40e0-afb5-e836c77c88B8",
                HubId = "c4974901-0519-40e0-afb5-e836c77c88B9",
                Params = "test",
                State =  DeviceState.Registered
            };

            _device = deviceFactory.CreateClimateDevice(deviceItem, logger); // get settings from the config, deviceId?
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

            return null;
        }
    }
}
