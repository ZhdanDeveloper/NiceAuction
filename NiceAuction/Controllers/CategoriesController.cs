using BLL.DTOs;
using BLL.Interfaces;
using BLL.Services;
using DAL.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NiceAuction.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// getting all categories
        /// </summary>
        /// <response code="200">category were received successfully</response>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            return Ok(_categoryService.GetAll());
        }

        /// <summary>
        /// creating a category
        /// </summary>
        /// <param name="category">category model</param>
        /// <response code="200">category successfully created</response>
        /// <response code="401">user is not logged in</response> 
        /// <response code="403">the user does not have administrator rights</response> 
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDTO category)
        {
            return Ok(await _categoryService.AddAsync(category));
        }

        /// <summary>
        /// category update
        /// </summary>
        /// <param name="category">category model</param>
        /// <param name="id">category id</param>
        /// <response code="200">category successfully updated</response>
        /// <response code="401">user is not logged in</response> 
        /// <response code="403">the user does not have administrator rights</response>
        /// <response code="404">category not found</response> 

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CreateCategoryDTO category)
        {
            return Ok(await _categoryService.UpdateAsync(category, id));
        }

        /// <summary>
        /// deleting a category
        /// </summary>
        /// <param name="id">category id</param>
        /// <response code="200">category successfully deleted</response>
        /// <response code="401">user is not logged in</response> 
        /// <response code="403">the user does not have administrator rights</response> 
        /// <response code="404">category not found</response> 

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            return Ok(await _categoryService.DeleteByIdAsync(id));
        }
     
    }
}
