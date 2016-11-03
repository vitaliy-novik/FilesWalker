using System;
using System.Collections.Generic;
using Infrastructure.Entities;

namespace Repository.Interface.Repositories
{
    /// <summary>
    /// Interface for Repositories with base CRUD functionality
    /// </summary>
    /// <typeparam name="T">Entities type, should implement IEntity interface</typeparam>
    public interface IRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// Returns Entity by Id
        /// </summary>
        /// <param name="id">Id of entity</param>
        /// <returns>Entity of type T</returns>
        T GetById(string id);

        /// <summary>
        /// Returns all entities
        /// </summary>
        /// <returns>List of entities</returns>
        IEnumerable<T> GetAll();
        
        /// <summary>
        /// Returns all entities satisfied predicate
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        IEnumerable<T> GetAll(Func<T, bool> func);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Target entity</param>
        void Delete(T entity);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">New entity data</param>
        void Update(T entity);

        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="entity">New entity</param>
        /// <returns>Created etity</returns>
        T Create(T entity);
    }
}
