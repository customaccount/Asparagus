using AzureTraining.DeviceEmulators.Abstractions;
using AzureTraining.DeviceEmulators.Abstractions.Devices;
using AzureTraining.DeviceEmulators.Abstractions.Factory;
using AzureTraining.DeviceEmulators.Abstractions.ServiceInterfaces;
using AzureTraining.DeviceEmulators.Constants;
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
            _device = deviceFactory.CreateClimateDevice("climate0", logger);
        }
        
        public void Register([RabbitQueueTrigger(QueueConstants.Hub.QueueRegister)]
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

        public void GetDeviceState([RabbitQueueTrigger(QueueConstants.Hub.QueueDeviceState)] string deviceId)
        {
            if (deviceId == _device.Id)
            {
                SendDeviceState();
            }
        }

        //add queue bind attribute? 
        [return: RabbitMessage(QueueConstants.Hub.Exchange, QueueConstants.Hub.QueueDeviceState)]
        public DeviceStateDto SendDeviceState()
        {
            _logger.Write(
                $"Device with {_device.Id} sent DeviceState({_device.GetDeviceState()}) to the hub with id = {_device.HubId}");

            return new DeviceStateDto
            {
                DeviceId = _device.Id,
                HubId = _device.HubId,
                State = _device.GetDeviceState()
            };
        }

        //[return: RabbitMessage("", "hello")]
        //public DeviceItem SendQueueMessage([RabbitQueueTrigger("hello2")] string message)
        //{   
        //    //_device.Register();//remove
        //    Console.WriteLine(message);

        //    return _device.GetDeviceItem();
        //}
    }
}
