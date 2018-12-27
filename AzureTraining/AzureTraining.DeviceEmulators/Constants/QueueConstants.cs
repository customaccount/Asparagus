using System;
using System.Collections.Generic;
using System.Text;

namespace AzureTraining.DeviceEmulators.Constants
{
    public static class QueueConstants
    {
        public static class Hub
        {
            public const string QueueRegister = "hub.register";
            public const string QueueDeviceState = "hub.deviceState";
            public const string Exchange = "";
        }

        public static class WebApi
        {
            public const string RouteKeyDeviceState = "webApi.deviceState";
            public const string Exchange = "";
        }

        public static class Device
        {
            public const string QueueRegister = "device.register";
            public const string QueueDeviceState = "device.deviceState";
        }
    }
}
