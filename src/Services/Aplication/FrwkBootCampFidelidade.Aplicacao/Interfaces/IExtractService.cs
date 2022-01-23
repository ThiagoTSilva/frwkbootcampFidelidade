using FrwkBootCampFidelidade.DTO.ExtractContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Aplicacao.Interfaces
{
    public interface IExtractService
    {
        Task Add(RansomHistoryStatusDTO obj);
        Task<IList<ExtractDTO>> GetByUserId(int userId);
        Task<List<RansomHistoryStatusDTO>> GetByCPF(string CPF);
        Task<List<SummaryPointsDTO>> GetSummaryPoints(int userId);
    }
}
