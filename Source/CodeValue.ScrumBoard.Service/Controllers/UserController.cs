using CodeValue.ScrumBoard.Service.DTOs;
using CodeValue.ScrumBoard.Service.Entities;
using CodeValue.ScrumBoard.Service.Managers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace CodeValue.ScrumBoard.Service.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Test response ok.");
        }
        

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