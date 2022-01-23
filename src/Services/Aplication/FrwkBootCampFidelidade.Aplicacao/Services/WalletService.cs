using AutoMapper;
using FrwkBootCampFidelidade.Aplicacao.Functions;
using FrwkBootCampFidelidade.Aplicacao.Interfaces;
using FrwkBootCampFidelidade.Dominio.BonificationContext.Entities;
using FrwkBootCampFidelidade.Dominio.WalletContext.Entities;
using FrwkBootCampFidelidade.Dominio.WalletContext.Interfaces;
using FrwkBootCampFidelidade.DTO.WalletContext;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Aplicacao.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _wallet;
        private readonly IWalletHistoryTransferRepository _walletHistory;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;

        public WalletService(
            IWalletRepository wallet,
            IMapper mapper,
            IWalletHistoryTransferRepository walletHistory,
            IServiceProvider serviceProvider)
        {
            _wallet = wallet;
            _walletHistory = walletHistory;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
        }

        public async Task<WalletDTO> Add(WalletDTO walletDTO)
        {
            var wallet = _mapper.Map<Wallet>(walletDTO);
            wallet.CreatedAt = DateTime.Now;
            wallet.UpdatedAt = wallet.CreatedAt;

            await _wallet.Add(wallet);
            await _wallet.SaveChanges();

            await PendingBonificationForWalletScore(wallet, walletDTO.CPF);

            return _mapper.Map<WalletDTO>(wallet);
        }

        public async Task<List<WalletDTO>> GetAllByUserId(int userId)
        {
            var wallets = await _wallet.GetAllByUserId(userId);
            return _mapper.Map<List<WalletDTO>>(wallets);
        }

        public async Task<List<WalletDTO>> GetByUserIdAndType(int userId, int walletType)
        {
            var wallets = await _wallet.GetAllByUserIdAndType(userId, walletType);
            return _mapper.Map<List<WalletDTO>>(wallets);
        }

        public async Task Update(WalletDTO walletDTO)
        {
            var wallet = _mapper.Map<Wallet>(walletDTO);
            wallet.UpdatedAt = DateTime.Now;

            _wallet.Update(wallet);
            await _wallet.SaveChanges();
        }

        public async Task<WalletTransferDTO> Transfer(WalletTransferDTO walletTransferDTO)
        {
            WalletTransferDTO retorno = null;

            var originWallet = await _wallet.GetById(walletTransferDTO.WalletOriginId) ?? throw new InvalidOperationException();
            var targetWallet = await _wallet.GetById(walletTransferDTO.WalletTargetId) ?? throw new InvalidOperationException();

            if (originWallet.UserId == targetWallet.UserId)
            {
                if (originWallet.Amount >= walletTransferDTO.Quantity)
                {
                    originWallet.Amount -= walletTransferDTO.Quantity;
                    targetWallet.Amount += TransferCalculation(walletTransferDTO.Quantity, targetWallet.WalletTypeId);

                    originWallet.UpdatedAt = DateTime.Now;
                    targetWallet.UpdatedAt = originWallet.UpdatedAt;

                    _wallet.Update(originWallet);
                    _wallet.Update(targetWallet);

                    await _wallet.SaveChanges();

                    await InsertWalletHistoryTransfer(walletTransferDTO);

                    retorno = _mapper.Map<WalletTransferDTO>(walletTransferDTO);
                }
            }

            return retorno;
        }

        public async Task Withdraw(WalletWithdrawDTO walletWithdrawDTO)
        {
            Wallet wallet = await _wallet.GetById(walletWithdrawDTO.WalletId);

            if (wallet != null && walletWithdrawDTO != null)
            {
                if (wallet.Amount >= walletWithdrawDTO.Amount)
                {
                    wallet.Amount -= walletWithdrawDTO.Amount;
                    wallet.UpdatedAt = DateTime.Now;

                    _wallet.Update(wallet);

                    await _wallet.SaveChanges();
                }
            }
        }

        public async Task UpdateWalletAmountValue(Bonification bonification)
        {
            if (bonification == null) throw new ArgumentException();

            var wallets = await GetByUserIdAndType(bonification?.UserId ?? 0, 1);

            if (wallets.Any())
            {
                using var scope = _serviceProvider.CreateScope();
                var _bonification = scope.ServiceProvider.GetRequiredService<IBonificationService>();

                var wallet = wallets.First();
                wallet.Amount += bonification.ScoreQuantity;
                await Update(wallet);

                await _bonification.UpdateScheduleScoreCredit(bonification);
            }
        }

        private async Task PendingBonificationForWalletScore(Wallet wallet, string cpf)
        {
            if (wallet.WalletTypeId == 1)
            {
                using var scope = _serviceProvider.CreateScope();
                var _bonification = scope.ServiceProvider.GetRequiredService<IBonificationService>();

                var amoutWallet = await _bonification.GetPendingBonificationsByCpf(cpf);

                wallet.Amount = amoutWallet;

                _wallet.Update(wallet);
                await _wallet.SaveChanges();
            }
        }

        private static float TransferCalculation(float quantity, int walletType)
        {
            if (walletType == 1)
                return ScoreCalculator.CalculateScoreByValue(quantity);

            return ScoreCalculator.CalculateValueByScore(quantity);
        }

        private async Task InsertWalletHistoryTransfer(WalletTransferDTO walletTransferDTO)
        {
            var walletHistory = _mapper.Map<WalletHistoryTransfer>(walletTransferDTO);

            walletHistory.CreatedAt = DateTime.Now;

            await _walletHistory.Add(walletHistory);
            await _walletHistory.SaveChanges();
        }
    }
}
