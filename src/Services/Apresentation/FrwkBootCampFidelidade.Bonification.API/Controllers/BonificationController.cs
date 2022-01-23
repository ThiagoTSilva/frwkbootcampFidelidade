using FrwkBootCampFidelidade.Dominio.BonificationContext.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using FrwkBootCampFidelidade.Dominio.BonificationContext.Entities;
using FrwkBootCampFidelidade.DTO.BonificationContext;
using FrwkBootCampFidelidade.Aplicacao.Interfaces;

namespace FrwkBootCampFidelidade.Bonification.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BonificationController : ControllerBase
    {
        private readonly IBonificationService _bonificationService;
        public BonificationController(IBonificationService bonificationService)
        {
            _bonificationService = bonificationService;
        }

        [HttpGet("GetByUserId/{userId}")]        
        public async Task<IEnumerable<BonificationDTO>> GetByUserId(int userId)
        {
            IEnumerable<BonificationDTO> bonifications = await _bonificationService.GetByUserId(userId);
            return bonifications;
        }

        [HttpGet("GetByCPF/{cpf}")]        
        public async Task<IEnumerable<BonificationDTO>> GetByCPF(string cpf)
        {
            IEnumerable<BonificationDTO> bonifications = await _bonificationService.GetByCPF(cpf);
            return bonifications;
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] BonificationDTO bonificationDTO)
        {
            if (bonificationDTO == null)
                return NotFound();
            try
            {
                await _bonificationService.Add(bonificationDTO);
                return Ok(bonificationDTO);
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _bonificationService.Remove(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}
