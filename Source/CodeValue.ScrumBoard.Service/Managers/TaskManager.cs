using CodeValue.ScrumBoard.Service.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace CodeValue.ScrumBoard.Service.Managers
{
    public class TaskManager : ITaskManager
    {
        public async System.Threading.Tasks.Task DeleteTask(ObjectId id)
        {
            var tasksCollection = GetCollection<Task>(DbCollections.Tasks);
            var Deletetask = await tasksCollection.DeleteOneAsync(Builders<Task>.Filter.Eq("Id", id));
        }

        private static IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            var db = new MongoClient("mongodb://localhost");
            var server = db.GetDatabase("scrumboard");
            var collection = server.GetCollection<T>(collectionName);
            return collection;
        }
    }
}