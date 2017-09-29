using CodeValue.ScrumBoard.Service.DTOs;
using System.Collections.Generic;

namespace CodeValue.ScrumBoard.Service.Managers
{
    public interface IUserManager
    {
        System.Threading.Tasks.Task RegisterUserAsync(UserRegistrationDto userRegistration);

        System.Threading.Tasks.Task<string> AuthenticateUserAsync(UserCredentialsDto userCredentials);

        System.Threading.Tasks.Task<IEnumerable<UserDto>> GetAllUsersAsync();

        System.Threading.Tasks.Task<UserDto> GetUserByNameAsync(string userName);
    }
}