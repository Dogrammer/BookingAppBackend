using BookingCore.Repository;
using BookingDomain.DomainInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingCore.Services
{
    public interface IService<TEntity> : ITrackableRepository<TEntity> where TEntity : class, ITrackable
    {

    }
}
