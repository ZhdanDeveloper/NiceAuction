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
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<User> _userManager;

        public ProductController(IProductService productService, IWebHostEnvironment webHostEnvironment, UserManager<User> userManager)
        {
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
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

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductDTO product)
        {
            product.UserId = _userManager.FindByNameAsync(_userManager.GetUserName(User)).Result.Id;
            return Ok(await _productService.AddAsync(product));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> DeleteProduct(int id, [FromForm] UpdateProductDTO product)
        {
            return Ok(await _productService.UpdateAsUserAsync(id, _userManager.FindByNameAsync(_userManager.GetUserName(User)).Result.Id, product));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {       
            return Ok(await _productService.DeleteAsUserByIdAsync(id, _userManager.FindByNameAsync(_userManager.GetUserName(User)).Result.Id));
        }

    }
}
