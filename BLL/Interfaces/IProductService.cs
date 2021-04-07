using BLL.DTOs;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProductService 
    {
        IEnumerable<ReadProductDTO> GetAll();
        Task<ReadProductDTO> GetByIdAsync(int id);
        Task<ReadProductDTO> AddAsync(CreateProductDTO model);
        Task<string> UpdateAsync(UpdateProductDTO model, int id);
        Task<string> DeleteByIdAsync(int modelId, string currentUserId, string role);
        Task<ReadProductDTO> UpdateAsUserAsync(int id, string currentUserId, UpdateProductDTO productDTO);
        Task<string> DeleteProductFromCategoryByIdAsync(int productId, int categoryId, string currentUserId);
        Task<string> AssignProductToCategoryAsync(int productId, int categoryId, string currentUserId);
        IEnumerable<ReadProductDTO> SearchByName(string name);

    }
}
