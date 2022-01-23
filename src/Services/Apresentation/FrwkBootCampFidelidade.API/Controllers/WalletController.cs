using FrwkBootCampFidelidade.Aplicacao.Interfaces;
using FrwkBootCampFidelidade.DTO.WalletContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }


        [HttpGet("GetByUserId/{userId}")]
        public async Task<List<WalletDTO>> GetByUserId(int userId)
        {
            List<WalletDTO> wallets = await _walletService.GetAllByUserId(userId);
            return wallets;
        }

        [HttpGet("GetByUserIdAndType/{userId}/{walletType}")]
        public async Task<List<WalletDTO>> GetByUserIdAndType(int userId, int walletType)
        {
            List<WalletDTO> wallets = await _walletService.GetByUserIdAndType(userId, walletType);
            return wallets;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] WalletDTO walletDTO)
        {
            if (walletDTO == null)
                return NotFound();
            try
            {
                await _walletService.Add(walletDTO);
                return Ok(walletDTO);
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPut]
        public ActionResult Put([FromBody] WalletDTO walletDTO)
        {
            if (walletDTO == null)
                return NotFound();
            try
            {
                _walletService.Update(walletDTO);
                return Ok(walletDTO);
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}
