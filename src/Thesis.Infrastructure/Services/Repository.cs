using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Thesis.Application.Common.Interfaces;
using Thesis.Domain.Commons;
using Thesis.Infrastructure.Presistance;

namespace Thesis.Infrastructure.Services
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public DbSet<T> Table => _context.Set<T>();

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task AddAsync(T entity)
        {
            await Table.AddAsync(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public virtual IQueryable<T> GetAll()
        {
            IQueryable<T> query = _context
                .Set<T>();
            return query;
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            var query = _context.Set<T>().Where(predicate);
            return query;
        }

        public IQueryable<T> FindActiveRecordsBy(Expression<Func<T, bool>> predicate)
        {
            var query = _context
                .Set<T>()
                .Where(predicate);
            return query;
        }

        public async Task UpdateAsync(T entity)
        {
            Table.Update(entity);
        }
        public ITransaction BeginTransaction()
        {
            return new Transaction(_context);
        }
    }
}
