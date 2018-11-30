using System;
using Asparagus.DeviceEmulators.Abstractions.ServiceInterfaces;
using Asparagus.DeviceEmulators.Devices;
using Asparagus.DeviceEmulators.Devices.ClimateControlDevice;
using Asparagus.DeviceEmulators.ServiceImplementations;
using Unity;
using Unity.Lifetime;

namespace Asparagus.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer()
                .RegisterType<ILogger, ConsoleLogger>(new TransientLifetimeManager());

            var logger = container.Resolve<ILogger>();
            var hub = new Hub(logger);
            var controlDevice = new ClimateControlDevice("climate0", logger);
            hub.RegisterDevice(controlDevice);
            hub.RebootDevice(controlDevice.Id);
            hub.ExecuteSpecificDeviceCommands(controlDevice.Id);

            Console.Read();
        }
    }
}
