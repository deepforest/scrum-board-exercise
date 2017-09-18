using CodeValue.ScrumBoard.Service.DTOs;
using CodeValue.ScrumBoard.Service.Entities;
using CodeValue.ScrumBoard.Service.Managers;
using Microsoft.AspNetCore.Mvc;

namespace CodeValue.ScrumBoard.Service.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        [HttpPost]
        public async System.Threading.Tasks.Task<NewTaskResponse> CreateNewTaskAsync([FromBody] NewTaskDetailsRequest newTaskDetailsRequest)
        {
            var taskManager = new TaskManager();
            var taskResponse = await taskManager.CreateTaskAsync(newTaskDetailsRequest);
            return taskResponse;// TODO:: return CreatRoute
        }

        public async System.Threading.Tasks.Task<bool> UpdateExistTaskAsync(string taskId, Task fildesToUpdate)
        {
            var taskManager = new TaskManager();
            var updateTaskResul = await taskManager.UpdateTaskAsync(taskId, fildesToUpdate);
            return updateTaskResul;
        }
    }
}
