using System;
using System.Collections.Generic;
using System.Linq;

namespace FrwkBootCampFidelidade.DTO.PromotionContext
{
    public class PromotionDTO : ICloneable
    {
        public string Id { get; set; }
        public long DrugstoreId { get; set; }
        public long UserId { get; set; }
        public bool Active { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<PromotionItemDTO> PromotionItems { get; set; }

        public object Clone()
        {
            var promotionDTO = (PromotionDTO)MemberwiseClone();
            var promotionItemsDTO = new List<PromotionItemDTO>();
            promotionDTO.PromotionItems.ToList().ForEach(x => promotionItemsDTO.Add((PromotionItemDTO)x.Clone()));
            promotionDTO.PromotionItems = promotionItemsDTO;
            return promotionDTO;
        }

        public PromotionDTO CloneTyped()
        {
            return (PromotionDTO)Clone();
        }
    }
}
