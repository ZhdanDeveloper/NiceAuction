using BLL.DTOs;
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

        /// <summary>
        /// receiving all orders
        /// </summary>
        /// <response code="200">orders received successfully</response>
        /// <response code="401">user is not logged in</response> 
        /// <response code="403">the user does not have administrator rights</response> 
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles ="Admin")]
        public IActionResult GetAll()
        {
            return Ok(_orderService.GetAll());
        }

        /// <summary>
        /// receiving all incoming user orders if name != null else receiving all outcoming user orders by name
        /// </summary>
        /// <response code="200">orders received successfully</response>
        /// <response code="401">user is not logged in</response> 
        [HttpGet("incoming")]
        public IActionResult GetIncomingUserOrders(string name)
        {

            var userId = _userManager.GetUserId(User);
            if (name != null)
            {
                return Ok(_orderService.IncomingUserOrdersByProductName(userId, name));
            }
            return Ok(_orderService.IncomingUserOrders(userId));
        }



        /// <summary>
        /// receiving all outcoming user orders if name != null else receiving all outcoming user orders by name
        /// </summary>
        /// <param name="name">product name</param>
        /// <response code="200">orders received successfully</response>
        /// <response code="401">user is not logged in</response> 
        [HttpGet("outcoming")]
        public IActionResult GetOutcomingUserOrdersByName(string name)
        {
            var userId = _userManager.GetUserId(User);
            if (name != null)
            {
                return Ok(_orderService.OutcomingUserOrdersByProductName(userId, name));
            }
            return Ok(_orderService.OutcomingUserOrders(userId));


        }


        /// <summary>
        /// order creation
        /// </summary>
        /// <param name="order">order model</param>
        /// <response code="200">order successfully created</response>
        /// <response code="400">the user entered incorrect data</response> 
        /// <response code="401">user is not logged in</response>
        /// <response code="404">product not found</response> 
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDTO order)
        {
            order.UserId = _userManager.GetUserId(User);
            return Ok(await _orderService.AddAsync(order));
        }

        /// <summary>
        /// deleting orders as user
        /// </summary>
        /// <param name="id">product id</param>
        /// <response code="200">order successfully deleted</response>
        /// <response code="400">the user entered incorrect data</response> 
        /// <response code="401">user is not logged in</response>
        /// <response code="404">The order was not found or does not belong to the current user or you are not the seller of this product</response> 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var userId =  _userManager.GetUserId(User);
            return Ok(await _orderService.DeleteAsUserByIdAsync(id, userId));
        }

        /// <summary> 
        /// deleting an order as an auction administrator
        /// </summary>
        /// <param name="id">order id</param>
        /// <response code="200">order successfully deleted</response>
        /// <response code="401">user is not logged in</response>
        /// <response code="403">the user does not have administrator rights</response> 
        /// <response code="404">order not found</response> 
        [HttpDelete("admin/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> DeleteOrderAsAdmin(int id)
        {
            return Ok(await _orderService.DeleteByIdAsync(id));
        }
    }
}
