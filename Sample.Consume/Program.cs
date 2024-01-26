// See https://aka.ms/new-console-template for more information

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RabbitLibrary;
using RabbitMQ.Client;
using System.Reflection;
var web=WebApplication.CreateBuilder(new WebApplicationOptions() { ApplicationName = Assembly.GetExecutingAssembly().GetName().Name });
web.Services.AddScoped(c => new TestScope { ID = 50 });
var builder = web.Build();
var rabbit=new RabbitLibrary.RabbbitMQQQClient(builder.Services);
rabbit.Consume();
builder.Run("http://localhost:1023");
Console.ReadLine();

