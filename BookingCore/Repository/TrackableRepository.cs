using BookingDomain.DomainBaseClasses;
using BookingDomain.DomainInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingCore.Repository
{
    public class TrackableRepository<TEntity> : Repository<TEntity>, ITrackableRepository<TEntity>
        where TEntity : class, ITrackable
    {
        public TrackableRepository(DbContext context) : base(context)
        {
        }

        public override void Insert(TEntity item)
        {
            item.TrackingState = TrackingState.Added;
            base.Insert(item);
        }

        public override void Update(TEntity item)
        {
            item.TrackingState = TrackingState.Modified;
            base.Update(item);
        }

        public override void Delete(TEntity item)
        {
            item.TrackingState = TrackingState.Deleted;
            base.Delete(item);
        }

        public virtual void ApplyChanges(params TEntity[] entities)
            => ApplyChanges(entities);

        public virtual void AcceptChanges(params TEntity[] entities)
            => AcceptChanges(entities);

        public virtual void DetachEntities(params TEntity[] entities)
            => DetachEntities(entities);

        public virtual async Task LoadRelatedEntities(params TEntity[] entities)
            => await LoadRelatedEntities(entities);


    }
}
