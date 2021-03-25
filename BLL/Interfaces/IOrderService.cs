using BLL.DTOs;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IOrderService : ICrud<CreateOrderDTO, ReadOrderDTO, CreateOrderDTO>
    {
         Task<string> DeleteAsUserByIdAsync(int modelId, string currentUserId);
         IEnumerable<ReadOrderDTO> IncomingUserOrders(string currentUserId);
         IEnumerable<ReadOrderDTO> OutcomingUserOrders(string currentUserId);
         IEnumerable<ReadOrderDTO> IncomingUserOrdersByProductName(string currentUserId, string name);
         IEnumerable<ReadOrderDTO> OutcomingUserOrdersByProductName(string currentUserId, string name);
    }
}
