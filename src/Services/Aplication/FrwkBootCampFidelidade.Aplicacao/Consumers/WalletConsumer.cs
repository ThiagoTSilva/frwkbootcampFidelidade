using FrwkBootCampFidelidade.Aplicacao.Configuration;
using FrwkBootCampFidelidade.Aplicacao.Constants;
using FrwkBootCampFidelidade.Aplicacao.Interfaces;
using FrwkBootCampFidelidade.Dominio.Base;
using FrwkBootCampFidelidade.DTO.WalletContext;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Aplicacao.Consumers
{
    public class WalletConsumer : BackgroundService
    {
        private readonly RabbitMqConfiguration _config;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceProvider _serviceProvider;

        public WalletConsumer(IOptions<RabbitMqConfiguration> option, IServiceProvider serviceProvider)
        {
            _config = option.Value;
            _serviceProvider = serviceProvider;

            var factory = new ConnectionFactory
            {
                HostName = _config.Host
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
                        queue: DomainConstant.WALLET,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

            _channel.BasicQos(0, 1, false);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            _channel.BasicConsume(queue: DomainConstant.WALLET, autoAck: false, consumer: consumer);

            consumer.Received += async (model, ea) =>
            {
                string response = null;

                var body = ea.Body.ToArray();
                var props = ea.BasicProperties;
                var replyProps = _channel.CreateBasicProperties();
                replyProps.CorrelationId = props.CorrelationId;

                try
                {
                    var contentArray = ea.Body.ToArray();
                    var incommingMessage = Encoding.UTF8.GetString(contentArray);
                    var message = JsonConvert.DeserializeObject<MessageInputModel>(incommingMessage);

                    response = await InvokeService(message);
                }
                catch (Exception e)
                {
                    response = "";
                }
                finally
                {
                    var responseBytes = Encoding.UTF8.GetBytes(response);
                    _channel.BasicPublish(exchange: "", routingKey: props.ReplyTo, basicProperties: replyProps, body: responseBytes);
                    _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
            };

            return Task.CompletedTask;
        }

        private async Task<string> InvokeService(MessageInputModel message)
        {
            using var scope = _serviceProvider.CreateScope();
            var _wallet = scope.ServiceProvider.GetRequiredService<IWalletService>();

            dynamic response = string.Empty;

            switch (message.Method)
            {
                case MethodConstant.POST:
                    response = await _wallet.Add(JsonConvert.DeserializeObject<WalletDTO>(message.Content));
                    break;
                case MethodConstant.PUT:
                    await _wallet.Update(JsonConvert.DeserializeObject<WalletDTO>(message.Content));
                    response = "sucesso";
                    break;
                case MethodConstant.TRANSFER:
                    response = await _wallet.Transfer(JsonConvert.DeserializeObject<WalletTransferDTO>(message.Content));
                    break;
                case MethodConstant.GETBYUSERIDANDTYPE:
                    var walletDTO = JsonConvert.DeserializeObject<WalletDTO>(message.Content);
                    await _wallet.GetByUserIdAndType(walletDTO.UserId, walletDTO.WalletTypeId);
                    break;
                case MethodConstant.GETBYUSERID:
                    response = await _wallet.GetAllByUserId(int.Parse(message.Content));
                    break;
            }

            return JsonConvert.SerializeObject(response);
        }
    }
}
