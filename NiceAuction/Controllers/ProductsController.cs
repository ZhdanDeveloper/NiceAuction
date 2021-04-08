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

        /// <summary>
        /// Receiving all products
        /// </summary>
        /// <response code="200">products received successfully</response> 
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_productService.GetAll());
        }

        /// <summary>
        /// Receiving all products
        /// </summary>
        /// <response code="200">products received successfully</response> 
        [AllowAnonymous]
        [HttpGet("my")]
        public IActionResult GetCurrentUserProducts()
        {
            return Ok(_productService.GetProductsOfCurrentUser(_userManager.GetUserId(User)));
        }

        /// <summary>
        /// receiving product by ID
        /// </summary>
        /// <param name="productId">produc ID</param>
        /// <response code="200">product received successfully</response> 
        /// <response code="404">product not found</response> 
        [AllowAnonymous]
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetById(int productId)
        {
            return Ok(await _productService.GetByIdAsync(productId));
        }

        /// <summary>
        /// receiving product by name
        /// </summary>
        /// <param name="name">product name</param>
        /// <response code="200">product received successfully</response> 
        [AllowAnonymous]
        [HttpGet("search")]
        public IActionResult GetByName([FromQuery]string name)
        {
            return Ok(_productService.SearchByName(name));
        }

        /// <summary>
        /// product creation
        /// </summary>
        /// <param name="product">product model</param>
        /// <response code="200">product received</response> 
        /// <response code="401">user is not logged in</response> 
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductDTO product)
        {
           
            product.UserId = _userManager.GetUserId(User);
            return Ok(await _productService.AddAsync(product));
        }

        /// <summary>
        /// adding a product to a category
        /// </summary>
        /// <param name="productId">product id</param>
        /// <param name="categoryId">category id</param>
        /// <response code="200">product has been successfully added to the category</response> 
        /// <response code="401">user is not logged in</response>
        /// <response code="404">product not found or does not belong to the current user</response>
        [HttpPost("{productId}/categories/{categoryId}")]
        public async Task<IActionResult> AssignProductToCategory(int productId, int categoryId)
        {
            var userId =  _userManager.GetUserId(User);
            return Ok(await _productService.AssignProductToCategoryAsync(productId, categoryId, userId));
        }

        /// <summary>
        /// product update
        /// </summary>
        /// <param name="product">product model</param>
        /// <param name="productId">product id</param>
        /// <response code="200">product updated successfully</response> 
        /// <response code="401">user is not logged in</response>
        /// <response code="404">product not found or does not belong to the current user</response>
        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProductAsUser(int productId, [FromForm] UpdateProductDTO product)
        {
            var userId = _userManager.GetUserId(User);
            return Ok(await _productService.UpdateAsUserAsync(productId, userId, product));
        }


        /// <summary>
        /// removing a product from a category
        /// </summary>
        /// <param name="productId">product id</param>
        /// <param name="categoryId">category id</param>
        /// <response code="200">product successfully deleted with category</response> 
        /// <response code="401">user is not logged in</response>
        /// <response code="404">product not found or does not belong to the current user</response>
        [HttpDelete("{productId}/categories/{categoryId}")]
        public async Task<IActionResult> DeleteProductFromCategory(int productId, int categoryId)
        {
            var userId = _userManager.GetUserId(User);
            return Ok(await _productService.DeleteProductFromCategoryByIdAsync(productId, categoryId, userId));
        }

        /// <summary>
        /// deleting a product (depends on role)
        /// </summary>
        /// <param name="productId">product id</param>
        /// <response code="200">product removed successfully</response> 
        /// <response code="401">user is not logged in</response>
        /// <response code="403">the user does not have administrator rights</response>
        /// <response code="404">product not found or does not belong to the current user</response>
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var user = await _userManager.GetUserAsync(User);
            return Ok(await _productService.DeleteByIdAsync(productId, user.Id, user.Role));
        }

        /// <summary>
        /// deleting a product as admin
        /// </summary>
        /// <param name="productId">product id</param>
        /// <response code="200">product removed successfully</response> 
        /// <response code="401">user is not logged in</response>
        /// <response code="403">the user does not have administrator rights</response>
        /// <response code="404">product not found</response>
        //[HttpDelete("admin/{productId}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        //public async Task<IActionResult> DeleteProductAsAdmin(int productId)
        //{
        //    return Ok(await _productService.DeleteByIdAsync(productId));
        //}


    }
}
