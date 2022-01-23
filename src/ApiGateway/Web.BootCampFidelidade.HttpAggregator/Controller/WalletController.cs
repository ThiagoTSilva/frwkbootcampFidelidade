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
    public class WalletController : ControllerBase
    {
        private readonly IRpcClientService service;
        public WalletController(IRpcClientService service)
        {
            this.service = service;
        }

        [HttpGet("GetByUserId")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RansomDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByUserId([FromQuery(Name = "userId")][Required] int id)
        {
            var message = new MessageInputModel(DomainConstant.WALLET, MethodConstant.GETBYUSERID, id.ToString());

            var response = await service.Call(message);
            service.Close();

            if (response.Equals(""))
                return NotFound();

            var wallets = JsonConvert.DeserializeObject<List<WalletDTO>>(response);

            return Ok(new { wallets });
        }

        [HttpGet("GetByUserIdAndType")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RansomDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByUserIdAndType([FromQuery(Name = "userId")][Required] int id, [FromQuery(Name = "walletType")][Required] int type)
        {
            var message = new MessageInputModel(DomainConstant.WALLET, MethodConstant.GETBYUSERIDANDTYPE, JsonConvert.SerializeObject(
                new WalletDTO
                {
                    UserId = id,
                    WalletTypeId = type
                }));

            var response = await service.Call(message);
            service.Close();

            if (response.Equals(""))
                return NotFound();

            var wallets = JsonConvert.DeserializeObject<List<WalletDTO>>(response);

            return Ok(new { wallets });
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RansomDTO), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody][Required] WalletDTO walletDTO)
        {
            var message = new MessageInputModel(DomainConstant.WALLET, MethodConstant.POST, JsonConvert.SerializeObject(walletDTO));

            var response = await service.Call(message);
            service.Close();

            if (response.Equals("")) return NotFound();

            var wallet = JsonConvert.DeserializeObject<WalletDTO>(response);

            return Created($"{Request.Path}/{wallet.Id}", wallet);

        }

        [HttpPut("{id:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RansomDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> Put([FromRoute][Required] int id, [FromBody][Required] WalletDTO walletDTO)
        {
            if (id == 0)
                return BadRequest("Usuário é obrigátorio.");

            walletDTO.Id = id;

            var message = new MessageInputModel(DomainConstant.WALLET, MethodConstant.PUT, JsonConvert.SerializeObject(walletDTO));

            var response = await service.Call(message);
            service.Close();

            var wallets = JsonConvert.DeserializeObject<List<WalletDTO>>(response);

            return Ok(new { wallets });
        }

        [HttpPost("Transfer")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RansomDTO), StatusCodes.Status201Created)]
        public async Task<IActionResult> WalletTransfer([FromBody][Required] WalletTransferDTO walletTransferDTO)
        {
            if (walletTransferDTO == null)
                return NotFound();

            var message = new MessageInputModel(DomainConstant.WALLET, MethodConstant.TRANSFER, JsonConvert.SerializeObject(walletTransferDTO));

            var response = await service.Call(message);
            service.Close();

            if (response.Equals(""))
                return NotFound();

            var walletTransfers = JsonConvert.DeserializeObject<WalletTransferDTO>(response);

            return Created($"{Request.Path}/{walletTransfers.WalletOriginId}/{walletTransfers.WalletTargetId}", walletTransfers);
        }
    }
}
