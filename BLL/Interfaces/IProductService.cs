using BLL.DTOs;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProductService : ICrud<CreateProductDTO, ReadProductDTO, UpdateProductDTO>
    {
        Task<string> DeleteAsUserByIdAsync(int modelId, string CurrentUserId);
        Task<ReadProductDTO> UpdateAsUserAsync(int id, string CurrentUserId, UpdateProductDTO productDTO);
        Task<string> DeleteProductFromCategoryById(int productId, int CategoryId, string CurrentUserId);
        Task<string> AssignProductToCategory(int productId, int CategoryId, string CurrentUserId);
        IEnumerable<ReadProductDTO> SearchByName(string Name);

    }
}
