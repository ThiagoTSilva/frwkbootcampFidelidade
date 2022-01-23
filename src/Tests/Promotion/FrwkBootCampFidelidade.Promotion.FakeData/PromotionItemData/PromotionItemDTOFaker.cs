using Bogus;
using FrwkBootCampFidelidade.DTO.PromotionContext;
using FrwkBootCampFidelidade.Promotion.FakeData.ProductData;

namespace FrwkBootCampFidelidade.Promotion.FakeData.PromotionItemData
{
    public class PromotionItemDTOFaker : Faker<PromotionItemDTO>
    {
        public PromotionItemDTOFaker()
        {
            var id = new Faker().Random.String2(24);
            var promotionId = new Faker().Random.String2(24);
            var productId = new Faker().Random.Number(0, 999999);
            RuleFor(x => x.ProductId, f => productId);
            RuleFor(x => x.DiscountPercentage, f => new Faker().Random.Double(0, 100));
            RuleFor(x => x.Product, f => new ProductDTOFaker().Generate());
        }
    }
}