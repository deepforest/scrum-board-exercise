using System.Threading.Tasks;
using CodeValue.ScrumBoard.Service.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CodeValue.ScrumBoard.Service.Managers
{
    public class BoardManager : IBoardManager
    {
        private readonly string _connectionString = "mongodb://localhost:27017";
        private readonly IMongoCollection<BsonDocument> _collection;

        public BoardManager()
        {
            var client = new MongoClient(_connectionString);
            var database = client.GetDatabase("ScrumBoard");
            _collection = database.GetCollection<BsonDocument>("Boards");
        }

        //private async void TestDb()
        //{
        //    var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse("59bfffd8d0ab4e2ecce43392"));
        //    var update = Builders<BsonDocument>.Update.Set("count", "it works!");

        //    await _collection.UpdateOneAsync(filter, update);

        //}

        public async Task<UpdateResult> UpdateBoard(Board board)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Id", board.Id);
            var update = Builders<BsonDocument>.Update.Set("Description", board.Description);

            UpdateResult result = await _collection.UpdateOneAsync(filter, update);
            return result;
        }

    }
}