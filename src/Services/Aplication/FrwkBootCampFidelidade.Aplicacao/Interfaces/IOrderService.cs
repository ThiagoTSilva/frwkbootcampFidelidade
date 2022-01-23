using FrwkBootCampFidelidade.DTO.OrderContext;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Aplicacao.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDTO> Add(OrderDTO orderDTO);
    }
}
