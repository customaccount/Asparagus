using System;
using System.Collections.Generic;
using System.Text;

namespace AzureTraining.DeviceEmulators.Constants
{
    public static class QueueConstants
    {
        public const string ExchangeDirect = "amq.direct";
        public static class Hub
        {
            public const string QueueRegister = "hub.register";
            public const string QueueDeviceState = "hub.deviceState";
        }

        public static class WebApi
        {
            public const string RouteKeyDeviceState = "webApi.deviceState";
        }

        public static class Device
        {
            public const string QueueRegister = "device.register";
            public const string QueueDeviceState = "device.deviceState";
        }
    }
}
