using FrwkBootCampFidelidade.Dominio.PromotionContext.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace FrwkBootCampFidelidade.Infraestrutura.Data.PromotionContext.Mapping
{
    public class PromotionContextSeed
    {
        public static void SeedData(IMongoCollection<Promotion> promotionCollection)
        {
            bool existPromotions = promotionCollection.Find(x => true).Any();

            if (!existPromotions)
            {
                promotionCollection.InsertManyAsync(GetMyPromotions());
            }
        }

        private static IEnumerable<Promotion> GetMyPromotions()
        {
            var createdAt = DateTime.Now;
            var updatedAt = createdAt;
            var blackFridayStart = new DateTime(2021, 11, 1);
            var blackFridayEnd = new DateTime(2021, 11, 30);

            return new[]{
                new Promotion()
                {
                    Id = "61ca0c75af617fc6b0458414",
                    Active = true,
                    DrugstoreId = 1,
                    UserId = 1,
                    Description = "Black Friday",
                    StartDate = blackFridayStart,
                    EndDate = blackFridayEnd,
                    CreatedAt = createdAt,
                    UpdatedAt = updatedAt,
                    PromotionItems = new[]{
                        new PromotionItem()
                        {
                            DiscountPercentage = 10,
                            ProductId = 1
                        },
                        new PromotionItem()
                        {
                            DiscountPercentage = 5,
                            ProductId = 1
                        }
                    }
                }
            };
        }

    }
}
