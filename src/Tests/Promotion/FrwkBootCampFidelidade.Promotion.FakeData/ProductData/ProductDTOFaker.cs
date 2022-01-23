using Bogus;
using FrwkBootCampFidelidade.DTO.ProductContext;

namespace FrwkBootCampFidelidade.Promotion.FakeData.ProductData
{
    public class ProductDTOFaker : Faker<ProductDTO>
    {
        public ProductDTOFaker()
        {
            var id = new Faker().Random.Number(1, 999999);
            RuleFor(x => x.Id, f => id);
            RuleFor(x => x.Name, f => new Faker().Random.String(15));
            RuleFor(x => x.Description, f => new Faker().Random.String(100));
            RuleFor(x => x.Price, f => new Faker().Random.Double(0, 100));
        }
    }
}