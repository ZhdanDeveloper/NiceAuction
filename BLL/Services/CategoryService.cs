using AutoMapper;
using BLL.DTOs;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Injecting repositories
        /// </summary>
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// this method adds a new category to the database
        /// throws an exception on failure
        /// </summary>
        /// <param name="model">category model</param>
        public async Task<Category> AddAsync(CreateCategoryDTO model)
        {
            bool CategoryIsExist = _categoryRepository.FindAll().FirstOrDefault(x => x.Name == model.Name) != null;

            if (CategoryIsExist)
            {
                throw new AuctionException("Category's alredy exist", System.Net.HttpStatusCode.Conflict);
            }

            var category = _mapper.Map<Category>(model);
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveAsync();
            return category;
        }

        /// <summary>
        /// this method delets the category by id from the database
        /// throws an exception on failure
        /// </summary>
        /// <param name="modelId">category id</param>
        public async Task<string> DeleteByIdAsync(int modelId)
        {
            var category = await _categoryRepository.GetByIdAsync(modelId);
            if (category == null)
            {
                throw new AuctionException("Category not found", System.Net.HttpStatusCode.NotFound);
            }

            await _categoryRepository.DeleteByIdAsync(modelId);
            await _categoryRepository.SaveAsync();
            return $"Deleted, name :{category.Name}, id : {category.Id}";
        }

        /// <summary>
        /// this method returns all categories from the database
        /// </summary>
        public IEnumerable<Category> GetAll()
        {
            return _categoryRepository.FindAll();
        }

        /// <summary>
        /// this method returns order from the database by id 
        /// </summary>
        /// <param name="id">category id</param>
        public async Task<Category> GetByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// this method updates category in database
        /// throws an exception on failure
        /// </summary>
        /// <param name="model">category model</param>
        /// <param name="id">category id</param> 
        public async Task<string> UpdateAsync(CreateCategoryDTO model, int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new AuctionException("Category not found", System.Net.HttpStatusCode.NotFound);
            }
            _mapper.Map(model, category);
            _categoryRepository.Update(category);
            await _categoryRepository.SaveAsync();
            return "category has been updated succedfully";

        }
    }
}
