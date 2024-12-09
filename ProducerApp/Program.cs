
using RabbitMQ.Client;
using System.Text;

Console.WriteLine("Hello, Please enter your phone number:");
var message = Console.ReadLine();
string queueName = "testQueue1";

var connectionFactory = new ConnectionFactory() { 
    HostName = "localhost",
    UserName = "guest",
    Password = "guest"
};
IConnection connection = await connectionFactory.CreateConnectionAsync();
IChannel channel = await connection.CreateChannelAsync();
await channel.QueueDeclareAsync(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);


if (message != null)
    await channel.BasicPublishAsync(exchange: String.Empty, routingKey: queueName, body: Encoding.UTF8.GetBytes(message));

Console.WriteLine(message + " sent to queue.");
Console.ReadKey();
