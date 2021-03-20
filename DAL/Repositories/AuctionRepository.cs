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
    public class AuctionRepository : Repository<Auction>, IAuctionRepository
    {
        public AuctionRepository(NiceAuctionContext context) : base(context) { }

        public IQueryable<Auction> FindAllWithDetails()
        {
            return _context.Auctions.Include(x => x.Bids).Include(x => x.AuctionCategories).Include(x=>x.User);
        }

        public Task<Auction> GetByIdWithDetailsAsync(int id)
        {
            return _context.Auctions.Include(x => x.Bids).Include(x => x.AuctionCategories).Include(x => x.User).FirstOrDefaultAsync(x=>x.Id == id);
        }
    }
}
