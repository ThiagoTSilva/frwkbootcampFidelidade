using FrwkBootCampFidelidade.Dominio.Base.Interfaces;
using FrwkBootCampFidelidade.Dominio.WalletContext.Entities;
using FrwkBootCampFidelidade.DTO.WalletContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Dominio.WalletContext.Interfaces
{
    public interface IWalletRepository : IBaseRepository<Wallet>
    {
        Task<List<WalletDTO>> GetAllByUserId(int userId);
        Task<List<WalletDTO>> GetAllByUserIdAndType(int userId, int walletType);
    }
}
