using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
namespace CodeValue.ScrumBoard.Client.ViewModels
{
    public class LoginViewModel : Screen
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
                if (object.Equals(value, _password))
                    return;
                _password = value;
                NotifyOfPropertyChange();
            }
        }


        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                if (object.Equals(value, _userName))
                    return;
                _userName = value;
                NotifyOfPropertyChange();
            }
        }

        
    }
}
