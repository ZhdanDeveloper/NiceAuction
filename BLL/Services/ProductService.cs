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
        private readonly UserManager<User> _userManager;
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

        public Task DeleteByIdAsync(int modelId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReadProductDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ReadProductDTO> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CreateProductDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
