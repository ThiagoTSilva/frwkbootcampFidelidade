using FrwkBootCampFidelidade.Dominio.WalletContext.Entities;
using FrwkBootCampFidelidade.Dominio.WalletContext.Interfaces;
using FrwkBootCampFidelidade.Infraestrutura.Base.Repository;
using FrwkBootCampFidelidade.Infraestrutura.Context;


namespace FrwkBootCampFidelidade.Infraestrutura.Data.WalletContext.Repository
{
    public class WalletHistoryTransferRepository : BaseRepository<WalletHistoryTransfer>, IWalletHistoryTransferRepository
    {
        private readonly DBContext _context;

        public WalletHistoryTransferRepository(DBContext context) : base(context)
        {
            _context = context;
        }
    }
}
