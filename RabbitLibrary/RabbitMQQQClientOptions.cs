namespace RabbitLibrary;

// This class represents the options for configuring a RabbitMQ client.

public class RabbitMQQQClientOptions
{
// Gets or sets the host name of the RabbitMQ server.
public string HostName { get; set; }

// Gets or sets the port of the RabbitMQ server.
public int Port { get; set; }

// Gets or sets the username for authenticating with the RabbitMQ server.
public string UserName { get; set; }

// Gets or sets the password for authenticating with the RabbitMQ server.
public string Password { get; set; }

// Gets or sets the virtual host for connecting to the RabbitMQ server.
public string VirtualHost { get; set; }

// Gets or sets the virtual port for connecting to the RabbitMQ server.
public string VirtualPort { get; set; }


}