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
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AuthenticationHelper _authenticationHelper;
        public UsersController(AuthenticationHelper authenticationHelper)
        {
            _authenticationHelper = authenticationHelper;
        }

        /// <summary>
        /// getting all users or users that name conains argument name
        /// </summary>
        /// <param name="name">user name</param>
        /// <response code="200">users got successfully</response>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAllUsers(string name)
        {
            var user = await _authenticationHelper.GetAllUsers(name);
            return Ok(user);
        }

        /// <summary>
        /// getting the current user
        /// </summary>
        /// <response code="200">user got successfully</response>
        [HttpGet("CurrentUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _authenticationHelper.GetUserByName(User.Identity.Name);
            return Ok(user);
        }


        /// <summary>
        /// account deleting (for admins)
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <response code="200">User has been deleted</response>
        /// <response code="401">user is not logged in</response> 
        /// <response code="403">User does not have administrator rights</response> 
        [HttpDelete("{userId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<string>> DeleteUser(string userId)
        {
            return Ok(await _authenticationHelper.DeleteUserById(userId));
        }
    }
}
