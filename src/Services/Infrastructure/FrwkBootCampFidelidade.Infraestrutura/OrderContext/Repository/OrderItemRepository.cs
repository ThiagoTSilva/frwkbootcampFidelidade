using FrwkBootCampFidelidade.Dominio.OrderContext.Entities;
using FrwkBootCampFidelidade.Dominio.OrderContext.Interfaces;
using FrwkBootCampFidelidade.Infraestrutura.Base.Repository;
using FrwkBootCampFidelidade.Infraestrutura.Context;

namespace FrwkBootCampFidelidade.Infraestrutura.OrderItemContext.Repository
{
    public class OrderItemRepository : BaseRepository<OrderItem>, IOrderItemRepository 
    {
        private readonly DBContext _context;

        public OrderItemRepository(DBContext context): base(context)
        {
            _context = context;
        }
    }
}
