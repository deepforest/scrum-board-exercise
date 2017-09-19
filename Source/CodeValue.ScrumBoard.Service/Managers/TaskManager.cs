using CodeValue.ScrumBoard.Service.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using CodeValue.ScrumBoard.Service.DTOs;
using System.Threading.Tasks;
using CodeValue.ScrumBoard.Service.Infrastructure;
using System;
using System.Collections.Generic;

namespace CodeValue.ScrumBoard.Service.Managers
{
    public class TaskManager : ITaskManager
    {
        public async Task<string> CreateTask(NewTask newTask)
        {
            var task = new Entities.Task()
            {
                CreationTimeUtc = DateTime.UtcNow,
                Version = 1,
                CreatedBy = newTask.CreatedBy,
                AssignedTo = newTask.AssignedTo,
                Description = newTask.Description,
                Priority = newTask.Priority,
                RemainingWork = newTask.RemainingWork,
                Status = Entities.TaskStatus.Todo
            };
            var mongoCollection = DBHelper.GetCollection<Entities.Task>(DbCollections.Tasks);
            await mongoCollection.InsertOneAsync(task);
            return task.Id.ToString();
        }

        public async Task<bool> UpdateTask(UpdateTask updateTask)
        {

            var mongoCollection = DBHelper.GetCollection<Entities.Task>(DbCollections.Tasks);
            var filter = Builders<Entities.Task>.Filter.Eq(nameof(updateTask.Id), updateTask.Id);
            var update = Builders<Entities.Task>.Update.Set(nameof(updateTask.Id), updateTask.Id);

            if (updateTask.Priority != null)
                update = update.Set(nameof(updateTask.Priority), updateTask.Priority);
            if (updateTask.AssignedTo != null)
                update = update.Set(nameof(updateTask.AssignedTo), updateTask.AssignedTo);
            if (updateTask.Description != null)
                update = update.Set(nameof(updateTask.Description), updateTask.Description);
            if (updateTask.Status != null)
                update = update.Set(nameof(updateTask.Status), updateTask.Status);
            if (updateTask.RemainingWork != null)
                update = update.Set(nameof(updateTask.RemainingWork), updateTask.RemainingWork);
            if (updateTask.Comments != null)
                update = update.Set(nameof(updateTask.Comments), updateTask.Comments);
            var result = await mongoCollection.UpdateOneAsync(filter, update);
            //await mongoCollection.UpdateOneAsync()
            return true;
        }

        public async System.Threading.Tasks.Task DeleteTask(ObjectId id)
        {
            var tasksCollection = GetCollection<Entities.Task>(DbCollections.Tasks);
            var Deletetask = await tasksCollection.DeleteOneAsync(Builders<Entities.Task>.Filter.Eq("Id", id));
        }

        public async System.Threading.Tasks.Task<IEnumerable<Entities.Task>> GetAllTasks(string boardId)
        {
            var tasksCollection = GetCollection<Entities.Task>(DbCollections.Tasks);
            var tasks = await tasksCollection.FindAsync(_ => true);
            return tasks.ToList();
        }

        private static IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            var db = new MongoClient("mongodb://localhost");
            var server = db.GetDatabase("scrumboard");
            var collection = server.GetCollection<T>(collectionName);
            return collection;
        }

        public IEnumerable<Entities.Task> GetTasks()
        {
            var mongoCollection = DBHelper.GetCollection<Entities.Task>(DbCollections.Tasks);
            return mongoCollection.Find(Builders<Entities.Task>.Filter.Empty).ToList();
        }
    }
}