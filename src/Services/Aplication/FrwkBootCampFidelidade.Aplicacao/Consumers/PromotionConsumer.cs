using FrwkBootCampFidelidade.Aplicacao.Configuration;
using FrwkBootCampFidelidade.Aplicacao.Constants;
using FrwkBootCampFidelidade.Aplicacao.Interfaces;
using FrwkBootCampFidelidade.Dominio.Base;
using FrwkBootCampFidelidade.DTO.PromotionContext;
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
    public class PromotionConsumer : BackgroundService
    {
        private readonly RabbitMqConfiguration _config;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IPromotionService _promotionService;

        public PromotionConsumer(IOptions<RabbitMqConfiguration> option, IPromotionService promotionService)
        {
            _config = option.Value;
            _promotionService = promotionService;

            var factory = new ConnectionFactory
            {
                HostName = _config.Host
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
                        queue: DomainConstant.PROMOTION,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

            _channel.BasicQos(0, 1, false);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            _channel.BasicConsume(queue: DomainConstant.PROMOTION, autoAck: false, consumer: consumer);

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

                    var replyMessage = await InvokeService(message);

                    response = replyMessage;
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
            dynamic response = string.Empty;

            switch (message.Method)
            {
                case MethodConstant.POST:
                    response = await _promotionService.Add(JsonConvert.DeserializeObject<PromotionCreateUpdateRemoveDTO>(message.Content));
                    break;
                case MethodConstant.GET:
                    response = await _promotionService.GetAll();
                    break;
                case MethodConstant.GETBYID:
                    response = await _promotionService.GetById(message.Content);
                    break;
                case MethodConstant.GETPROMOTIONTODAY:
                    response = await _promotionService.GetPromotionToday(JsonConvert.DeserializeObject<PromotionDTO>(message.Content));
                    break;
                case MethodConstant.GETPROMOTIONBYDATERANGE:
                    response = await _promotionService.GetPromotionByDateRange(JsonConvert.DeserializeObject<PromotionDTO>(message.Content));
                    break;
                case MethodConstant.DELETE:
                    response = await _promotionService.Remove(JsonConvert.DeserializeObject<PromotionCreateUpdateRemoveDTO>(message.Content));
                    break;
                case MethodConstant.DELETEBYID:
                    response = await _promotionService.RemoveById(message.Content);
                    break;
                case MethodConstant.PUT:
                    response = await _promotionService.Update(JsonConvert.DeserializeObject<PromotionCreateUpdateRemoveDTO>(message.Content));
                    break;
            }

            return JsonConvert.SerializeObject(response, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
    }
}
