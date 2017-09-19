using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Service.Entities
{
    public static class DBHelper
    {
        public const string ConnectionString = "mongodb://localhost:27017";
        public const string DBName = "scrumboard";

        public static IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            var db = new MongoClient(ConnectionString);
            var server = db.GetDatabase(DBName);
            var collection = server.GetCollection<T>(collectionName);
            return collection;
        }
    }
}
