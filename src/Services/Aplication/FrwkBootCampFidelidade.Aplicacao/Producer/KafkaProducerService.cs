using Confluent.Kafka;
using Confluent.Kafka.Admin;
using FrwkBootCampFidelidade.Aplicacao.Configuration;
using FrwkBootCampFidelidade.Aplicacao.Interfaces;
using FrwkBootCampFidelidade.Dominio.Base;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Aplicacao.Producer
{
    public class KafkaProducerService : IKafkaProducerService
    {
        private readonly KafkaConfiguration _config;
        private readonly string _topicName;
        private readonly ClientConfig _cloudConfig;

        public KafkaProducerService(IOptions<KafkaConfiguration> option)
        {
            _config = option.Value;
            _topicName = "frwkBootcampFidelidadeOrder";

            _cloudConfig = new ClientConfig
            {
                BootstrapServers = _config.Host
            };
        }

        public async Task Call(MessageInputModel message)
        {
            await CreateTopicMaybe(_topicName, 1, 1);

            var stringfiedMessage = JsonConvert.SerializeObject(message);

            using var producer = new ProducerBuilder<string, string>(_cloudConfig).Build();

            var key = new Guid().ToString();

            await producer.ProduceAsync(_topicName, new Message<string, string> { Key = key, Value = stringfiedMessage });

            producer.Flush(TimeSpan.FromSeconds(10));
        }

        public async Task CreateTopicMaybe(string name, int numPartitions, short replicationFactor)
        {
            using var adminClient = new AdminClientBuilder(_cloudConfig).Build();
            try
            {
                await adminClient.CreateTopicsAsync(new List<TopicSpecification> {
                        new TopicSpecification { Name = name, NumPartitions = numPartitions, ReplicationFactor = replicationFactor } });
            }
            catch (CreateTopicsException e)
            {
                
            }
        }
    }
}
