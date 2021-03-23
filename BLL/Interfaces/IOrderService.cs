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
         Task<string> DeleteAsUserByIdAsync(int modelId, string CurrentUserId);
         IEnumerable<ReadOrderDTO> IncomingUserOrders(string CurrentUserId);
         IEnumerable<ReadOrderDTO> OutcomingUserOrders(string CurrentUserId);
    }
}
