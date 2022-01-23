using FrwkBootCampFidelidade.Aplicacao.Constants;
using FrwkBootCampFidelidade.Aplicacao.Interfaces.RpcService;
using FrwkBootCampFidelidade.Dominio.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Web.BootCampFidelidade.HttpAggregator.Models.DTO;

namespace Web.BootCampFidelidade.HttpAggregator.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BonificationController : ControllerBase
    {
        private readonly IRpcClientService service;

        public BonificationController(IRpcClientService service)
        {
            this.service = service;
        }

        [HttpGet("GetByUserId")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BonificationDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByUserId([FromQuery(Name = "userId")][Required] int id)
        {

            var message = new MessageInputModel(DomainConstant.BONIFICATION, MethodConstant.GETBYUSERID, id.ToString());

            var response = await service.Call(message);
            service.Close();

            if (response.Equals(""))
                return NotFound("");

            var bonifications = JsonConvert.DeserializeObject<IEnumerable<BonificationDTO>>(response);

            return Ok(new { bonifications });
        }

        [HttpGet("GetByCPF")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BonificationDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByCPF([FromQuery(Name = "cpf")][Required] string cpf)
        {
            var message = new MessageInputModel(DomainConstant.BONIFICATION, MethodConstant.GETBYCPF, cpf);

            var response = await service.Call(message);
            service.Close();

            var bonifications = JsonConvert.DeserializeObject<IEnumerable<BonificationDTO>>(response);

            return Ok(new { bonifications });
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BonificationDTO), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] BonificationDTO bonificationDTO)
        {
            var message = new MessageInputModel(
                DomainConstant.BONIFICATION, 
                MethodConstant.POST,
                JsonConvert.SerializeObject(bonificationDTO));

            var response = await service.Call(message);
            service.Close();

            var bonification = JsonConvert.DeserializeObject<BonificationDTO>(response);

            return Created($"{Request.Path}/{bonification.Id}", bonification);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BonificationDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var message = new MessageInputModel(DomainConstant.BONIFICATION, MethodConstant.GETBYCPF, string.Empty);

            var response = await service.Call(message);
            service.Close();

            if (response.Equals(""))
                return NotFound("");

            var bonifications = JsonConvert.DeserializeObject<List<BonificationDTO>>(response);

            return Ok(new { bonifications });

        }
    }
}
