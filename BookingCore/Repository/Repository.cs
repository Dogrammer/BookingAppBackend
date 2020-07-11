using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookingCore.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {


        public Repository(DbContext context)
        {
            Context = context;
            Set = context.Set<TEntity>();
        }

        protected DbContext Context { get; }
        protected DbSet<TEntity> Set { get; }

        public virtual void Attach(TEntity item)
            => Set.Attach(item);

        public virtual void Detach(TEntity item)
            => Context.Entry(item).State = EntityState.Detached;

        public virtual void Insert(TEntity item)
            => Context.Entry(item).State = EntityState.Added;

        public virtual void Update(TEntity item)
            => Context.Entry(item).State = EntityState.Modified;

        public virtual void Delete(TEntity item)
            => Context.Entry(item).State = EntityState.Deleted;

        public virtual IQueryable<TEntity> Queryable() => Set;

        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await SaveChangesAsync(cancellationToken);

        public async Task Save()
        {
            await Context.SaveChangesAsync();
        }


    }
}
