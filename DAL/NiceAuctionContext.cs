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
        public NiceAuctionContext(DbContextOptions<NiceAuctionContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
