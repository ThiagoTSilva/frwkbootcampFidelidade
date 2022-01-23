using FrwkBootCampFidelidade.Aplicacao.Interfaces;
using FrwkBootCampFidelidade.DTO.ExtractContext;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExtractController : ControllerBase
    {
        private readonly IExtractService _extractService;
        public ExtractController(IExtractService ExtractService)
        {
            _extractService = ExtractService;
        }

        [HttpGet("GetByUserId/{userId}")]        
        public async Task<IList<ExtractDTO>> GetByUserId(int userId)
        {
            var extracts = await _extractService.GetByUserId(userId);
            return extracts;
        }

        [HttpGet("GetByCPF/{cpf}")]        
        public async Task<List<RansomHistoryStatusDTO>> GetByCPF(string cpf)
        {
            List<RansomHistoryStatusDTO> extracts = await _extractService.GetByCPF(cpf);
            return extracts;
        }

        [HttpGet("GetSummaryPointsByUserID/{userId}")]
        public async Task<List<SummaryPointsDTO>> GetSummaryPoints(int userId)
        {
            List<SummaryPointsDTO> extracts = await _extractService.GetSummaryPoints(userId);
            return extracts;
        }
    }
}
