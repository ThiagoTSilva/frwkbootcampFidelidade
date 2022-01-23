using AutoMapper;
using FrwkBootCampFidelidade.Aplicacao.Interfaces;
using FrwkBootCampFidelidade.DTO.OrderContext;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _OrderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService OrderService, IMapper mapper)
        {
            _OrderService = OrderService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> AddOrder([FromBody] OrderDTO orderDTO)
        {
            if (orderDTO == null) return BadRequest();

            try
            {                   
                await _OrderService.Add(orderDTO);
                return Ok(orderDTO);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
