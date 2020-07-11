using BookingCore.Repository;
using BookingDomain.DomainInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookingCore.Services
{
    public abstract class Service<TEntity> : IService<TEntity> where TEntity : class, ITrackable
    {
        protected readonly ITrackableRepository<TEntity> Repository;

        protected Service(ITrackableRepository<TEntity> repository)
            => Repository = repository;

        public virtual void Attach(TEntity item)
           => Repository.Attach(item);

        public virtual void Delete(TEntity item)
            => Repository.Delete(item);

        public virtual void Insert(TEntity item)
            => Repository.Insert(item);

        public virtual void Detach(TEntity item)
            => Repository.Detach(item);

        public virtual void Update(TEntity item)
            => Repository.Update(item);

        public virtual IQueryable<TEntity> Queryable()
            => Repository.Queryable();

        public virtual void ApplyChanges(params TEntity[] entities)
            => Repository.ApplyChanges(entities);

        public virtual void AcceptChanges(params TEntity[] entities)
            => Repository.AcceptChanges(entities);

        public virtual void DetachEntities(params TEntity[] entities)
            => Repository.DetachEntities(entities);

        public virtual async Task LoadRelatedEntities(params TEntity[] entities)
            => await Repository.LoadRelatedEntities(entities);

        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await SaveChangesAsync(cancellationToken);

        public async Task Save()
        {
            await Repository.Save();
        }

    }
}
