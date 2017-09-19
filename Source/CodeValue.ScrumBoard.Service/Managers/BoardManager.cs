using System.Threading.Tasks;
using CodeValue.ScrumBoard.Service.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using CodeValue.ScrumBoard.Service.DTOs;
using System.Collections.Generic;

namespace CodeValue.ScrumBoard.Service.Managers
{
    public class BoardManager : IBoardManager
    {
        private readonly string _connectionString = "mongodb://localhost:27017";

        //public async void TestDb()
        //{
        //    var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse("59bfffd8d0ab4e2ecce43392"));
        //    var update = Builders<BsonDocument>.Update.Set("count", "one one").Set("type", "two two");

        //    await _collection.UpdateOneAsync(filter, update);

        //}

        public async Task<UpdateResult> UpdateBoardAsync(Board board)
        {
            var filter = Builders<Board>.Filter.Eq("Id", board.Id);
            var update = Builders<Board>.Update.Set("Name", board.Name)
                .Set("Description", board.Description);

            var collection = GetBoardsCollection();
            UpdateResult result = await collection.UpdateOneAsync(filter, update);
            return result;
        }

        public async Task<Board> CreateBoardAsync(NewBoardDetails board)
        {
            var boardCollection = GetBoardsCollection();

            var newBoard = new Board
            {
                Description = board.Description,
                CreatedBy = "Tomer shamam",
                CreationTimeUtc = System.DateTime.UtcNow,
                Name = board.Name,
                Version = 1,
            };

            await boardCollection.InsertOneAsync(newBoard);
            return newBoard;
        }

        public IEnumerable<Board> GetBoards()
        {
            var dbBoardsCollection = GetBoardsCollection();
            var collection = dbBoardsCollection.Find(Builders<Board>.Filter.Empty).ToList();

            return collection;
        }

        private IMongoCollection<Board> GetBoardsCollection()
        {
            var client = new MongoClient(_connectionString);
            var database = client.GetDatabase("scrumboard");
            var collection = database.GetCollection<Board>(DbCollections.Boards);
            return collection;
        }
    }
}