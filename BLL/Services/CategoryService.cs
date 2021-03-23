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


        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Category> AddAsync(CreateCategoryDTO model)
        {
            bool CategoryIsExist = _categoryRepository.FindAll().FirstOrDefault(x => x.Name == model.Name) != null;

            if (CategoryIsExist)
            {
                throw new AuctionException("Category's alredy exist", System.Net.HttpStatusCode.Conflict);
            }

            var category = _mapper.Map<Category>(model);
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.Save();
            return category;
        }

        public async Task<string> DeleteByIdAsync(int modelId)
        {
            var category = await _categoryRepository.GetByIdAsync(modelId);
            if (category == null)
            {
                throw new AuctionException("Category not found", System.Net.HttpStatusCode.NotFound);
            }

            await _categoryRepository.DeleteByIdAsync(modelId);
            await _categoryRepository.Save();
            return $"Deleted, name :{category.Name}, id : {category.Id}";
        }

        public IEnumerable<Category> GetAll()
        {
            return _categoryRepository.FindAll();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<string> UpdateAsync(CreateCategoryDTO model, int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new AuctionException("Category not found", System.Net.HttpStatusCode.NotFound);
            }
            _mapper.Map(model, category);
            _categoryRepository.Update(category);
            await _categoryRepository.Save();
            return "category has been updated succedfully";

        }
    }
}
