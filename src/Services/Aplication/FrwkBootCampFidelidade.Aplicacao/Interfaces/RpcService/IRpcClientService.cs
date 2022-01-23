using FrwkBootCampFidelidade.Dominio.Base;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Aplicacao.Interfaces.RpcService
{
    public interface IRpcClientService
    {
        Task<string> Call(MessageInputModel message);
        void Close();
    }
}
