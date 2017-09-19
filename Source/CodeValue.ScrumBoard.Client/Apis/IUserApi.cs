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
        
        [Get("/users}")]
        Task<UserModel> GetUserAsync(string name, string password);

        [Post("/users")]
        Task<UserModel> CreateUserAsync(UserModel student);

        [Delete("/students/{id}")]
        Task DeleteUserAsync(int id);
    }

  
}
