using RabbitMQ.Client;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
namespace RabbitLibrary
{


    public class RabbbitMQQQClient
    {
        private readonly IModel _channel;
        private readonly IServiceProvider _serviceProvider;
        public RabbbitMQQQClient(IServiceProvider serviceProvider, IConnection _connection)
        {
            _channel = _connection.CreateModel();
            _serviceProvider = serviceProvider;
        }

        public void Send(IEvent @event)
        {
            //get @event RabbitMQAttribute 
            var rabbitAttribute = @event.GetType().GetCustomAttribute<RabbitAttribute>();
            var exchangeName = rabbitAttribute.ExchangeName;
            var messageName = rabbitAttribute.MessageName;
            var route = rabbitAttribute.Route;
            var message = Newtonsoft.Json.JsonConvert.SerializeObject(@event);
            _channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, false, false, null);
            //_channel.QueueDeclare("test", false, false, false, null);
            //_channel.QueueBind("test", "demo", "routing-key", null);
            //var body = System.Text.Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchangeName, route, null, System.Text.Encoding.UTF8.GetBytes(message));
        }

        public void Consume()
        {
            //get events where RabbitMQAttribute is not null
            var events = Assembly.GetEntryAssembly().GetTypes().Where(x => x.GetCustomAttribute<RabbitAttribute>() != null).ToList();
            foreach (var @event in events)
            {
                var rabbitAttribute = @event.GetCustomAttribute<RabbitAttribute>();
                var exchangeName = rabbitAttribute.ExchangeName;
                var messageName = rabbitAttribute.MessageName;
                var route = rabbitAttribute.Route;
                _channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, false, false, null);
                _channel.QueueDeclare("test", false, false, false, null);
                _channel.QueueBind("test", exchangeName, route, null);
                var cunsumer = new RabbitMQ.Client.Events.EventingBasicConsumer(_channel);
                cunsumer.Received += (sender, e) =>
                {
                    //get ICunsumer  in ServiceProvider for @event
                    var cunsumerInstance = _serviceProvider.GetKeyedService<IConsumer>(nameof(@event));

                    var body = e.Body.ToArray();
                    var message = System.Text.Encoding.UTF8.GetString(body);
                    cunsumerInstance.Consume(e.BasicProperties.AppId, message);
                    Console.WriteLine(message);
                };
                var cunsumeTag = _channel.BasicConsume("test", false, cunsumer);
            }

        }
    }
}
