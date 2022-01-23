using FrwkBootCampFidelidade.Aplicacao.Constants;
using FrwkBootCampFidelidade.Aplicacao.Interfaces.RpcService;
using FrwkBootCampFidelidade.Dominio.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Web.BootCampFidelidade.HttpAggregator.Models.DTO;

namespace Web.BootCampFidelidade.HttpAggregator.Controller
{
    [Route("api/v1/[Controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IRpcClientService _service;

        public PromotionController(IRpcClientService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(PromotionDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var message = new MessageInputModel(
               DomainConstant.PROMOTION,
               MethodConstant.GET,
               string.Empty);

            var response = await _service.Call(message);
            _service.Close();

            var promotions = JsonConvert.DeserializeObject<IEnumerable<PromotionDTO>>(response);

            return Ok(new { promotions });
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(PromotionDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(string id)
        {
            var message = new MessageInputModel(
                DomainConstant.PROMOTION,
                MethodConstant.GETBYID,
                id);

            var response = await _service.Call(message);
            _service.Close();

            var promotions = JsonConvert.DeserializeObject<PromotionDTO>(response);

            return Ok(new { promotions });
        }

        [HttpGet("GetPromotionByDateRange")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(PromotionDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPromotionByDateRange([FromQuery] long userId, long drugstoreId, string startDate, string endDate)
        {
            DateTime.TryParse(startDate, out DateTime _startDate);
            DateTime.TryParse(endDate, out DateTime _endDate);

            var promotionRequestDTO = new PromotionDTO
            {
                UserId = userId,
                DrugstoreId = drugstoreId,
                StartDate = _startDate,
                EndDate = _endDate
            };

            var message = new MessageInputModel(
                DomainConstant.PROMOTION,
                MethodConstant.GETPROMOTIONBYDATERANGE,
                JsonConvert.SerializeObject(promotionRequestDTO));

            var response = await _service.Call(message);
            _service.Close();

            var promotions = JsonConvert.DeserializeObject<IEnumerable<PromotionDTO>>(response);

            return Ok(new { promotions });
        }

        [HttpGet("GetPromotionToday")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(PromotionDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPromotionToday()
        {
            var message = new MessageInputModel(
                DomainConstant.PROMOTION,
                MethodConstant.GETPROMOTIONTODAY,
                string.Empty);

            var response = await _service.Call(message);
            _service.Close();

            var promotions = JsonConvert.DeserializeObject<IEnumerable<PromotionDTO>>(response);

            return Ok(new { promotions });
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(PromotionDTO), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] PromotionCreateUpdateRemoveDTO promotion)
        {

            var message = new MessageInputModel(
                DomainConstant.PROMOTION,
                MethodConstant.POST,
                JsonConvert.SerializeObject(promotion));

            var response = await _service.Call(message);
            _service.Close();

            var promotions = JsonConvert.DeserializeObject<PromotionDTO>(response);

            return CreatedAtAction(nameof(GetById), new { id = promotions.Id }, promotions);
        }

        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Put([FromQuery(Name = "id")][Required] string id, [FromBody][Required] PromotionCreateUpdateRemoveDTO promotion)
        {

            if (string.IsNullOrEmpty(id) || id.Equals("0"))
                return BadRequest("Id da promoção é obrigatório.");

            promotion.Id = id;

            var message = new MessageInputModel(
                DomainConstant.PROMOTION,
                MethodConstant.PUT,
                JsonConvert.SerializeObject(promotion));

            var response = await _service.Call(message);
            _service.Close();

            var promotions = JsonConvert.DeserializeObject<PromotionDTO>(response);

            return Ok(new { promotions });
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute][Required] string id)
        {
            if (string.IsNullOrEmpty(id) || id.Equals("0"))
                return BadRequest("Id da promoção é obrigatório.");

            var message = new MessageInputModel(
                DomainConstant.PROMOTION,
                MethodConstant.DELETEBYID,
                id);

            var response = await _service.Call(message);
            _service.Close();

            var promotions = JsonConvert.DeserializeObject<PromotionDTO>(response);

            return Ok(new { promotions });
        }

        [HttpDelete]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromBody][Required] PromotionCreateUpdateRemoveDTO promotion)
        {
            var message = new MessageInputModel(
                DomainConstant.PROMOTION,
                MethodConstant.DELETE,
                JsonConvert.SerializeObject(promotion));

            var response = await _service.Call(message);
            _service.Close();

            var promotions = JsonConvert.DeserializeObject<PromotionDTO>(response);

            return Ok(new { promotions });
        }
    }
}
