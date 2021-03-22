using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProductService : IProductService
    {

        private readonly FileManager _fileManager;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(FileManager fileManager, IProductRepository productRepository, IMapper mapper)
        {
            _fileManager = fileManager;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ReadProductDTO> AddAsync(CreateProductDTO model)
        {
            var prod = _mapper.Map<Product>(model);
            prod.PhotoPath = await _fileManager.SaveImage(model.Photo);
            await _productRepository.AddAsync(prod);
            _productRepository.Save();
            prod.ProductCategories = new List<ProductCategory>();
            foreach (var item in model.CategoriesIds)
            {
                prod.ProductCategories.Add(new ProductCategory { ProductId = prod.Id, CategoryId = item });
            }
            _productRepository.Update(prod);
            _productRepository.Save();

            return _mapper.Map<ReadProductDTO>(prod);
           
        }

        public async Task DeleteAsUserByIdAsync(int modelId, string CurrentUserId)
        {
            var product = await _productRepository.GetByIdAsync(modelId);
            if (CurrentUserId == product.UserId)
            {
                _fileManager.DeleteImage(product.PhotoPath);
                await _productRepository.DeleteByIdAsync(modelId);
                _productRepository.Save();
            }
            
        }

        public async Task<string> DeleteByIdAsync(int modelId)
        {
            var product = await _productRepository.GetByIdAsync(modelId);
            _fileManager.DeleteImage(product.PhotoPath);
            await _productRepository.DeleteByIdAsync(modelId);
            _productRepository.Save();
            return $"deleted, ID : {product.Id}";

        }

        public IEnumerable<ReadProductDTO> GetAll()
        {
            return _mapper.Map<List<ReadProductDTO>>(_productRepository.FindAllWithDetails());
        }

        public async Task<ReadProductDTO> GetByIdAsync(int id)
        {
            return _mapper.Map<ReadProductDTO>(await _productRepository.GetByIdWithDetailsAsync(id));
        }

        public Task<string> UpdateAsync(CreateProductDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
