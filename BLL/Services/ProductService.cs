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
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Injecting repositories
        /// </summary>
        public ProductService(FileManager fileManager, IProductRepository productRepository, IMapper mapper, IProductCategoryRepository productCategoryRepository, ICategoryRepository categoryRepository)
        {
            _fileManager = fileManager;
            _productRepository = productRepository;
            _mapper = mapper;
            _productCategoryRepository = productCategoryRepository;
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// this method adds the product to the database
        /// throws an exception on failure
        /// </summary>
        /// <param name="model">product model</param>
        public async Task<ReadProductDTO> AddAsync(CreateProductDTO model)
        {
            var prod = _mapper.Map<Product>(model);
            prod.PhotoPath = await _fileManager.SaveImage(model.Photo);
            await _productRepository.AddAsync(prod);
            await _productRepository.Save();
            prod = await _productRepository.GetByIdWithDetailsAsync(prod.Id);
            prod.ProductCategories = new List<ProductCategory>();
            var categories = _categoryRepository.FindAll();
            foreach (var item in model.CategoriesIds)
            {
                if (!categories.Contains(categories.FirstOrDefault(x=>x.Id == item)))
                {
                    throw new AuctionException("one of categories does not exist", System.Net.HttpStatusCode.BadRequest);
                }
                prod.ProductCategories.Add(new ProductCategory { ProductId = prod.Id, CategoryId = item });
            }
            _productRepository.Update(prod);
            await _productRepository.Save();

            return _mapper.Map<ReadProductDTO>(prod);
           
        }

        /// <summary>
        /// this method delets the product by id from the database, and checks if current user is owner of product
        /// throws an exception on failure
        /// </summary>
        /// <param name="modelId">order id</param>
        /// <param name="CurrentUserId">user id</param>
        public async Task<string> DeleteAsUserByIdAsync(int modelId, string CurrentUserId)
        {
            var product = await _productRepository.GetByIdAsync(modelId);

            if (product == null)
            {
                throw new AuctionException("Product not found", System.Net.HttpStatusCode.NotFound);
            }

            if (CurrentUserId == product.UserId)
            {
                _fileManager.DeleteImage(product.PhotoPath);
                await _productRepository.DeleteByIdAsync(modelId);
                await _productRepository.Save();
                return $"deleted, Name : {product.Name}, ID : {product.Id}";
            }

            throw new AuctionException("Product doesn't belong to user", System.Net.HttpStatusCode.Forbidden);


        }

        /// <summary>
        /// this method delets the order by id from the database
        /// throws an exception on failure
        /// </summary>
        /// <param name="modelId">order id</param>
        public async Task<string> DeleteByIdAsync(int modelId)
        {
            var product = await _productRepository.GetByIdAsync(modelId);
            if (product == null)
            {
                throw new AuctionException("Product not found", System.Net.HttpStatusCode.NotFound);
            }
            _fileManager.DeleteImage(product.PhotoPath);
            await _productRepository.DeleteByIdAsync(modelId);
            await _productRepository.Save();
            return $"deleted, Name : {product.Name}, ID : {product.Id}";

        }

        /// <summary>
        /// this method returns all products from the database
        /// </summary>
        public IEnumerable<ReadProductDTO> GetAll()
        {
            return _mapper.Map<List<ReadProductDTO>>(_productRepository.FindAllWithDetails());
        }


        /// <summary>
        /// this method returns product from the database by id 
        /// </summary>
        /// <param name="id">order id</param>
        public async Task<ReadProductDTO> GetByIdAsync(int id)
        {
            return _mapper.Map<ReadProductDTO>(await _productRepository.GetByIdWithDetailsAsync(id));
        }

        /// <summary>
        /// this method updates product in database
        /// throws an exception on failure
        /// </summary>
        /// <param name="model">category model</param>
        /// <param name="id">category id</param> 
        public async Task<ReadProductDTO> UpdateAsUserAsync(int id, string CurrentUserId, UpdateProductDTO productDTO)
        {
            var product = await _productRepository.GetByIdWithDetailsAsync(id);
            
            if (product == null || CurrentUserId != product.UserId)
            {
                throw new AuctionException("Product not found", System.Net.HttpStatusCode.NotFound);
            }

                var photopath = productDTO.Photo == null ? null : await _fileManager.SaveImage(productDTO.Photo);
                if (photopath != null)
                {
                    _fileManager.DeleteImage(product.PhotoPath);
                    product.PhotoPath = photopath;
                }
            
                 product = _mapper.Map(productDTO, product);
                 product.UserId = CurrentUserId;
                _productRepository.Update(product);
                 await _productRepository.Save();
              
            
               return _mapper.Map<ReadProductDTO>(product);
        }

        /// <summary>
        /// this method delets product from the category
        /// </summary>
        /// <param name="productId">product id</param>
        /// <param name="CategoryId">category id</param>
        /// <param name="CurrentUserId">category id</param>

        public async Task<string> DeleteProductFromCategoryById(int productId, int CategoryId, string CurrentUserId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            var ProductCategory = _productCategoryRepository.FindAll().FirstOrDefault(x => x.ProductId == productId && x.CategoryId == CategoryId);
            if (product == null)
            {
                throw new AuctionException("Product not found", System.Net.HttpStatusCode.NotFound);
            }
            else if(ProductCategory == null)
            {
                throw new AuctionException("Category not found", System.Net.HttpStatusCode.NotFound);
            }
            if (CurrentUserId == product.UserId)
            {
                _productCategoryRepository.Delete(ProductCategory);
                await _productCategoryRepository.Save();
                return $"Product has been removed from category, Category Id :{CategoryId}, ProductId : {productId}";
            }

            throw new AuctionException("Product doesn't belong to user", System.Net.HttpStatusCode.Forbidden);

        }

        /// <summary>
        /// this method adds product to category the category
        /// </summary>
        /// <param name="productId">product id</param>
        /// <param name="CategoryId">category id</param>
        /// <param name="CurrentUserId">category id</param>
        public async Task<string> AssignProductToCategory(int productId, int CategoryId, string CurrentUserId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            var category = _categoryRepository.FindAll().FirstOrDefault(x => x.Id == CategoryId);
            var ProductCategory = _productCategoryRepository.FindAll().FirstOrDefault(x => x.CategoryId == CategoryId && x.ProductId == productId);
            if (ProductCategory != null)
            {
                throw new AuctionException("Product alredy in category", System.Net.HttpStatusCode.BadRequest);
            }
            else if (product == null)
            {
                throw new AuctionException("Product not found", System.Net.HttpStatusCode.NotFound);
            }
            else if (category == null)
            {
                throw new AuctionException("Category not found", System.Net.HttpStatusCode.NotFound);
            }
            else if (product.UserId != CurrentUserId)
            {
                throw new AuctionException("Product doesn't belong to user", System.Net.HttpStatusCode.Forbidden);
            }
            await  _productCategoryRepository.AddAsync(new ProductCategory { CategoryId = CategoryId, ProductId = productId });
            await _productCategoryRepository.Save();
            return $"Product {product.Name} has been assign to category {category.Name}";
        }

        /// <summary>
        /// this method returns product from the database by name
        /// </summary>
        /// <param name="Name">order id</param>
        public IEnumerable<ReadProductDTO> SearchByName(string Name)
        {
           return _mapper.Map<IEnumerable<ReadProductDTO>>(_productRepository.FindAllWithDetails().Where(x => x.Name.Contains(Name)));
        }

        public Task<string> UpdateAsync(UpdateProductDTO model, int id)
        {
            throw new NotImplementedException();
        }
    }
    
}
