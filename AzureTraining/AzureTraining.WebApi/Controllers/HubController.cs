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
            // hubid = c4974901-0519-40e0-afb5-e836c77c88B9
            // devclimateid = c4974901-0519-40e0-afb5-e836c77c88B8
            var result = _queueManager.QueueMessage<DeviceStateDto>(QueueConstants.Hub.QueueDeviceState, 
                QueueConstants.WebApi.DeviceState,
                QueueConstants.ExchangeDirect, 
                QueueConstants.Hub.QueueDeviceState, 
                deviceStateDto);

            return deviceStateDto?.State.ToString() ?? string.Empty;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] RegisterDeviceDto registerDeviceDto)
        {
            _queueManager.QueueMessage<RegisterDeviceDto>(QueueConstants.Hub.QueueRegister, 
                QueueConstants.WebApi.QueueRegister,
                QueueConstants.ExchangeDirect, 
                QueueConstants.Hub.QueueRegister, 
                registerDeviceDto);
        }
    }
}