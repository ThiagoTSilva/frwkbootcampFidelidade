using FrwkBootCampFidelidade.Dominio.Base.Interfaces;
using FrwkBootCampFidelidade.Dominio.PromotionContext.Entities;
using FrwkBootCampFidelidade.Dominio.PromotionContext.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Infraestrutura.Data.PromotionContext.Repository
{
    public class PromotionRepository : IPromotionRepository
    {
        private readonly IMongoContext _context;

        public PromotionRepository(IMongoContext context)
        {
            _context = context;
        }

        public async Task<Promotion> Add(Promotion promotion)
        {
            await _context.Promotions.InsertOneAsync(promotion);

            return promotion;
        }

        public async Task<bool> Update(Promotion promotion)
        {
            var updateResult = await _context.Promotions.ReplaceOneAsync(
                filter: x => x.Id == promotion.Id, replacement: promotion);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Remove(Promotion promotion)
        {
            FilterDefinition<Promotion> filter = Builders<Promotion>.Filter.Eq(x => x.Id, promotion.Id);

            DeleteResult deleteResult = await _context.Promotions.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<bool> RemoveById(string id)
        {
            FilterDefinition<Promotion> filter = Builders<Promotion>.Filter.Eq(x => x.Id, id);

            DeleteResult deleteResult = await _context.Promotions.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Promotion>> GetAll()
        {
            return await _context.Promotions.Find(x => true).ToListAsync();
        }

        public async Task<Promotion> GetById(string id)
        {
            return await _context.Promotions.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Promotion>> GetPromotionByDateRange(Promotion promotion)
        {
            FilterDefinition<Promotion> filter = Builders<Promotion>.Filter
                .Where(x => x.StartDate >= promotion.StartDate && x.EndDate <= promotion.EndDate);

            return await _context.Promotions.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Promotion>> GetPromotionToday(Promotion promotion)
        {
            FilterDefinition<Promotion> filter = Builders<Promotion>.Filter
                .Where(x => x.UserId == promotion.UserId &&
                       x.DrugstoreId == promotion.DrugstoreId &&
                       x.StartDate >= DateTime.Now.Date &&
                       x.EndDate <= DateTime.Now.Date);
                        
            return await _context.Promotions.Find(filter).ToListAsync();
        }
    }
}
