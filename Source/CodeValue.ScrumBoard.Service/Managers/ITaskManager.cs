using CodeValue.ScrumBoard.Service.DTOs;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task = CodeValue.ScrumBoard.Service.Entities.Task;

namespace CodeValue.ScrumBoard.Service.Managers
{
    public interface ITaskManager
    {
        System.Threading.Tasks.Task DeleteTask(ObjectId id);
        System.Threading.Tasks.Task<IEnumerable<Task>> GetAllTasks(string boardId);
    }
}