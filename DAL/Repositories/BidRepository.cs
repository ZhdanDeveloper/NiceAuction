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
    public class BidRepository : Repository<Bid>, IBidRepository
    {

        public BidRepository(NiceAuctionContext context) : base(context) { }


        public IQueryable<Bid> FindAllWithDetails()
        {
            return _context.Bids.Include(x => x.User).Include(x => x.Auction);

        }

        public Task<Bid> GetByIdWithDetailsAsync(int id)
        {
            return _context.Bids.Include(x => x.User).Include(x => x.Auction).FirstOrDefaultAsync(x=>x.Id == id);
        }
    }
}
