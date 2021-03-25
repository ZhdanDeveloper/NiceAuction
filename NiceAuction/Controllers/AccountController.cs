using BLL.DTOs;
using DAL.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NiceAuction.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AuthenticationHelper _authenticationHelper;


        public AccountController(AuthenticationHelper authenticationHelper)
        {
            _authenticationHelper = authenticationHelper;
        }

        /// <summary>
        /// getting the current user
        /// </summary>
        /// <response code="200">user got successfully</response>
        [HttpGet("CurrentUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _authenticationHelper.GetCurrentUserByName(User.Identity.Name);

            return Ok(user);
        }

        /// <summary>
        /// User registration 
        /// </summary>
        /// <param name="model">user model</param>
        /// <response code="200">user created successfully</response>
        /// <response code="400">incorrect data entry</response> 
        [HttpPost("Create")]
        public async Task<ActionResult<TokenDTO>> CreateUser([FromBody] CreateUserDTO userModel) 
        {
            var result =  await _authenticationHelper.CreateUser(userModel);
            if (result.Errors == null || result.Errors.ToList().Count < 0)
            {
                return Ok(result);
            }
            return BadRequest(result.Errors);
        }

        /// <summary>
        /// account deleting
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <response code="200">User has been deleted</response>
        /// <response code="401">user is not logged in</response> 
        /// <response code="403">User does not have administrator rights</response> 
        [HttpDelete("admin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles ="Admin")]
        public async Task<ActionResult<string>> DeleteUser(string userId)
        {
            return Ok(await _authenticationHelper.DeleteUserById(userId));
        }

        /// <summary>
        /// authorization
        /// </summary>
        /// <param name="model">login model </param>
        /// <response code="200">user successfully received a token</response>
        /// <response code="400">incorrect data entry</response> 
        [HttpPost("Login")]
        public async Task<ActionResult<TokenDTO>> Login(LoginDTO model)
        {
           return await _authenticationHelper.Login(model);

        }
    



    }
}
