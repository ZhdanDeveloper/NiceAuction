﻿using AutoMapper;
using BLL.DTOs;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProductService : IProductService
    {

        private readonly FileManager _fileManager;
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IMapper _mapper;

        public ProductService(FileManager fileManager, IProductRepository productRepository, IMapper mapper, IProductCategoryRepository productCategoryRepository)
        {
            _fileManager = fileManager;
            _productRepository = productRepository;
            _mapper = mapper;
            _productCategoryRepository = productCategoryRepository;
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

        public async Task<string> DeleteAsUserByIdAsync(int modelId, string CurrentUserId)
        {
            var product = await _productRepository.GetByIdAsync(modelId);

            if (product == null)
            {
                throw new AuctionException("Category not found", System.Net.HttpStatusCode.NotFound);
            }

            if (CurrentUserId == product.UserId)
            {
                _fileManager.DeleteImage(product.PhotoPath);
                await _productRepository.DeleteByIdAsync(modelId);
                _productRepository.Save();              
            }
            return $"deleted, Name : {product.Name}, ID : {product.Id}";
        }

        public async Task<string> DeleteByIdAsync(int modelId)
        {
            var product = await _productRepository.GetByIdAsync(modelId);
            if (product == null)
            {
                throw new AuctionException("Category not found", System.Net.HttpStatusCode.NotFound);
            }
            _fileManager.DeleteImage(product.PhotoPath);
            await _productRepository.DeleteByIdAsync(modelId);
            _productRepository.Save();
            return $"deleted, Name : {product.Name}, ID : {product.Id}";

        }

        public IEnumerable<ReadProductDTO> GetAll()
        {
            return _mapper.Map<List<ReadProductDTO>>(_productRepository.FindAllWithDetails());
        }

        public async Task<ReadProductDTO> GetByIdAsync(int id)
        {
            return _mapper.Map<ReadProductDTO>(await _productRepository.GetByIdWithDetailsAsync(id));
        }

        public async Task<ReadProductDTO> UpdateAsUserAsync(int id, string CurrentUserId, UpdateProductDTO productDTO)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                throw new AuctionException("Category not found", System.Net.HttpStatusCode.NotFound);
            }
            if (CurrentUserId == product.UserId)
            {
                var photopath = productDTO.Photo == null ? null : await _fileManager.SaveImage(productDTO.Photo);
                if (photopath != null)
                {
                    _fileManager.DeleteImage(product.PhotoPath);
                    product.PhotoPath = photopath;
                }
               
                 product = _mapper.Map(productDTO, product);
                 product.UserId = CurrentUserId;
                _productRepository.Update(product);
                _productRepository.Save();
              
            }
            return _mapper.Map<ReadProductDTO>(product);
        }

        public Task<string> UpdateAsync(CreateProductDTO model)
        {
            throw new NotImplementedException();
        }
    }
    
}
