using CodeValue.ScrumBoard.Client.DTOs;
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
        [Post("/user")]
        Task RegisterUserAsync([Body] UserRegistrationDto registration);

        [Post("/user/login")]
        Task<LoginResponse> LoginAsync([Body] UserCredentialsDto credentials);

        [Get("/user")]
        [Headers("Authorization: Bearer")]
        Task<GetUsersResponse> GetUsersAsync();

        [Get("/user/{userName}")]
        [Headers("Authorization: Bearer")]
        Task<GetUserResponse> GetUserAsync(string userName);
    }
}
