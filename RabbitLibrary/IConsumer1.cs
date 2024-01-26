namespace RabbitLibrary
{
    public interface IConsumer<T> : IConsumer where T : IEvent
    {
    }
}
