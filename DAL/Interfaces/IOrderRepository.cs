using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetByIdWithDetailsAsync(int id);
        IQueryable<Order> FindAllWithDetails();
    }
}
