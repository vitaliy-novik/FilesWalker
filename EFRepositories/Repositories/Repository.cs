using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Repository.Interface.Repositories;
using Infrastructure.Entities;

namespace EFRepositories.Repositories
{
    public abstract class Repository<TDalEntity, TEntity> : IRepository<TEntity> where TEntity : class, IEntity where TDalEntity : class, TEntity
    {
        protected readonly DbContext context;

        protected Repository(DbContext context)
        {
            this.context = context;
        }

        public TEntity Create(TEntity t)
        {
            TDalEntity dalEntity = ConvertToDal(t);
            context.Set<TDalEntity>().Add(dalEntity);
            context.SaveChanges();
            context.Dispose();
            return dalEntity;
        }

        public void Delete(TEntity t)
        {
            TDalEntity dalEntity = ConvertToDal(t);
            context.Set<TDalEntity>().Remove(dalEntity);
            context.SaveChanges();
            context.Dispose();
        }

        public IEnumerable<TEntity> GetAll(Func<TEntity, bool> func)
        {
            return context.Set<TDalEntity>().Where(func).ToList();
        }

        public TEntity GetById(string id)
        {
            return context.Set<TDalEntity>().FirstOrDefault(entity => entity.Id == id);
        }

        public void Update(TEntity t)
        {
            context.Set<TDalEntity>().Attach((TDalEntity)t);
            context.Entry(t).State = EntityState.Modified;
            context.SaveChanges();
            context.Dispose();
        }

        protected abstract TDalEntity ConvertToDal(TEntity entity);
    }
}
