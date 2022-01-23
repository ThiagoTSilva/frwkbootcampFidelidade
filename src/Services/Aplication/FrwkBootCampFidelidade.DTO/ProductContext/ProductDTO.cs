using System;

namespace FrwkBootCampFidelidade.DTO.ProductContext
{
    public class ProductDTO : ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
