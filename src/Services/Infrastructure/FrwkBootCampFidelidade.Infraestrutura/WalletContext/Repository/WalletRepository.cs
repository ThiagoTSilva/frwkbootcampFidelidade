using FrwkBootCampFidelidade.Dominio.WalletContext.Entities;
using FrwkBootCampFidelidade.Dominio.WalletContext.Interfaces;
using FrwkBootCampFidelidade.DTO.WalletContext;
using FrwkBootCampFidelidade.Infraestrutura.Base.Repository;
using FrwkBootCampFidelidade.Infraestrutura.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Infraestrutura.Data.WalletContext.Repository
{
    public class WalletRepository : BaseRepository<Wallet>, IWalletRepository
    {
        private readonly DBContext _context;

        public WalletRepository(DBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<WalletDTO>> GetAllByUserId(int userId)
        {
            var query = from wallets in _context.Wallets
                        join walletTypes in _context.WalletTypes on wallets.WalletTypeId equals walletTypes.Id
                        where userId == wallets.UserId
                        orderby wallets.Id
                        select new WalletDTO() { Id = wallets.Id, Amount = wallets.Amount, DrugstoreId = wallets.DrugstoreId, UserId = wallets.UserId, WalletTypeId = wallets.WalletTypeId };

            return await query.ToListAsync();
        }

        public async Task<List<WalletDTO>> GetAllByUserIdAndType(int userId, int walletType)
        {
            var query = from wallets in _context.Wallets
                        join walletTypes in _context.WalletTypes on wallets.WalletTypeId equals walletTypes.Id
                        where userId == wallets.UserId && walletTypes.Id == walletType
                        orderby wallets.Id
                        select new WalletDTO() { Id = wallets.Id, Amount = wallets.Amount, DrugstoreId = wallets.DrugstoreId, UserId = wallets.UserId, WalletTypeId = wallets.WalletTypeId };

            return await query.ToListAsync();
        }
    }
}
