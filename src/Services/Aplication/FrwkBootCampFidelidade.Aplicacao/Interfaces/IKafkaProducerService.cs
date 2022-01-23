using FrwkBootCampFidelidade.Dominio.Base;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Aplicacao.Interfaces
{
    public interface IKafkaProducerService
    {
        Task Call(MessageInputModel message);
        Task CreateTopicMaybe(string name, int numPartitions, short replicationFactor);
    }
}
