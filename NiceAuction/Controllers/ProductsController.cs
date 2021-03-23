using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;

namespace NiceAuction.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<User> _userManager;

        public ProductsController(IProductService productService, IWebHostEnvironment webHostEnvironment, UserManager<User> userManager)
        {
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_productService.GetAll());
        }


        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _productService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductDTO product)
        {
            product.UserId = _userManager.FindByNameAsync(_userManager.GetUserName(User)).Result.Id;
            return Ok(await _productService.AddAsync(product));
        }

        [HttpPost("{productId}/categories/{categoryId}")]
        public async Task<IActionResult> AssignProductToCategory(int productId, int categoryId)
        {
            return Ok(await _productService.AssigntProductToCategory(productId, categoryId, _userManager.FindByNameAsync(_userManager.GetUserName(User)).Result.Id));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> DeleteProduct(int id, [FromForm] UpdateProductDTO product)
        {
            return Ok(await _productService.UpdateAsUserAsync(id, _userManager.FindByNameAsync(_userManager.GetUserName(User)).Result.Id, product));
        }

        [HttpDelete("{productId}/categories/{categoryId}")]
        public async Task<IActionResult> DeleteProductFromCategory(int productId, int categoryId)
        {

            return Ok(await _productService.DeleteProductFromCategoryById(productId, categoryId, _userManager.FindByNameAsync(_userManager.GetUserName(User)).Result.Id));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {       
            return Ok(await _productService.DeleteAsUserByIdAsync(id, _userManager.FindByNameAsync(_userManager.GetUserName(User)).Result.Id));
        }

        [HttpDelete("admin/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> DeleteProductAsAdmin(int id)
        {
            return Ok(await _productService.DeleteByIdAsync(id));
        }


    }
}
