using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WebApi.Domain.Colaborador;

namespace WebApi.Infra.Subscribers;

public class NotifyOnboardingColaboradorSubscriber : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private const string Queue = "sales-service/customer-created";
    public NotifyOnboardingColaboradorSubscriber()
    {
        var connectionFactory = new ConnectionFactory
        {
            HostName = "localhost"
        };

        _connection = connectionFactory.CreateConnection("sales-service-customer-created-consumer");

        _channel = _connection.CreateModel();
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (sender, eventArgs) =>
        {
            var contentArray = eventArgs.Body.ToArray();
            var contentString = Encoding.UTF8.GetString(contentArray);
            var colaborador = JsonConvert.DeserializeObject<ColaboradorResponseModel>(contentString);

            Console.WriteLine($"Novo Colaborador cadastrado - Mat: {colaborador.Matricula} - {colaborador.Nome} - {colaborador.Cargo}");

            _channel.BasicAck(eventArgs.DeliveryTag, false);
        };

        _channel.BasicConsume(Queue, false, consumer);

        return Task.CompletedTask;
    }
}