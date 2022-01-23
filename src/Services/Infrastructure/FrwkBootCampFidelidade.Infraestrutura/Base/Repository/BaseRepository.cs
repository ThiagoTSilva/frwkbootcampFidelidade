using FrwkBootCampFidelidade.Dominio.Base.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Infraestrutura.Base.Repository
{
    public abstract class BaseRepository<TEntity> : BaseRepositorio<IDBContext, TEntity> where TEntity : class
    {
        protected BaseRepository(IDBContext context) : base(context)
        {
        }
    }
    public abstract class BaseRepositorio<TContext, TEntity> : IBaseRepository<TEntity>
        where TEntity : class
        where TContext : IDBContext
    {
        protected virtual TContext Db { get; private set; }
        protected readonly DbSet<TEntity> DbSet;
        protected BaseRepositorio(TContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual async Task Add(TEntity obj)
            => await Db
                .Set<TEntity>()
                .AddAsync(obj);

        public virtual void Update(TEntity obj)
            => Db
                .Set<TEntity>()
                .Update(obj);

        public virtual async Task<TEntity> GetById(int id)
            => await DbSet
                .FindAsync(id);

        public virtual void Remove(int id)
        {
            TEntity entity = DbSet.FindAsync(id).Result;

            Db
             .Set<TEntity>()
             .Remove(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            Db
             .Set<TEntity>()
             .Remove(entity);
        }

        public async Task<int> SaveChanges()
            => await Db.SaveChangesAsync();

        public void Dispose() =>
             Db.Dispose();

        public IEnumerable<TEntity> GetAll(bool asNoTracking = true)
        {
            if (!asNoTracking)
                return DbSet.ToListAsync().Result;

            return DbSet.AsNoTracking().ToListAsync().Result;
        }

        public IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicado, bool asNoTracking = true)
        {
            if (!asNoTracking)
                return DbSet.Where(predicado);

            return DbSet.Where(predicado).AsNoTracking();
        }

    }
}
