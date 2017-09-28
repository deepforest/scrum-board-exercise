using System.Threading.Tasks;
using CodeValue.ScrumBoard.Service.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using CodeValue.ScrumBoard.Service.DTOs;
using System.Collections.Generic;

namespace CodeValue.ScrumBoard.Service.Managers
{
    internal class BoardManager : IBoardManager
    {
        private readonly string _connectionString = "mongodb://localhost:27017";

        //public async void TestDb()
        //{
        //    var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse("59bfffd8d0ab4e2ecce43392"));
        //    var update = Builders<BsonDocument>.Update.Set("count", "one one").Set("type", "two two");
        //    await _collection.UpdateOneAsync(filter, update);
        //}

        public async Task<bool> UpdateBoardAsync(NewBoardDetails board)
        {
            ObjectId boardObjectId;
            bool result = false;
            if (ObjectId.TryParse(board.Id, out boardObjectId))
            {
                var filter = Builders<Board>.Filter.Eq("Id", boardObjectId);
                var update = Builders<Board>.Update.Set("Name", board.Name)
                    .Set("Description", board.Description);

                var collection = GetBoardsCollection();
                await collection.UpdateOneAsync(filter, update);
                result = true;
            }
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
            //TODO: Handle enumerable//
            var collection = dbBoardsCollection.Find(Builders<Board>.Filter.Empty).ToEnumerable();

            return collection;
        }

        public async Task<bool> RemoveBoardAsync(string boardId)
        {
            bool status = false;
            if (boardId == null)
            {
                throw new System.ArgumentNullException(nameof(boardId));
            }       

            var boardCollection = GetBoardsCollection();
            ObjectId boardObjectId;
            ;
            if (ObjectId.TryParse(boardId, out boardObjectId))
            {
                await boardCollection.FindOneAndDeleteAsync(Builders<Board>.Filter.Eq("Id", boardObjectId));
                status = true;
            }
            return status;
        }

        private IMongoCollection<Board> GetBoardsCollection()
        {
            string dataBase = "scrumboard";
            var client = new MongoClient(_connectionString);
            var database = client.GetDatabase(dataBase);
            var collection = database.GetCollection<Board>(DbCollections.Boards);
            return collection;
        }


    }
}