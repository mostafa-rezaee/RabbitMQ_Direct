

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

string queueName = "testQueue1";
var connectionFactory = new ConnectionFactory()
{
    HostName = "localhost",
    UserName = "guest",
    Password = "guest"
};
IConnection connection = await connectionFactory.CreateConnectionAsync();
IChannel channel = await connection.CreateChannelAsync();
await channel.QueueDeclareAsync(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

var consumer = new AsyncEventingBasicConsumer(channel);
consumer.ReceivedAsync += (sender, args) => { 
    var body = args.Body.ToArray();
    var result = Encoding.UTF8.GetString(body);
    Console.WriteLine("Your number is: " + result);
    return Task.CompletedTask;
};

await channel.BasicConsumeAsync(queue: queueName, autoAck: true, consumer: consumer);
Console.ReadLine();

