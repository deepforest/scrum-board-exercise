using CodeValue.ScrumBoard.Service.DTOs;
using CodeValue.ScrumBoard.Service.Entities;
using CodeValue.ScrumBoard.Service.Managers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace CodeValue.ScrumBoard.Service.Controllers
{
    /// <summary>
    /// Virtual scrum board user APIs.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Get a user by unique ID.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = nameof(GetUser))]
        public IActionResult GetUser(int id)
        {
            var manager = new UserManager();
            var user = manager.GetUsers().FirstOrDefault(u => Equals(u.Id, id));
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        /// <summary>
        /// Authenticate given user.
        /// </summary>
        /// <param name="user">User to authenticate.</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public IActionResult Login([FromBody] LoginUser user)
        {
            var manager = new UserManager();
            var userFromDb = manager.GetUsers().FirstOrDefault(u => Equals(u.Name, user.Name) && Equals(u.Secret, user.Password));
            if (user == null)
            {
                return NotFound();
            }

            return Ok(userFromDb);
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="user">User details.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] NewUser user)
        {
            var manager = new UserManager();
            var id = await manager.CreateUser(user);
            if (id == null)
            {
                return NotFound();
            }

            return Ok(id);
        }
    }
}