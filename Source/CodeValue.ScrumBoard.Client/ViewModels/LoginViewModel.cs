using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using CodeValue.ScrumBoard.Client.Navigation;

namespace CodeValue.ScrumBoard.Client.ViewModels
{
    public class LoginViewModel : Screen, ILoginViewModel
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

        public async Task<bool> NavigateToAsync()
        {
            return await Task.Run(() =>
            {

                return true;
            });
        }

        public void Login()
        {

        }
    }
}
