using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NiceAuction
{
    public class AuthenticationHelper
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        public AuthenticationHelper(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, IMapper mapper, IUserRepository userRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _mapper = mapper;
            _userRepository = userRepository;
        }


        public async Task<TokenDTO> CreateUser(UserDTO model)
        {
            var UserToCreate = _mapper.Map<User>(model);
            var result = await _userManager.CreateAsync(UserToCreate, model.Password);
          
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                user.Role = "User";
                _userRepository.Save();
                return BuildToken(_mapper.Map<LoginDTO>(model));
              
            }
            else
            {
                return new TokenDTO { Token = "Invalid operation", Expiration = default, Errors = result.Errors};
            }
        }


        public async Task<TokenDTO> Login(LoginDTO model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Name, model.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return BuildToken(model);
            }
            else
            {
                return new TokenDTO { Token = "Invalid login attempt" };
            }
        }



        public async Task<UserDTO> GetCurrentUserByName(string Name)
        {
            User user = await _userManager.FindByNameAsync(Name);
            return _mapper.Map<UserDTO>(user);

        }


        private TokenDTO BuildToken(LoginDTO model)
        {

            var user = _userManager.FindByNameAsync(model.Name).Result;

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, model.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)

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
