using BookingCore.Repository;
using BookingDomain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingCore.Services
{
    public class UserService : Service<User>, IUserService
    {
        public UserService(ITrackableRepository<User> repository) : base(repository)
        {

        }

        public async Task<User> GetUser(int id)
        {
            var user = await Repository.Queryable().FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }


    }
}
