using CodeValue.ScrumBoard.Client.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Client.Apis
{
    public interface IUserApi
    {
        
        [Post("/user/login")]
        Task<UserModel> GetUserAsync(UserModel student);

        [Post("/user")]
        Task<string> CreateUserAsync(UserModel student);

        [Delete("/user/{id}")]
        Task DeleteUserAsync(int id);
    }

  
}
