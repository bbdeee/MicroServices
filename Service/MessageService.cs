using System.Text;
using System.Text.Json;
using MicroServicesProject.Data;
using MicroServicesProject.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class MessageService : IHostedService
{
    private IConnection connection;
    private IModel channel;
    private IServiceProvider provider;
    private HttpClient httpClient;

    public MessageService(IServiceProvider provider)
    {
        this.provider = provider; 
    }

    public void Connect()
    {
        var factory = new ConnectionFactory { HostName = "10.104.114.61", Port = 5672 };
        connection = factory.CreateConnection();
        channel = connection.CreateModel();
        channel.ExchangeDeclare("logging", ExchangeType.Fanout);
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Connect();
        ListenForProfileCreations();  
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        channel.Close();
        connection.Close();
        return Task.CompletedTask;
    }

    void ListenForProfileCreations()
    {
        channel.ExchangeDeclare(exchange: "create-user", type: ExchangeType.Fanout);

        var queueName = channel.QueueDeclare("user-queue", true, false, false);
        channel.QueueBind(queue: queueName, exchange: "create-user", routingKey: string.Empty);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var json = Encoding.UTF8.GetString(body);

            try
            {
                var profile = JsonSerializer.Deserialize<Profile>(json);
                Console.WriteLine("Received profile for user: " + profile.Username);

                using (var scope = provider.CreateScope())
                {
                    var profileSaver = scope.ServiceProvider.GetRequiredService<ProfileService>();
                    profileSaver.SaveProfile(profile);  
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Error processing profile: " + e.ToString());
            }
        };

        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
    }
    public void SendLoggingActions(string action)
    {
        var message = Encoding.UTF8.GetBytes(action);
        channel.BasicPublish("logging", string.Empty, null, message);
    }
}
