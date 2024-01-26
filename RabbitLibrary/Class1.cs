using System;
namespace RabbitLibrary
{



    public class RabbitAttribute : Attribute
    {
        public string ExchangeName { get; set; }
        public string MessageName { get; set; }
        public string Route { get; set; }
        public RabbitAttribute(string exchangeName, string messageName,string route)
        {
            ExchangeName = exchangeName;
            MessageName = messageName;
            Route = route;
        }
    }
}
