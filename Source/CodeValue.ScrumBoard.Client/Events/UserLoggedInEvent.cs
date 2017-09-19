using CodeValue.ScrumBoard.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Client.Events
{
    public class UserLoggedInEvent
    {
        private readonly UserModel _userModel;

        public UserLoggedInEvent(UserModel userModel)
        {
            _userModel = userModel;
        }

        public UserModel UserModel => _userModel;
    }
}
