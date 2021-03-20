using BLL.DTOs;
using DAL.Entities;
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
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }



        [HttpPost("Create")]
        public async Task<ActionResult<TokenDTO>> CreateUser([FromBody] UserDTO userModel)
        {
            var user = new User { UserName = userModel.UserName, Email = userModel.Email, FirstName = userModel.FirstName, LastName = userModel.LastName, Balance = 0, Phone = userModel.Phone };
            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (result.Succeeded)
            {
                return BuildToken(new RegisterDTO { EmailAdress = userModel.Email, Name = userModel.UserName, Password = userModel.Password });
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }


        [HttpPost("Login")]
        public async Task<ActionResult<TokenDTO>> Login(LoginDTO model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Name, model.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return BuildToken(new RegisterDTO {Name = model.Name, Password = model.Password });
            }
            else
            {
                return BadRequest("Invalid login attempt");
            }
        }


        private TokenDTO BuildToken(RegisterDTO model)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, model.Name),
              
              
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddYears(10);

            JwtSecurityToken token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: expiration,
            signingCredentials: creds
            );

            return new TokenDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };

        }

    }
}
