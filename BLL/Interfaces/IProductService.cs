using BLL.DTOs;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProductService : ICrud<CreateProductDTO, ReadProductDTO>
    {
        public Task<string> DeleteAsUserByIdAsync(int modelId, string CurrentUserId);
        Task<ReadProductDTO> UpdateAsUserAsync(int id, string CurrentUserId, UpdateProductDTO productDTO);

    }
}
