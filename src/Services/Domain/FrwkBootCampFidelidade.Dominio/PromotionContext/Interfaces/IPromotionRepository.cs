using FrwkBootCampFidelidade.Dominio.PromotionContext.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Dominio.PromotionContext.Interfaces
{
    public interface IPromotionRepository
    {
        Task<IEnumerable<Promotion>> GetAll();
        Task<Promotion> GetById(string id);
        Task<IEnumerable<Promotion>> GetPromotionByDateRange(Promotion promotion);
        Task<IEnumerable<Promotion>> GetPromotionToday(Promotion promotion);
        Task<Promotion> Add(Promotion promotion);
        Task<bool> Update(Promotion promotion);
        Task<bool> RemoveById(string id);
        Task<bool> Remove(Promotion promotion);
    }
}
