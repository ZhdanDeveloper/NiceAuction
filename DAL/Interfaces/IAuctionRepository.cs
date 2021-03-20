using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    interface IAuctionRepository : IRepository<Auction>
    {
        Task<Auction> GetByIdWithDetailsAsync(int id);
        IQueryable<Auction> FindAllWithDetails();

    }
}
