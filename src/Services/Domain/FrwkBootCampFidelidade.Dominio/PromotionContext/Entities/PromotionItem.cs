using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FrwkBootCampFidelidade.Dominio.PromotionContext.Entities
{
    public class PromotionItem
    {
        [BsonElement("ProductId")]
        public long ProductId { get; set; }

        [BsonElement("DiscountPercentage")]
        public double DiscountPercentage { get; set; }
    }
}
