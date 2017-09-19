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
using Microsoft.Win32;

namespace CodeValue.ScrumBoard.Client.ViewModels
{
    public class LoginViewModel : Screen, ILoginViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private string _password;

        private string _name;

        private string _imagePath;

        public LoginViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.Subscribe(this);
            _eventAggregator = eventAggregator;
            _name = string.Empty;
            _password = string.Empty;
            _imagePath = string.Empty;
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

        public string ImagePath
        {
            get => _imagePath;
            set
            {
                if (Equals(_imagePath, value))
                    return;
                _imagePath = value;
                NotifyOfPropertyChange(() => ImagePath);
            }
        }

        public async Task<bool> NavigateToAsync<T>(T args)
        {
            return await Task.Run(() => { return true; });
        }

        public void SelectImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            openFileDialog.Multiselect = false;
            ImagePath = openFileDialog.FileName;

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
          //  const string imagePath = @"d:\images\people-icon.png";
            try
            {
                var image = Utils.ImageToBytes(_imagePath);
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
