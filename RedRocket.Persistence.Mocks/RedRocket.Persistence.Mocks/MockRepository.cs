using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using RedRocket.Persistence.Common;
using RedRocket.Persistence.Common.Validation;

namespace RedRocket.Persistence.Mocks
{
    public class MockRepository<T> : IRepository<T> where T : class
    {
        public List<T> Data { get; set; }
        
        public MockRepository()
        {
            Data = new List<T>();
        }

        public MockRepository(IEnumerable<T> initialData)
            : this()
        {
            Data.AddRange(initialData);
        }

        public virtual IQueryable<T> All()
        {
            return Data.AsQueryable();
        }

        public virtual IQueryable<T> Query(Func<T, bool> predicate)
        {
            return All().Where(predicate).AsQueryable();
        }

        public virtual T FindWithKey(Expression<Func<T, bool>> predicate)
        {
            return All().FirstOrDefault(predicate);
        }

        public virtual T Add(T entity)
        {
            Data.Add(entity);
            return entity;
        }

        public virtual T Update(T entity)
        {
            return entity;
        }

        public virtual void Delete(T entity)
        {
            Data.Remove(entity);
        }

        public IEnumerable<PersistenceValidationError> Validate(T entity)
        {
            return Enumerable.Empty<PersistenceValidationError>();
        }
    }
}