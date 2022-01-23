using AutoMapper;
using FrwkBootCampFidelidade.Aplicacao.Interfaces;
using FrwkBootCampFidelidade.Dominio.OrderContext.Entities;
using FrwkBootCampFidelidade.Dominio.OrderContext.Interfaces;
using FrwkBootCampFidelidade.Dominio.OrderContext.Validator;
using FrwkBootCampFidelidade.DTO.OrderContext;
using System;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Aplicacao.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _OrderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository OrderRepository,  IMapper mapper)
        {
            _OrderRepository = OrderRepository;
            _mapper = mapper;
        }

        public async Task<OrderDTO> Add(OrderDTO orderDTO)
        {
            if (orderDTO == null) return null;

            var order = _mapper.Map<Order>(orderDTO);

            if (!EhValido(order)) return null;

            order.CreatedAt = DateTime.Now;
            order.UpdatedAt = DateTime.Now;

            try
            {
                await _OrderRepository.Add(order);
            }
            catch
            {
            }

            return null;
        }

        private bool EhValido(Order Order)
        {
            return new OrderValidator().Validate(Order).IsValid;
        }

    }
}
