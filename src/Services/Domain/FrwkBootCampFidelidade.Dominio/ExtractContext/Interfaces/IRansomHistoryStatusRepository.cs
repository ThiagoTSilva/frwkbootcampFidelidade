using FrwkBootCampFidelidade.Dominio.Base.Interfaces;
using FrwkBootCampFidelidade.Dominio.ExtractContext.Entities;
using FrwkBootCampFidelidade.DTO.ExtractContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Dominio.ExtractContext.Interfaces
{
    public interface IRansomHistoryStatusRepository : IBaseRepository<RansomHistoryStatus>
    {
        Task<IEnumerable<ExtractDTO>> GetByUserId(int userId);
        Task<List<RansomHistoryStatusDTO>> GetByCPF(string CPF);
        Task<List<SummaryPointsDTO>> GetSummaryPoints(int userId);
    }
}
