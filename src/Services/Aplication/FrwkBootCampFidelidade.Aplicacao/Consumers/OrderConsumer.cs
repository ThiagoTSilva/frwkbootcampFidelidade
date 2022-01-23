using Confluent.Kafka;
using FrwkBootCampFidelidade.Aplicacao.Configuration;
using FrwkBootCampFidelidade.Aplicacao.Constants;
using FrwkBootCampFidelidade.Aplicacao.Interfaces;
using FrwkBootCampFidelidade.Dominio.Base;
using FrwkBootCampFidelidade.DTO.OrderContext;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Aplicacao.Consumers
{
    public class OrderConsumer : BackgroundService
    {
        private readonly KafkaConfiguration _config;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly IServiceProvider _serviceProvider;
        private readonly string _topicName;
        private readonly ConsumerConfig _consumerConfig;

        public OrderConsumer(IOptions<KafkaConfiguration> option, IServiceProvider serviceProvider)
        {
            _config = option.Value;
            _serviceProvider = serviceProvider;
            _topicName = "frwkBootcampFidelidadeOrder";

            _consumerConfig = new ConsumerConfig
            {
                BootstrapServers = _config.Host,
                GroupId = $"{_topicName}-group-0",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _cancellationTokenSource = new CancellationTokenSource();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var task = Task.Run(() => ProcessQueue(stoppingToken), stoppingToken);

            return task;
        }

        private void ProcessQueue(CancellationToken stoppingToken)
        {
            using (var consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build())
            {
                consumer.Subscribe(_topicName);

                try
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        try
                        {
                            var consumeResult = consumer.Consume(stoppingToken);
 
                            Task.Run(async () => { await InvokeService(consumeResult); }, stoppingToken);
                        }
                        catch (ConsumeException ex)
                        { }
                    }
                }
                catch (OperationCanceledException ex)
                {
                    consumer.Close();
                }
            }
        }

        private async Task InvokeService(ConsumeResult<Ignore, string> message)
        {
            var mensagem = JsonConvert.DeserializeObject<MessageInputModel>(message.Message.Value);

            using var scope = _serviceProvider.CreateScope();
            var _orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();

            switch (mensagem.Method)
            {
                case MethodConstant.POST:
                    await _orderService.Add(JsonConvert.DeserializeObject<OrderDTO>(mensagem.Content));
                    break;
            }
        }
    }
}
