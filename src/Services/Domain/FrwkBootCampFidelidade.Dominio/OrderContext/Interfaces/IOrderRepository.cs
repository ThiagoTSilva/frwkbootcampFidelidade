using FrwkBootCampFidelidade.Dominio.Base.Interfaces;
using FrwkBootCampFidelidade.Dominio.OrderContext.Entities;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Dominio.OrderContext.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<Order> AddRange(Order obj);
    }
}
