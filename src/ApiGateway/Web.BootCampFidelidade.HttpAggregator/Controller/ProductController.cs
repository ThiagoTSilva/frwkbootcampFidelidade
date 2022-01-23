using FrwkBootCampFidelidade.Aplicacao.Constants;
using FrwkBootCampFidelidade.Aplicacao.Interfaces.RpcService;
using FrwkBootCampFidelidade.Dominio.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Web.BootCampFidelidade.HttpAggregator.Models.DTO;

namespace Web.BootCampFidelidade.HttpAggregator.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRpcClientService service;
        public ProductController(IRpcClientService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(PromotionItemDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<PromotionItemDTO>> Get()
        {
            var message = new MessageInputModel(DomainConstant.PRODUCT, MethodConstant.GET, string.Empty);

            var response = await service.Call(message);
            service.Close();

            var product = JsonSerializer.Deserialize<List<PromotionItemDTO>>(response);

            return Ok(new { product });
        }
    }
}
