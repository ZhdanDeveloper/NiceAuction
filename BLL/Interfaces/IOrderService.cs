using BLL.DTOs;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IOrderService 
    {
         IEnumerable<ReadOrderDTO> GetAll();
         Task RemoveOrdersConnectedWithUser(string id);
         Task<ReadOrderDTO> GetByIdAsync(int id);
         Task<ReadOrderDTO> AddAsync(CreateOrderDTO model);
         Task<string> UpdateAsync(CreateOrderDTO model, int id);
         Task<string> DeleteByIdAsync(int modelId, string currentUserId, string role);
         IEnumerable<ReadOrderDTO> IncomingUserOrders(string currentUserId);
         IEnumerable<OutcomingOrderDTO> OutcomingUserOrders(string currentUserId);
         IEnumerable<ReadOrderDTO> IncomingUserOrdersByProductName(string currentUserId, string name);
         IEnumerable<OutcomingOrderDTO> OutcomingUserOrdersByProductName(string currentUserId, string name);
    }
}
