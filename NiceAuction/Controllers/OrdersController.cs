﻿using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NiceAuction.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : ControllerBase
    {

        private readonly IOrderService _orderService;
        private readonly UserManager<User> _userManager;

        public OrdersController(IOrderService orderService, UserManager<User> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }


        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles ="Admin")]
        public IActionResult GetAll()
        {
            return Ok(_orderService.GetAll());
        }

        [HttpGet("incoming")]
        public IActionResult GetIncomingUserOrders()
        {
            return Ok(_orderService.IncomingUserOrders(_userManager.FindByNameAsync(_userManager.GetUserName(User)).Result.Id));
        }

        [HttpGet("outcoming")]
        public IActionResult GetOutcomingUserOrders()
        {
            return Ok(_orderService.OutcomingUserOrders(_userManager.FindByNameAsync(_userManager.GetUserName(User)).Result.Id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDTO order)
        {
            order.UserId = _userManager.FindByNameAsync(_userManager.GetUserName(User)).Result.Id;
            return Ok(await _orderService.AddAsync(order));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return Ok(await _orderService.DeleteAsUserByIdAsync(id, _userManager.FindByNameAsync(_userManager.GetUserName(User)).Result.Id));
        }

        [HttpDelete("admin/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> DeleteProductAsAdmin(int id)
        {
            return Ok(await _orderService.DeleteByIdAsync(id));
        }
    }
}
