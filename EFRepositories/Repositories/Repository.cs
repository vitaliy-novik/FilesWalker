using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EntityFrameworkContext;
using Repository.Interface.Repositories;
using Infrastructure.Entities;

namespace EFRepositories.Repositories
{
    /// <summary>
    /// Base Repository with CRUD operations
    /// </summary>
    /// <typeparam name="TDalEntity">Type of ORM entity</typeparam>
    /// <typeparam name="TEntity">Base type for ORM entity</typeparam>
    public abstract class Repository<TDalEntity, TEntity> : IRepository<TEntity> where TEntity : class, IEntity where TDalEntity : class, TEntity
    {
        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="entity">New entity</param>
        /// <returns>Created etity</returns>
        public TEntity Create(TEntity entity)
        {
            TDalEntity dalEntity = ConvertToDal(entity);
            using (FilesWalkerContext context = new FilesWalkerContext())
            {
                context.Set<TDalEntity>().Add(dalEntity);
                context.SaveChanges();
            }
            
            return dalEntity;
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Target entity</param>
        public void Delete(TEntity entity)
        {
            TDalEntity dalEntity = ConvertToDal(entity);
            using (FilesWalkerContext context = new FilesWalkerContext())
            {
                context.Set<TDalEntity>().Attach(dalEntity);
                context.Set<TDalEntity>().Remove(dalEntity);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Returns all entities
        /// </summary>
        /// <returns>List of entities</returns>
        public IEnumerable<TEntity> GetAll()
        {
            using (FilesWalkerContext context = new FilesWalkerContext())
            {
                return context.Set<TDalEntity>().ToList();
            }
        }

        /// <summary>
        /// Returns all entities satisfied predicate
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAll(Func<TEntity, bool> func)
        {
            using (FilesWalkerContext context = new FilesWalkerContext())
            {
                return context.Set<TDalEntity>().Where(func).ToList();
            }
        }

        /// <summary>
        /// Returns Entity by Id
        /// </summary>
        /// <param name="id">Id of entity</param>
        /// <returns>Entity of type T</returns>
        public TEntity GetById(string id)
        {
            using (FilesWalkerContext context = new FilesWalkerContext())
            {
                return context.Set<TDalEntity>().FirstOrDefault(entity => entity.Id == id);
            }
        }

        /// <summary>
        /// Update existing user with new data
        /// </summary>
        public void Update(TEntity entity)
        {
            using (FilesWalkerContext context = new FilesWalkerContext())
            {
                TDalEntity dalEntity = ConvertToDal(entity);
                context.Set<TDalEntity>().Attach(dalEntity);
                context.Entry(dalEntity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Conver entity to ORM type
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>ORM entity</returns>
        protected abstract TDalEntity ConvertToDal(TEntity entity);
    }
}
