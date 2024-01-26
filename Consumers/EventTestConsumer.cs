using Events;
using RabbitLibrary;

namespace Consumers
{
    public class EventTestConsumer : IConsumer<EventTest>
    {
        public void Consume(string sender, object data)
        {
            Console.WriteLine($"EventTestConsumer received {sender} {data}");
        }
    }
}
