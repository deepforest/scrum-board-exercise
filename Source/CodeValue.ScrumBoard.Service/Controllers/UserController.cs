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
            var accessor = new StudentAccessor();
            var student = accessor.GetStudents().FirstOrDefault(x => x.Id == id);
            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User student)
        {
            var accessor = new StudentAccessor();
            var newStudent = await accessor.CreateStudentAsync(student.Name);
            return CreatedAtRoute(nameof(GetStudent), new { id = newStudent.Id }, newStudent);
        }
    }
}