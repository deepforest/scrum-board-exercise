using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeValue.ScrumBoard.Service.Entities;
using MongoDB.Driver;
using CodeValue.ScrumBoard.Service.Infrastructure;
using CodeValue.ScrumBoard.Service.DTOs;

namespace CodeValue.ScrumBoard.Service.Managers
{
    public class UserManager : IUserManager
    {
        public async Task<string> CreateUser(NewUser user)
        {
            var newUser = new User()
            {
                CreationTimeUtc = DateTime.UtcNow,
                Version = 1,
                Secret = user.Password,
                Name = user.Name,
                Image = user.Image
            };
            var mongoCollection = DBHelper.GetCollection<User>(DbCollections.Users);
            await mongoCollection.InsertOneAsync(newUser);
            return newUser.Id.ToString();
        }

        public IEnumerable<User> GetUsers()
        {
            var mongoCollection = DBHelper.GetCollection<User>(DbCollections.Users);
            return mongoCollection.Find(Builders<User>.Filter.Empty).ToEnumerable();
        }
    }
}
