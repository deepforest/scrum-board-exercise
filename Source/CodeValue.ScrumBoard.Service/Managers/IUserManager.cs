using CodeValue.ScrumBoard.Service.Entities;
using System.Collections.Generic;

namespace CodeValue.ScrumBoard.Service.Managers
{
    public interface IUserManager
    {
        IEnumerable<User> GetUsers();
    }
}