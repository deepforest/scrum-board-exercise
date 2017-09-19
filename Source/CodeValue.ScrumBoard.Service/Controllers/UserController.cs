using CodeValue.ScrumBoard.Service.Entities;
using CodeValue.ScrumBoard.Service.Managers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Service.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        [HttpGet("{id}", Name = nameof(GetUser))]
        public async Task<IActionResult> GetUser(int id)
        {
            var manager = new UserManager();
            var user = manager.GetUsers().FirstOrDefault(u => Equals(u.Id, id));
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            return Ok();
        }
    }
}