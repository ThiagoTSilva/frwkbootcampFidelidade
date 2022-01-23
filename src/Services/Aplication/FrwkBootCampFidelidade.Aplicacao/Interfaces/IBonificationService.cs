using FrwkBootCampFidelidade.Dominio.BonificationContext.Entities;
using FrwkBootCampFidelidade.DTO.BonificationContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Aplicacao.Interfaces
{
    public interface IBonificationService
    {
        Task<BonificationDTO> Add(BonificationDTO bonificationDTO);
        Task Remove(int Id);
        Task<IEnumerable<BonificationDTO>> GetByUserId(int userId);
        Task<IEnumerable<BonificationDTO>> GetByCPF(string CPF);
        IEnumerable<BonificationDTO> GetPendingBonification(string CPF);
        Task<float> GetPendingBonificationsByCpf(string cpf);
        Task UpdateScheduleScoreCredit(Bonification bonification);
    }
}
