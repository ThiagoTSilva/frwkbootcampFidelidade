using FrwkBootCampFidelidade.Dominio.OrderContext.Entities;
using FrwkBootCampFidelidade.Dominio.OrderContext.Interfaces;
using FrwkBootCampFidelidade.Infraestrutura.Base.Repository;
using FrwkBootCampFidelidade.Infraestrutura.Context;
using System;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Infraestrutura.OrderItemContext.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository 
    {
        private readonly DBContext _context;

        public OrderRepository(DBContext context): base(context)
        {
            _context = context;
        }

        public async override Task<Order> Add(Order order)
        {
            await _context.Set<Order>().AddAsync(order);
            try
            {
                var sucesso = await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }

            return order;
        }

        public async Task<Order> AddRange(Order order)
        {
            await _context.Set<Order>().AddRangeAsync(order);
            try
            {
                var sucesso = await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }

            return order;
        }

    }
}
