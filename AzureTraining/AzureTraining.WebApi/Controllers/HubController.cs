using AzureTraining.DeviceEmulators.Abstractions.ServiceInterfaces;
using AzureTraining.DeviceEmulators.Constants;
using AzureTraining.DeviceEmulators.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AzureTraining.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HubController : ControllerBase
    {
        private readonly IQueueManager _queueManager;

        public HubController(IQueueManager queueManager)
        {
            _queueManager = queueManager;
        }
        
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get([FromQuery] DeviceStateDto deviceStateDto)
        {
            var result = _queueManager.QueueMessage(QueueConstants.Hub.QueueDeviceState, 
                QueueConstants.WebApi.DeviceState,
                QueueConstants.ExchangeDirect, 
                QueueConstants.Hub.QueueDeviceState, 
                deviceStateDto);

            return $@"Device with id => {deviceStateDto.DeviceId} 
                      has state => {result?.State.ToString() ?? string.Empty}";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] RegisterDeviceDto registerDeviceDto)
        {
            _queueManager.QueueMessage(QueueConstants.Hub.QueueRegister, 
                QueueConstants.WebApi.QueueRegister,
                QueueConstants.ExchangeDirect, 
                QueueConstants.Hub.QueueRegister, 
                registerDeviceDto);
        }
    }
}