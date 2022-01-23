using FrwkBootCampFidelidade.Dominio.PromotionContext.Entities;
using MongoDB.Driver;

namespace FrwkBootCampFidelidade.Dominio.Base.Interfaces
{
    public interface IMongoContext
    {
        IMongoCollection<Promotion> Promotions { get; }
        //IMongoCollection<PromotionItem> PromotionItems { get; }
    }
}
