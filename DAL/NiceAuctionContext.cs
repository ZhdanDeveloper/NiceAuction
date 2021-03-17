using DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class NiceAuctionContext : IdentityDbContext<User>
    {

        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Category> Categories { get; set; }
     


        public NiceAuctionContext(DbContextOptions<NiceAuctionContext> options) : base(options)
        {
           
        }
    }
}
