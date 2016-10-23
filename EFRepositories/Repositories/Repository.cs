using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EntityFrameworkContext;
using EntityFrameworkContext.Entities;
using Repository.Interface.Repositories;
using Infrastructure.Entities;

namespace EFRepositories.Repositories
{
    public abstract class Repository<TDalEntity, TEntity> : IRepository<TEntity> where TEntity : class, IEntity where TDalEntity : class, TEntity
    {
        public TEntity Create(TEntity t)
        {
            TDalEntity dalEntity = ConvertToDal(t);
            using (FilesWalkerContext context = new FilesWalkerContext())
            {
                context.Set<TDalEntity>().Add(dalEntity);
                context.SaveChanges();
            }
            
            return dalEntity;
        }

        public void Delete(TEntity t)
        {
            TDalEntity dalEntity = ConvertToDal(t);
            using (FilesWalkerContext context = new FilesWalkerContext())
            {
                context.Set<TDalEntity>().Remove(dalEntity);
                context.SaveChanges();
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            using (FilesWalkerContext context = new FilesWalkerContext())
            {
                return context.Set<TDalEntity>().ToList();
            }
        }

        public IEnumerable<TEntity> GetAll(Func<TEntity, bool> func)
        {
            using (FilesWalkerContext context = new FilesWalkerContext())
            {
                return context.Set<TDalEntity>().Where(func).ToList();
            }
        }

        public TEntity GetById(string id)
        {
            using (FilesWalkerContext context = new FilesWalkerContext())
            {
                return context.Set<TDalEntity>().FirstOrDefault(entity => entity.Id == id);
            }
        }

        public void Update(TEntity t)
        {
            using (FilesWalkerContext context = new FilesWalkerContext())
            {
                TDalEntity dalEntity = ConvertToDal(t);
                context.Set<TDalEntity>().Attach(dalEntity);
                context.Entry(dalEntity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        protected abstract TDalEntity ConvertToDal(TEntity entity);
    }
}
