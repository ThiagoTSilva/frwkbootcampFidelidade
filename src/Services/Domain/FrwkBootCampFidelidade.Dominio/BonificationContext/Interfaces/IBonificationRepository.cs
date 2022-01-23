using FrwkBootCampFidelidade.Dominio.Base.Interfaces;
using FrwkBootCampFidelidade.Dominio.BonificationContext.Entities;
using FrwkBootCampFidelidade.DTO.BonificationContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Dominio.BonificationContext.Interfaces
{
    public interface IBonificationRepository : IBaseRepository<Bonification>
    {
        //GetByUserId
        Task<List<Bonification>> GetByUserId(int userId);
        Task<List<Bonification>> GetByCPF(string CPF);
    }
}
