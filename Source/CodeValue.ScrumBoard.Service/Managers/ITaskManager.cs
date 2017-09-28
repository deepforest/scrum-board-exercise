using CodeValue.ScrumBoard.Service.DTOs;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task = CodeValue.ScrumBoard.Service.Entities.Task;

namespace CodeValue.ScrumBoard.Service.Managers
{
    public interface ITaskManager
    {
        System.Threading.Tasks.Task<string> CreateTask(NewTask newTask);
        System.Threading.Tasks.Task<bool> UpdateTask(UpdateTask updateTask);
        System.Threading.Tasks.Task DeleteTask(string id);
        System.Threading.Tasks.Task<IEnumerable<Entities.Task>> GetAllTasks(string boardId);
    }
}