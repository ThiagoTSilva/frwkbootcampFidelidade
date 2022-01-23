using Bogus;
using FrwkBootCampFidelidade.Dominio.PromotionContext.Entities;

namespace FrwkBootCampFidelidade.Promotion.FakeData.PromotionItemData
{
    public class PromotionItemFaker : Faker<PromotionItem>
    {
        public PromotionItemFaker()
        {
            var promotionId = new Faker().Random.String2(24);
            var productId = new Faker().Random.Number(0, 999999);
            RuleFor(x => x.ProductId, f => productId);
            RuleFor(x => x.DiscountPercentage, f => new Faker().Random.Double(0, 100));
        }
    }
}
