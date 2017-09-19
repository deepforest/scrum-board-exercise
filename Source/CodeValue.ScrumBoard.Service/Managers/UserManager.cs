using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeValue.ScrumBoard.Service.Entities;
using MongoDB.Driver;

namespace CodeValue.ScrumBoard.Service.Managers
{
    public class UserManager : IUserManager
    {
        public async Task<string> CreateUser(User user)
        {
            var mongoCollection = DBHelper.GetCollection<User>(DbCollections.Users);
            await mongoCollection.InsertOneAsync(user);
            return user.Id.ToString();
        }

        public IEnumerable<User> GetUsers()
        {
            var mongoCollection = DBHelper.GetCollection<User>(DbCollections.Users);
            return mongoCollection.Find(Builders<User>.Filter.Empty).ToEnumerable();
        }
    }
}
