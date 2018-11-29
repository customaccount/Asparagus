using Asparagus.DeviceEmulators.Enum;

namespace Asparagus.DeviceEmulators.Abstractions.Devices
{
    public abstract class BaseDevice : IBaseDevice
    {
        public string Name { get; set; }

        protected BaseDevice(string name)
        {
            Name = name;
        }

        public abstract void Register();
        public abstract DeviceState GetDeviceState();
        public abstract void Reboot();
        public abstract void UpdateParams(params string[] arr);
        
    }
}
