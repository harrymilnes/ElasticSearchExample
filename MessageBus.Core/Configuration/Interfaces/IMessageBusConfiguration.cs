namespace MessageBus.Core.Configuration.Interfaces
{
    public interface IMessageBusConfiguration
    {
        public string Hostname { get; }
        public ushort Port { get; }
        public string VirtualHost { get; }
        public string SearchRecordQueueName { get; }
    }
}