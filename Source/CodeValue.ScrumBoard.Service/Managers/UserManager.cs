using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeValue.ScrumBoard.Service.Entities;
using MongoDB.Driver;
using CodeValue.ScrumBoard.Service.Infrastructure;
using CodeValue.ScrumBoard.Service.DTOs;
using SimplePassword;
using EnsureThat;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace CodeValue.ScrumBoard.Service.Managers
{
    internal class UserManager : IUserManager
    {
        private const int MaxImageSize = 262144;
        private const int MinPasswordLength = 8;

        public async System.Threading.Tasks.Task RegisterUserAsync(UserRegistrationDto userRegistration)
        {
            EnsureArg.IsNotNull(userRegistration);
            EnsureArg.IsNotNull(userRegistration.Credentials);
            EnsureArg.IsNotNullOrEmpty(userRegistration.Credentials.UserName);
            EnsureArg.IsNotNullOrEmpty(userRegistration.Credentials.Password);

            var credentials = userRegistration.Credentials;
            var collection = DBHelper.GetCollection<User>(DbCollections.Users);
            var users = await collection.FindAsync(x => x.Name.ToLower() == credentials.UserName.ToLower());
            if (users.Any())
            {
                throw new ArgumentException($"User name '{credentials.UserName}' already exist.");
            }

            ValidatePassword(userRegistration.Credentials.Password);
            ValidateImage(userRegistration.Image);

            var hash = new SaltedPasswordHash(credentials.Password);
            var user = new User
            {
                CreationTimeUtc = DateTime.UtcNow,
                Version = 1,
                Name = credentials.UserName,
                Hash = hash.Hash,
                Salt = hash.Salt,
                Image = userRegistration.Image
            };

            await collection.InsertOneAsync(user).ConfigureAwait(false);
        }
        
        public async Task<string> AuthenticateUserAsync(UserCredentialsDto userCredentials)
        {
            var collection = DBHelper.GetCollection<User>(DbCollections.Users);
            var users = await collection.FindAsync(x => x.Name.ToLower() == userCredentials.UserName.ToLower()).ConfigureAwait(false);
            var user = await users.FirstOrDefaultAsync();
            if (user == null)
            {
                throw new ArgumentException("Invalid user name or password.");
            }

            var hash = new SaltedPasswordHash(user.Hash, user.Salt);
            if (!hash.Verify(userCredentials.Password))
            {
                throw new ArgumentException("Invalid user name or password.");
            }

            var token = AuthHelper.GenerateToken(userCredentials.UserName);
            return token;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var collection = DBHelper.GetCollection<User>(DbCollections.Users);
            var users = await collection.FindAsync(Builders<User>.Filter.Empty).ConfigureAwait(false);
            var userList = await users.ToListAsync();
            return userList.Select(x => new UserDto
            {
                Name = x.Name,
                Image = x.Image
            });
        }

        public async Task<UserDto> GetUserByNameAsync(string userName)
        {
            EnsureArg.IsNotNullOrEmpty(userName);

            var collection = DBHelper.GetCollection<User>(DbCollections.Users);
            var users = await collection.FindAsync(x => x.Name.ToLower() == userName.ToLower());
            var userList = await users.ToListAsync();

            if (userList.Count == 0)
            {
                throw new ArgumentException($"User name {userName} is not registered.");
            }

            return userList.Select(x => new UserDto
            {
                Name = x.Name,
                Image = x.Image

            }).First();
        }

        private static void ValidatePassword(string password)
        {
            if (password?.Length < MinPasswordLength)
                throw new ArgumentException($"Password must be at least {MinPasswordLength} characters long.");
        }

        private static void ValidateImage(byte[] image)
        {
            if (image?.Length > MaxImageSize)
                throw new ArgumentException($"Max image size supported is {MaxImageSize / 1024} KBytes.");
        }
    }
}
