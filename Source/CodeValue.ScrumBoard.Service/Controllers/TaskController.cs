using CodeValue.ScrumBoard.Service.DTOs;
using CodeValue.ScrumBoard.Service.Managers;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Service.Controllers
{
    /// <summary>
    /// Virtual scrum board tasks APIs.
    /// </summary>
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly ITaskManager _taskManager;

        internal TaskController(ITaskManager taskManager)
        {
            _taskManager = taskManager;
        }

        /// <summary>
        /// Get all tasks belongs to a board given its ID.
        /// </summary>
        /// <param name="boardId">The board unique ID,</param>
        /// <returns></returns>
        [HttpGet("{boardId}")]
        public async System.Threading.Tasks.Task<IActionResult> GetBoardTasksAsync([FromQuery] string boardId)
        {
            var tasks = await _taskManager.GetAllTasks(boardId);
            return Ok(tasks);
        }

        /// <summary>
        /// Delete a task given its id.
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async System.Threading.Tasks.Task<IActionResult> DeleteTaskAsync(string taskId)
        {            
            await _taskManager.DeleteTask(taskId);
            return Ok();
        }

        /// <summary>
        /// Create a new task.
        /// </summary>
        /// <param name="task">New task details.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] NewTask task)
        {            
            var id = await _taskManager.CreateTask(task);
            if (id == null)
            {
                return NotFound();
            }

            return Ok(id);
        }

        /// <summary>
        /// Update an existing task.
        /// </summary>
        /// <param name="task">Task properties to update.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateTask task)
        {            
            var IsExist = await _taskManager.UpdateTask(task);
            if (!IsExist)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
