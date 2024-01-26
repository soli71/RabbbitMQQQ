// See https://aka.ms/new-console-template for more information


// See https://aka.ms/new-console-template for more information

using RabbitLibrary;

namespace Events
{
    [Rabbit("TestExchange", "EventTest", "test")]
    public class EventTest : IEvent
    {
        public string ID { get; set; }
    }
};
