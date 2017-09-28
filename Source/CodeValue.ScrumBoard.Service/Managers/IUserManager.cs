using CodeValue.ScrumBoard.Service.DTOs;
using CodeValue.ScrumBoard.Service.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Service.Managers
{
    public interface IUserManager
    {
        Task<string> CreateUser(NewUser user);
        IEnumerable<User> GetUsers();
    }
}