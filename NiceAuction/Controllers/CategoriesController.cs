using BLL.DTOs;
using BLL.Interfaces;
using BLL.Services;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles ="Admin")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            return Ok(_categoryService.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDTO category)
        {
           return Ok(await _categoryService.AddAsync(category));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            return Ok(await _categoryService.DeleteByIdAsync(id));
        }
     
    }
}
