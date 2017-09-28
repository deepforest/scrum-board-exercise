using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using CodeValue.ScrumBoard.Client.Navigation;
using CodeValue.ScrumBoard.Client.Apis;
using CodeValue.ScrumBoard.Client.Common;
using CodeValue.ScrumBoard.Client.Models;
using Microsoft.Win32;
using CodeValue.ScrumBoard.Client.DTOs;
using CodeValue.ScrumBoard.Client.Messages;
using System.Diagnostics;

namespace CodeValue.ScrumBoard.Client.ViewModels
{
    public class LoginViewModel : Screen, ILoginViewModel<object>
    {
        private readonly ITokenStore _tokenStore;
        private readonly IEventAggregator _eventAggregator;
        private readonly IUserApi _userApi;
        private string _password;

        private string _name;

        private string _imagePath;

        public LoginViewModel(ITokenStore tokenStore, IEventAggregator eventAggregator, IUserApi userApi)
        {
            eventAggregator.Subscribe(this);
            _tokenStore = tokenStore;
            _eventAggregator = eventAggregator;
            _userApi = userApi;
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

        public async Task<bool> NavigateToAsync(object args)
        {
            return await Task.Run(() => 
            {
                
                return true; });
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
                var credentials = new UserCredentialsDto
                {
                    UserName = _name,
                    Password = _password
                };

                await LoginAsync(credentials);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public async Task Register()
        {
            //  const string imagePath = @"d:\images\people-icon.png";
            try
            {
                var image = Utils.ImageToBytes(_imagePath);
                var registration = new UserRegistrationDto
                {
                    Credentials = new UserCredentialsDto
                    {
                        UserName = _name,
                        Password = _password,
                    },
                    Image = image
                };

                await RegisterUserAsync(registration);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private async Task RegisterUserAsync(UserRegistrationDto registration)
        {
            try
            {                
                await _userApi.RegisterUserAsync(registration);
                var credentials = new UserCredentialsDto
                {
                    UserName = registration.Credentials.UserName,
                    Password = registration.Credentials.Password
                };

                await LoginAsync(credentials);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async Task LoginAsync(UserCredentialsDto credentials)
        {
            try
            {
                var loginResponse = await _userApi.LoginAsync(credentials);
                if (loginResponse.Error != null)
                {
                    Debug.WriteLine(loginResponse.Error);
                }
                else
                {
                    _tokenStore.StoreToken(loginResponse.JwtToken);

                    // Create api again, now with credentials.                    
                    var getUserResponse = await _userApi.GetUserAsync(credentials.UserName);
                    var userModel = new UserModel
                    {
                        Name = getUserResponse.User.Name,
                        Image = getUserResponse.User.Image,
                        Token = loginResponse.JwtToken
                    };

                    _eventAggregator.PublishOnUIThread(new UserLoggedInMessage(userModel));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
