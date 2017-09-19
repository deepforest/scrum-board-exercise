using CodeValue.ScrumBoard.Service.DTOs;
using CodeValue.ScrumBoard.Service.Entities;
using CodeValue.ScrumBoard.Service.Managers;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Service.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        [HttpGet("{boardId}")]
        public async System.Threading.Tasks.Task<IActionResult> GetBoardTasksAsync([FromQuery] string boardId)
        {
            var tasksManager = new TaskManager();
            var tasks = await tasksManager.GetAllTasks(boardId);
            return Ok(tasks);
        }

        [HttpDelete("{id}")]
        public async System.Threading.Tasks.Task<IActionResult> DeleteTaskAsync(ObjectId id)
        {
            var tasksManager = new TaskManager();
            await tasksManager.DeleteTask(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody]NewTask task)
        {
            var manager = new TaskManager();
            var id = await manager.CreateTask(task);
            if (id == null)
            {
                return NotFound();
            }
            return Ok(id);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTask([FromBody]UpdateTask task)
        {
            var manager = new TaskManager();
            var IsExist = await manager.UpdateTask(task);
            if (!IsExist)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
