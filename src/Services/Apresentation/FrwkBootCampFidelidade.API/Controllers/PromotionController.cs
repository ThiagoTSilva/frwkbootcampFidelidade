using FrwkBootCampFidelidade.Aplicacao.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _promotionService;
        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        [HttpGet("GetAll")]
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

        //[HttpGet("GetById/{id}")]
        //public async Task<IActionResult> GetById(string id)
        //{
        //    try
        //    {
        //        var promotion = await _promotionService.GetById(id);
        //        return Ok(promotion);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        //[HttpGet("GetPromotionByDateRange")]
        //public async Task<IActionResult> GetPromotionByDateRange([FromQuery]PromotionDTO promotionRequestDTO)
        //{
        //    try
        //    {
        //        var promotions = await _promotionService.GetPromotionByDateRange(promotionRequestDTO);
        //        return Ok(promotions);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        //[HttpGet("GetPromotionToday")]
        //public async Task<IActionResult> GetPromotionToday()
        //{
        //    try
        //    {
        //        var promotions = await _promotionService.GetPromotionToday();
        //        return Ok(promotions);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}
    }
}
