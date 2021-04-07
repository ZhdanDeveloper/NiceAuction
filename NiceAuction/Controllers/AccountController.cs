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
