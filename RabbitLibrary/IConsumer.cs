namespace RabbitLibrary
{
    public interface IConsumer
    {
        void Consume(string sender,object data);
    }
}
