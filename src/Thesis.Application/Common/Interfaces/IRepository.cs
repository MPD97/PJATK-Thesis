using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Thesis.Domain.Entities.Abstract;

namespace Thesis.Application.Common.Interfaces
{
    public interface IRepository<T> : IDisposable where T : BaseEntity
    {
        IQueryable<T> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        IQueryable<T> FindActiveRecordsBy(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task SaveChangesAsync();
        Task Delete(T entity);
        Task UpdateAsync(T entity);
        ITransaction BeginTransaction();
    }
}
