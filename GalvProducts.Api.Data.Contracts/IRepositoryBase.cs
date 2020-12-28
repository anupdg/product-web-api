using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace GalvProducts.Api.Data.Contracts
{
    /// <summary>
    /// Generic repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepositoryBase<T>
    {
        DbContext Context { get; set; }
        IQueryable<T> FindAll();
        IQueryable<T> FindByFilter(Expression<Func<T, bool>> filter);
        void Create(T entity);
        void Update(T entity);
    }
}
