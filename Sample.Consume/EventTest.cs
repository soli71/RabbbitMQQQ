// See https://aka.ms/new-console-template for more information

using RabbitLibrary;

namespace Sample.Consume
{
    [Rabbit("TestExchange", "EventTest","test")]
    public class EventTest : IEvent
    {
        public string ID { get; set; }
    }
    };
