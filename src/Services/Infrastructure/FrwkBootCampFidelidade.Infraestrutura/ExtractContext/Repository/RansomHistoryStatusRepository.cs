using FrwkBootCampFidelidade.Dominio.ExtractContext.Entities;
using FrwkBootCampFidelidade.Dominio.ExtractContext.Interfaces;
using FrwkBootCampFidelidade.DTO.ExtractContext;
using FrwkBootCampFidelidade.Infraestrutura.Base.Repository;
using FrwkBootCampFidelidade.Infraestrutura.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Infraestrutura.ExtractContext.Repository
{
    public class RansomHistoryStatusRepository : BaseRepository<RansomHistoryStatus>, IRansomHistoryStatusRepository
    {
        private readonly DBContext _context;

        public RansomHistoryStatusRepository(DBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<RansomHistoryStatusDTO>> GetByCPF(string cpf)
        {
            var query = from extracts in _context.RansomHistoryStatus
                        join ransom in _context.Ransoms on extracts.RansomId equals ransom.Id
                        join wallet in _context.Wallets on ransom.WalletId equals wallet.Id
                        where cpf == ransom.CPF
                        orderby extracts.Date
                        select new RansomHistoryStatusDTO()
                        {
                            Id = extracts.Id,
                            WalletId = wallet.Id,
                            Amount = ransom.Amount,
                            Date = extracts.Date
                        };


            return await query.ToListAsync();
        }

        public async Task<List<SummaryPointsDTO>> GetSummaryPoints(int userId)
        {
            var query = from ransom in _context.Ransoms
                        join ransonHistoryStatus in _context.RansomHistoryStatus on ransom.Id equals ransonHistoryStatus.RansomId
                        join wallet in _context.Wallets on ransom.WalletId equals wallet.Id
                        where wallet.UserId == userId
                        group ransom by new { ransom.Date } into g
                        select new SummaryPointsDTO()
                        {
                            Date = g.Key.Date,
                            SumAmount = g.Sum(x => x.Amount)
                        };

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<ExtractDTO>> GetByUserId(int userId)
        {
            var ransomList = await (from ransom in _context.Ransoms
                                    join wallet in _context.Wallets on ransom.WalletId equals wallet.Id
                                    where userId == wallet.UserId
                                    orderby ransom.Date descending
                                    select new ExtractDTO()
                                    {
                                        Id = ransom.Id,
                                        Amount = ransom.Amount,
                                        Date = ransom.Date,
                                        TransactionType = EnumTransactionType.ransom
                                    }).ToListAsync();

            var transferList = await (from transfer in _context.WalletHistoryTransfers
                                      join walletOrigin in _context.Wallets on transfer.WalletOriginId equals walletOrigin.Id
                                      join walletTarget in _context.Wallets on transfer.WalletTargetId equals walletTarget.Id
                                      where userId == walletOrigin.UserId && userId == walletTarget.UserId
                                      orderby transfer.CreatedAt descending
                                      select new ExtractDTO()
                                      {
                                          Id = transfer.Id,
                                          Amount = transfer.Quantity,
                                          Date = transfer.CreatedAt,
                                          TransactionType = EnumTransactionType.transfer
                                      }).ToListAsync();

            return transferList.Union(ransomList);
        }
    }
}
