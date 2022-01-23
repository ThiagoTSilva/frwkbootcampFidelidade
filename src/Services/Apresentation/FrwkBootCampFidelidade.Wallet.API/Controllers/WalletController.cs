using FrwkBootCampFidelidade.Aplicacao.Interfaces;
using FrwkBootCampFidelidade.DTO.WalletContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Wallet.API.Controllers
{
    [Route("[controller]")]
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

        [HttpPost("Transfer")]
        public async Task<ActionResult> WalletTransfer([FromBody] WalletTransferDTO walletTransferDTO)
        {
            if (walletTransferDTO == null)
                return NotFound();
            try
            {
                await _walletService.Transfer(walletTransferDTO);
                return Ok(walletTransferDTO);
            }
            catch
            {
                return BadRequest();
            }

        }

        //[HttpPost("Withdraw")]
        //public async Task<ActionResult> WalletWithdraw([FromBody] WalletWithdrawDTO walletWithdrawDTO)
        //{
        //    if (walletWithdrawDTO == null)
        //        return NotFound();
        //    try
        //    {
        //        await _walletService.Withdraw(walletWithdrawDTO);
        //        return Ok(walletWithdrawDTO);
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }

        //}
    }
}
