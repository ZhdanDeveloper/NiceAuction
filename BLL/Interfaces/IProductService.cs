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
        Task<string> DeleteAsUserByIdAsync(int modelId, string currentUserId);
        Task<ReadProductDTO> UpdateAsUserAsync(int id, string currentUserId, UpdateProductDTO productDTO);
        Task<string> DeleteProductFromCategoryByIdAsync(int productId, int categoryId, string currentUserId);
        Task<string> AssignProductToCategoryAsync(int productId, int categoryId, string currentUserId);
        IEnumerable<ReadProductDTO> SearchByName(string name);

    }
}
