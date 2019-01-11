namespace AzureTraining.DeviceEmulators.Abstractions.ServiceInterfaces
{
    public interface ILogger
    {
        /// <summary>
        /// Writes message to the log
        /// </summary>
        void Write(string message);
    }
}
