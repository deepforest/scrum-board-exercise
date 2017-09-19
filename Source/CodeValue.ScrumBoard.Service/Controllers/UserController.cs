using CodeValue.ScrumBoard.Service.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Service.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        [HttpGet("{id}", Name = nameof(GetUser))]
        public async Task<IActionResult> GetUser(int id)
        {

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            return Ok();
        }
    }
}