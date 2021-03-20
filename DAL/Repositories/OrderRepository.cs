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
    public class OrderRepository : Repository<Order>, IOrderRepository
    {

        public OrderRepository(NiceAuctionContext context) : base(context) { }


        public IQueryable<Order> FindAllWithDetails()
        {
            return _context.Orders.Include(x => x.User).Include(x => x.Auction);

        }

        public Task<Order> GetByIdWithDetailsAsync(int id)
        {
            return _context.Orders.Include(x => x.User).Include(x => x.Auction).FirstOrDefaultAsync(x=>x.Id == id);
        }
    }
}
