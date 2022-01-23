using FrwkBootCampFidelidade.Dominio.Base.Interfaces;
using FrwkBootCampFidelidade.Dominio.RansomContext.Entities;
using FrwkBootCampFidelidade.DTO.RansomContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Dominio.RansomContext.Interfaces
{
    public interface IRansomRepository : IBaseRepository<Ransom>
    {
        Task<List<RansomDTO>> GetListByCPF(string CPF);
    }
}
