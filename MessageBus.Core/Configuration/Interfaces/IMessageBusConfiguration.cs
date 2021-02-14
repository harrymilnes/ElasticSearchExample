namespace MessageBus.Core.Configuration.Interfaces
{
    public interface IMessageBusConfiguration
    {
        public string Hostname { get; }
        public int Port { get; }
    }
}