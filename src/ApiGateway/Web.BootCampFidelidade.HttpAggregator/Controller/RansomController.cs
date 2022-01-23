using FrwkBootCampFidelidade.Aplicacao.Constants;
using FrwkBootCampFidelidade.Aplicacao.Interfaces.RpcService;
using FrwkBootCampFidelidade.Dominio.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Web.BootCampFidelidade.HttpAggregator.Models.DTO;

namespace Web.BootCampFidelidade.HttpAggregator.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RansomController : ControllerBase
    {
        private readonly IRpcClientService service;

        public RansomController(IRpcClientService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RansomDTO), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddRansom([FromBody][Required] RansomDTO ransomDTO)
        {
            var message = new MessageInputModel(DomainConstant.RANSOM, MethodConstant.POST, JsonConvert.SerializeObject(ransomDTO));

            var response = await service.Call(message);
            service.Close();

            if (response.Equals(""))
                return NotFound();

            var ransoms = JsonConvert.DeserializeObject<RansomDTO>(response);

            return Created($"{Request.Path}/{ransoms.Id}", ransoms);
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RansomDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var message = new MessageInputModel(DomainConstant.RANSOM, MethodConstant.GET, string.Empty);

            var response = await service.Call(message);
            service.Close();

            if (response.Equals(""))
                return NotFound();

            var ransoms = JsonConvert.DeserializeObject<RansomDTO>(response);

            return Ok(new { ransoms });
        }

        [HttpGet("GetByCPF")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RansomDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByCPF([FromQuery(Name = "cpf")][Required] string cpf)
        {
            var message = new MessageInputModel(DomainConstant.RANSOM, MethodConstant.GETBYCPF, cpf);

            var response = await service.Call(message);
            service.Close();

            if (response.Equals(""))
                return NotFound();

            var ransoms = JsonConvert.DeserializeObject<RansomDTO>(response);

            return Ok(new { ransoms });
        }
    }
}
