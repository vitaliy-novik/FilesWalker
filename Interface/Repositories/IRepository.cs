using System;
using System.Collections.Generic;
using Infrastructure.Entities;

namespace Repository.Interface.Repositories
{
    public interface IRepository<T> where T : class, IEntity
    {
        T GetById(string id);

        IEnumerable<T> GetAll();
        
        IEnumerable<T> GetAll(Func<T, bool> func);

        void Delete(T entity);

        void Update(T entity);

        T Create(T entity);
    }
}
