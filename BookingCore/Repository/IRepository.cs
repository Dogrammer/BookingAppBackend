using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookingCore.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Attach(TEntity item);
        void Detach(TEntity item);
        void Insert(TEntity item);
        void Update(TEntity item);
        void Delete(TEntity item);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task Save();
        IQueryable<TEntity> Queryable();

    }
}
