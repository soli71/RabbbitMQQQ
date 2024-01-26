// See https://aka.ms/new-console-template for more information

using Microsoft.AspNetCore.Builder;
using RabbitMQ.Client;
using Sample.Send;
var builder = WebApplication.CreateBuilder(args);
var build=builder.Build();
var rabbit=new RabbitLibrary.RabbbitMQQQClient(build.Services);
rabbit.Send(new EventTest() { ID="1"});
build.Run("http://localhost:1022");
