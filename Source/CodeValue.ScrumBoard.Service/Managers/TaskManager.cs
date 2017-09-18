using CodeValue.ScrumBoard.Service.DTOs;
using MongoDB.Driver;
using CodeValue.ScrumBoard.Service.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CodeValue.ScrumBoard.Service.Managers
{
    public class TaskManager : ITaskManager
    {
        public async System.Threading.Tasks.Task<NewTaskResponse> CreateTaskAsync(NewTaskDetailsRequest newTaskDetailsRequest)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var tasksDatabase = client.GetDatabase("scrumboard");
            var tasksCollection = tasksDatabase.GetCollection<Task>(DbCollections.Tasks);
            //TODO:: ID NOT ASSIGNED!!
            Task newTaskToAdd = new Task
            {
                CreationTimeUtc = System.DateTime.UtcNow,
                Version = 1,
                Description = newTaskDetailsRequest.Description,
                CreatedBy = "Tomer shamam",
                AssignedTo = newTaskDetailsRequest.AssignedTo,
                Priority = newTaskDetailsRequest.Priority,
                RemainingWork = newTaskDetailsRequest.RemainingWork,
                Status = TaskStatus.Todo,
            };
            await tasksCollection.InsertOneAsync(newTaskToAdd);
            var returnedTaskResponse = new NewTaskResponse
            {
                Name = newTaskToAdd.Id.ToString(),
                Status = newTaskToAdd.Status
            };
            return returnedTaskResponse;
        }

        public async System.Threading.Tasks.Task<bool> UpdateTaskAsync(string taskId, Task fildesToUpdate)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var tasksDatabase = client.GetDatabase("scrumboard");
            var tasksCollection = tasksDatabase.GetCollection<Task>(DbCollections.Tasks);
            var update = MongoDB.Driver.Builders<Task>.Update
                .Set(task => task.Description, fildesToUpdate?.Description)
                .Set(task => task.CreationTimeUtc, fildesToUpdate?.CreationTimeUtc)
                .Set(task => task.AssignedTo, fildesToUpdate?.AssignedTo)
                .Set(task => task.Comments, fildesToUpdate?.Comments)
                .Set(task => task.CreatedBy, fildesToUpdate?.CreatedBy)
                .Set(task => task.Priority, fildesToUpdate?.Priority)
                .Set(task => task.RemainingWork, fildesToUpdate?.RemainingWork)
                .Set(task => task.Status, fildesToUpdate?.Status)
                .Set(task => task.Version, fildesToUpdate?.Version);
            await tasksCollection.UpdateManyAsync<Task>(taskFilter => taskFilter.Id.ToString() == taskId, update);
            return true;
        }

    }
}