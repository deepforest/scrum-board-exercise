using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
namespace CodeValue.ScrumBoard.Client.ViewModels
{
    public class LoginViewModel
    {
        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (_password.Equals(value))
                    return;
                _password = value;

            }
        }

        public string UserName { get; set; }

    }
}
