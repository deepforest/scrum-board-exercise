using CodeValue.ScrumBoard.Service.DTOs;
using CodeValue.ScrumBoard.Service.Managers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Service.Controllers
{
    /// <summary>
    /// Virtual scrum board user APIs.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : Controller
    {
        private readonly IUserManager _userManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="registration">User registration details.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto registration)
        {
            try
            {
                await _userManager.RegisterUserAsync(registration);
                return Ok(new RegisterUserResponse());
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RegisterUserResponse { Error = ex.Message });
            }
        }

        /// <summary>
        /// Authenticate given user.
        /// </summary>
        /// <param name="credentials">User credentials..</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserCredentialsDto credentials)
        {
            try
            {
                var token = await _userManager.AuthenticateUserAsync(credentials);
                return Ok(new LoginResponse { JwtToken = token });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new LoginResponse { Error = ex.Message });
            }
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _userManager.GetAllUsersAsync();
                var userList = users.ToList();
                return Ok(new GetUsersResponse
                {
                    Count = userList.Count,
                    Users = userList
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new GetUsersResponse { Error = ex.Message });
            }
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{userName}")]
        public async Task<IActionResult> GetUserByName(string userName)
        {
            try
            {
                var user = await _userManager.GetUserByNameAsync(userName);
                return Ok(new GetUserResponse
                {
                    User = user
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new GetUsersResponse { Error = ex.Message });
            }
        }
    }
}