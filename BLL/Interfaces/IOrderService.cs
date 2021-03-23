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
        public Task<string> DeleteAsUserByIdAsync(int modelId, string CurrentUserId);

    }
}
