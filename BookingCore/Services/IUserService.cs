using BookingDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingCore.Services
{
    public interface IUserService : IService<User>
    {
        Task<User> GetUser(int id);
    }
}
