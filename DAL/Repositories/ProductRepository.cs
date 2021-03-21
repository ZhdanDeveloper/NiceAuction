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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(NiceAuctionContext context) : base(context) { }

        public IQueryable<Product> FindAllWithDetails()
        {
            return _context.Products.Include(x => x.Orders).Include(x => x.ProductCategories).Include(x=>x.User);
        }

        public Task<Product> GetByIdWithDetailsAsync(int id)
        {
            return _context.Products.Include(x => x.Orders).Include(x => x.ProductCategories).Include(x => x.User).FirstOrDefaultAsync(x=>x.Id == id);
        }


       
    }
}
