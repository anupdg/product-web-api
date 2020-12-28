using GalvProducts.Api.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace GalvProducts.Api.Data
{
    /// <summary>
    /// Generic repository for data access
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        public DbContext Context { get; set; }
        public RepositoryBase(DbContext context)
        {
            Context = context;
        }
        public IQueryable<T> FindAll()
        {
            return Context.Set<T>();
        }
        public IQueryable<T> FindByFilter(Expression<Func<T, bool>> filter)
        {
            return Context.Set<T>().Where(filter);
        }
        public void Create(T entity)
        {
            Context.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            Context.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }
    }
}
