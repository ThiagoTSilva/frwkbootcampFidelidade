using FrwkBootCampFidelidade.DTO.PromotionContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Aplicacao.Interfaces
{
    public interface IPromotionService
    {
        Task<IEnumerable<PromotionDTO>> GetAll();
        Task<PromotionDTO> GetById(string id);
        Task<IEnumerable<PromotionDTO>> GetPromotionByDateRange(PromotionDTO promotionRequest);
        Task<IEnumerable<PromotionDTO>> GetPromotionToday(PromotionDTO promotionRequest);
        Task<PromotionDTO> Add(PromotionCreateUpdateRemoveDTO promotionRequest);
        Task<bool> Update(PromotionCreateUpdateRemoveDTO promotionRequest);
        Task<bool> RemoveById(string id);
        Task<bool> Remove(PromotionCreateUpdateRemoveDTO promotionRequest);
    }
}
