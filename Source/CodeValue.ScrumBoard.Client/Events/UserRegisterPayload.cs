using CodeValue.ScrumBoard.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Client.Events
{
    public class UserRegisterPayload
    {

        private readonly UserModel _userModel;

        public UserRegisterPayload(UserModel userModel)
        {
            _userModel = userModel;
        }

        public UserModel UserModel => _userModel;
    }
}
