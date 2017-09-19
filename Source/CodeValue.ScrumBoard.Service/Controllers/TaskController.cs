using CodeValue.ScrumBoard.Service.Managers;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace CodeValue.ScrumBoard.Service.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        [HttpDelete("{id}")]
        public async System.Threading.Tasks.Task<IActionResult> DeleteTaskAsync(ObjectId id)
        {
            var tasksManager = new TaskManager();
            await tasksManager.DeleteTask(id);
            return Ok();
        }
    }
}