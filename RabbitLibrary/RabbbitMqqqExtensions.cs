using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
namespace RabbitLibrary
{
    public static class RabbbitMqqqExtensions
    {
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, Action<RabbitMQQQClientOptions> action)
        {
            services.Configure(action);

            var options = new RabbitMQQQClientOptions(); //get from servic
            action?.Invoke(options);

            services.AddSingleton(sp =>
            {
                var factory = new ConnectionFactory();

                factory = new ConnectionFactory()
                {
                    HostName = options.HostName,
                    Port = options.Port,
                    UserName = options.UserName,
                    Password = options.Password,
                    VirtualHost = options.VirtualHost
                };

                var connection = factory.CreateConnection();

                // Add logging for debugging
                Console.WriteLine("RabbitMQ connection created");

                return connection;
            });

            // Add logging for debugging
            Console.WriteLine("RabbitMQ service added");

            return services;
        }

        //add IConsumer<> to ServiceProvider like services.Addkeysingletone(typeof(IConsumer),"")
        public static IServiceCollection AddRabbitMQConsumers(this IServiceCollection services, Assembly assembly)
        {
            var asssTypes = assembly.GetTypes();
            var types = asssTypes.Where(x => x.GetInterfaces().Any((Type i) => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IConsumer<>)));
            //var types=from type in assembly.GetTypes()
            //where type.GetInterfaces().Any((Type i) => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IConsumer<>))
            //select type;

            foreach (var consumer in types)
            {
                services.AddKeyedSingleton(typeof(IConsumer), consumer.Name, consumer);
            }

            return services;
        }

    }
}
