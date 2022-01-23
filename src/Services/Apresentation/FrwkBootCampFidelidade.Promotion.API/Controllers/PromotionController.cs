using FrwkBootCampFidelidade.Aplicacao.Interfaces;
using FrwkBootCampFidelidade.DTO.PromotionContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Promotion.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _promotionService;
        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<PromotionDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var promotions = await _promotionService.GetAll();
                return Ok(promotions);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetById/{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(PromotionDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var promotion = await _promotionService.GetById(id);

                if (promotion == null)
                {
                    return NotFound();
                }

                return Ok(promotion);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetPromotionByDateRange")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<PromotionDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPromotionByDateRange([FromQuery] long userId, long drugstoreId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var promotion = new PromotionDTO
                {
                    UserId = userId,
                    DrugstoreId = drugstoreId,
                    StartDate = startDate,
                    EndDate = endDate
                };
                var promotions = await _promotionService.GetPromotionByDateRange(promotion);
                return Ok(promotions);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetPromotionToday")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<PromotionDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPromotionToday([FromQuery] long userId, long drugstoreId)
        {
            try
            {
                var promotion = new PromotionDTO
                {
                    UserId = userId,
                    DrugstoreId = drugstoreId,
                };
                var promotions = await _promotionService.GetPromotionToday(promotion);
                return Ok(promotions);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(PromotionDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add([FromBody] PromotionCreateUpdateRemoveDTO promotion)
        {
            try
            {
                if (promotion == null)
                {
                    return BadRequest();
                }

                var promotionDTO = await _promotionService.Add(promotion);

                return CreatedAtAction(nameof(GetById), new { id = promotionDTO.Id }, promotionDTO);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody] PromotionCreateUpdateRemoveDTO promotion)
        {
            try
            {
                if (promotion == null)
                {
                    return BadRequest();
                }

                return Ok(await _promotionService.Update(promotion));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("RemoveById/{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Remove(string id)
        {
            try
            {
                return Ok(await _promotionService.RemoveById(id));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Remove([FromBody] PromotionCreateUpdateRemoveDTO promotion)
        {
            try
            {
                if (promotion == null)
                {
                    return BadRequest();
                }

                return Ok(await _promotionService.Remove(promotion));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
