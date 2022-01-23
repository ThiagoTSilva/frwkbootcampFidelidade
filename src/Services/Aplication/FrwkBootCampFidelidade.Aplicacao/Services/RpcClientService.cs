using FrwkBootCampFidelidade.Aplicacao.Configuration;
using FrwkBootCampFidelidade.Aplicacao.Interfaces.RpcService;
using FrwkBootCampFidelidade.Dominio.Base;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Aplicacao.Services.RpcService
{
    public class RpcClientService : IRpcClientService
    {
        private readonly ConnectionFactory _factory;
        private readonly RabbitMqConfiguration _config;
        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly string replyQueueName;
        private readonly EventingBasicConsumer consumer;
        private readonly BlockingCollection<string> respQueue = new BlockingCollection<string>();
        private readonly IBasicProperties props;

        public RpcClientService(IOptions<RabbitMqConfiguration> options)
        {
            _config = options.Value;

            _factory = new ConnectionFactory
            {
                HostName = _config.Host
            };

            connection = _factory.CreateConnection();
            channel = connection.CreateModel();
            replyQueueName = channel.QueueDeclare().QueueName;
            consumer = new EventingBasicConsumer(channel);

            props = channel.CreateBasicProperties();
            var correlationId = Guid.NewGuid().ToString();
            props.CorrelationId = correlationId;
            props.ReplyTo = replyQueueName;

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var response = Encoding.UTF8.GetString(body);
                if (ea.BasicProperties.CorrelationId == correlationId)
                {
                    respQueue.Add(response);
                }
            };

            channel.BasicConsume(
                consumer: consumer,
                queue: replyQueueName,
                autoAck: true);
        }

        public Task<string> Call(MessageInputModel message)
        {
            var stringfiedMessage = JsonConvert.SerializeObject(message);
            var messageBytes = Encoding.UTF8.GetBytes(stringfiedMessage);
            channel.BasicPublish(
                exchange: "",
                routingKey: message.Queue,
                basicProperties: props,
                body: messageBytes);

            return Task.FromResult(respQueue.Take());
        }

        public void Close()
        {
            connection.Close();
        }
    }
}
