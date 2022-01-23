using Bogus;
using FrwkBootCampFidelidade.Promotion.FakeData.PromotionItemData;

namespace FrwkBootCampFidelidade.Promotions.FakeData.PromotionData
{
    public class PromotionFaker : Faker<Dominio.PromotionContext.Entities.Promotion>
    {
        public PromotionFaker()
        {
            var drugstoreId = new Faker().Random.Number(1, 999999);
            var userId = new Faker().Random.Number(1, 999999);
            RuleFor(x => x.DrugstoreId, f => drugstoreId);
            RuleFor(x => x.UserId, f => userId);
            RuleFor(x => x.Active, f => f.Random.Bool());
            RuleFor(x => x.Description, f => f.Random.String2(15));
            RuleFor(x => x.StartDate, f => f.Date.Past().Date);
            RuleFor(x => x.EndDate, f => f.Date.Past().Date);
            RuleFor(x => x.CreatedAt, f => f.Date.Past().Date);
            RuleFor(x => x.UpdatedAt, f => f.Date.Past().Date);
            RuleFor(x => x.PromotionItems, f => new PromotionItemFaker().Generate(3));
        }
    }
}
