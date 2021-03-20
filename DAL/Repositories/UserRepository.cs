using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(NiceAuctionContext context) : base(context) { }

        public Task<User> FindByNameWithDetails(string name)
        {
            throw new NotImplementedException();
        }
    }
}
