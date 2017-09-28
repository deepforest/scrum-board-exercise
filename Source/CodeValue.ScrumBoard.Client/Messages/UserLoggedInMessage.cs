using CodeValue.ScrumBoard.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Client.Messages
{
    public class UserLoggedInMessage
    {
        private readonly UserModel _userModel;

        public UserLoggedInMessage(UserModel userModel)
        {
            _userModel = userModel;
        }

        public UserModel UserModel => _userModel;
    }
}
