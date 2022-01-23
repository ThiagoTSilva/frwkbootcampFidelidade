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
using Web.BootCampFidelidade.HttpAggregator.Models;
using Web.BootCampFidelidade.HttpAggregator.Models.DTO;

namespace Web.BootCampFidelidade.HttpAggregator.Controller
{
    [Route("api/v1/[controller]")]
    public class ExtractController : ControllerBase
    {
        private readonly IRpcClientService service;
        public ExtractController(IRpcClientService service)
        {
            this.service = service;
        }

        [HttpGet("GetByUserId")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RansomHistoryStatusDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByUserId([FromQuery(Name = "userId")] int id)
        {
            var message = new MessageInputModel(DomainConstant.EXTRACT, MethodConstant.GETBYUSERID, id.ToString());

            var response = await service.Call(message);
            service.Close();

            if (response.Equals("")) return NotFound();

            var extracts = JsonConvert.DeserializeObject<List<ExtractDTO>>(response);

            return Ok(new { extracts });
        }

        [HttpGet("GetByCPF")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RansomHistoryStatusDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByCPF([FromQuery(Name = "cpf")] string cpf)
        {
            var message = new MessageInputModel(DomainConstant.EXTRACT, MethodConstant.GETBYCPF, cpf);

            var response = await service.Call(message);
            service.Close();

            if (response.Equals(""))
                return NotFound();

            var extracts = JsonConvert.DeserializeObject<List<RansomHistoryStatusDTO>>(response);

            return Ok(new { extracts });
        }

        [HttpGet("GetSummaryPoints")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SummaryPointsDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSummaryPoints([FromQuery(Name = "userId")][Required]int id)
        {
            var message = new MessageInputModel(DomainConstant.EXTRACT, MethodConstant.GETSUMMARYPOINTSBYUSERID, id.ToString());

            var response = await service.Call(message);
            service.Close();

            if (response.Equals(""))
                return NotFound();

            var extracts = JsonConvert.DeserializeObject<List<SummaryPointsDTO>>(response);

            return Ok(new { extracts });
        }
    }
}
