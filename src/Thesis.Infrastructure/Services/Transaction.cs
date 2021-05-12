using Microsoft.EntityFrameworkCore.Storage;
using Thesis.Application.Common.Interfaces;
using Thesis.Infrastructure.Presistance;

namespace Thesis.Infrastructure.Services
{
    public class Transaction : ITransaction
    {
        private readonly IDbContextTransaction _transaction;

        public Transaction(AppDbContext context)
        {
            _transaction = context.Database.BeginTransaction();
        }
        public void Commit()
        {
            _transaction.Commit();
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }
    }
}
