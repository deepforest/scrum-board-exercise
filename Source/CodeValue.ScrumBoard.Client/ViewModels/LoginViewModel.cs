using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using CodeValue.ScrumBoard.Client.Navigation;
using Refit;
using CodeValue.ScrumBoard.Client.Apis;
using CodeValue.ScrumBoard.Client.Common;
using CodeValue.ScrumBoard.Client.Events;
using CodeValue.ScrumBoard.Client.Models;

namespace CodeValue.ScrumBoard.Client.ViewModels
{
    public class LoginViewModel : Screen, ILoginViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private string _password;

        private string _name;

        public LoginViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.Subscribe(this);
            _eventAggregator = eventAggregator;
            _name = string.Empty;
            _password = string.Empty;
        }
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

        public string UserName
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name.Equals(value))
                    return;
                _name = value;

            }
        }

        public async Task<bool> NavigateToAsync()
        {
            return await Task.Run(() =>
            {

                return true;
            });
        }

        public async void Login()
        {
            try
            {
                var user = new UserModel { Name = _name, Password = _password, Image = null };
                var api = RestService.For<IUserApi>(Constants.ServerUri);
                var resultUser = await api.GetUserAsync(user);
                if (resultUser == null)
                    return;

                _eventAggregator.PublishOnUIThread(new UserLoggedInEvent(resultUser));

            }
            catch { }
        }

        public async Task Register()
        {
            const string imagePath = @"d:\images\people-icon.png";
            try
            {
                var image = Utils.ImageToBytes(imagePath);
                var user = new UserModel { Name = _name, Password = _password, Image = image };
                var api = RestService.For<IUserApi>(Constants.ServerUri);
                var resultUser = await api.CreateUserAsync(user);
                if (resultUser != null)
                    _eventAggregator.PublishOnUIThread(new UserRegisterEvent(user));

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

      
    }
}
