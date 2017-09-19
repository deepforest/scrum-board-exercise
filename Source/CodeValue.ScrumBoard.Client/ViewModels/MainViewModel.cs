using Caliburn.Micro;
using CodeValue.ScrumBoard.Client.Common;
using CodeValue.ScrumBoard.Client.Events;
using CodeValue.ScrumBoard.Client.Navigation;
using CodeValue.ScrumBoard.Client.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CodeValue.ScrumBoard.Client.ViewModels
{
    public sealed class MainViewModel : Conductor<INavigation>.Collection.OneActive,
                                        IHandle<UserLoggedInEvent>,
                                        IHandle<UserLoggedOutEvent>
    {

        private WindowState _currentWindowState;

        private Visibility _progressBarVisibility;

        private Func<ILoginViewModel> _loginViewModelCreator;
        private Func<IBoardsViewModel> _boardViewModelCreator;
        private Func<ITaskViewModel> _taskViewModelCreator;

        private string _currentUserName;
        private ImageSource _userImage;

     
                     
        public MainViewModel(IEventAggregator eventAggregator,                                                           
                            Func<ILoginViewModel> loginViewModelCreator,
                            Func<IBoardsViewModel> boardViewModelCreator,
                            Func<ITaskViewModel> taskViewModelCreator)
        {
            _progressBarVisibility = Visibility.Collapsed;
            eventAggregator.Subscribe(this);
            _loginViewModelCreator = loginViewModelCreator;
            _boardViewModelCreator = boardViewModelCreator;
            _taskViewModelCreator = taskViewModelCreator;

            Items.AddRange(new INavigation[]
            {               
               loginViewModelCreator() 
            });

            CurrentWindowState = WindowState.Normal;
            UserImage = new BitmapImage(new Uri(@"/Images/unknown_user.png", UriKind.Relative));
            CurrentUserName = "guest";

        }

        public WindowState CurrentWindowState
        {
            get
            {
                return _currentWindowState;
            }
            set
            {
                _currentWindowState = value;
                NotifyOfPropertyChange(() => CurrentWindowState);
            }
        }

        public Visibility ProgressBarVisibility
        {
            get { return _progressBarVisibility; }
            set
            {
                if (Equals(_progressBarVisibility, value))
                    return;
                _progressBarVisibility = value;
                NotifyOfPropertyChange(() => ProgressBarVisibility);
            }
        }

        public string CurrentUserName 
         { 
             get => _currentUserName; 
             set 
             { 
                 if (Equals(_currentUserName, value)) 
                     return;   
                 _currentUserName = value; 
                 NotifyOfPropertyChange(() => CurrentUserName); 
             } 
         } 
  
         public ImageSource UserImage
         { 
             get => _userImage; 
             set 
             { 
                 if (Equals(_userImage, value)) 
                     return;   
                 _userImage = value; 
                 NotifyOfPropertyChange(() => UserImage); 
             } 
         } 
 
 
         protected override void OnInitialize()
         { 
             ActiveItem = Items.First(); 
         }

        #region WINDOW STATE FUNCTIONS

        public void CloseWindow()
        {
            TryClose();
        }

        public void MaximizeWindow()
        {
            CurrentWindowState = WindowState.Maximized;
        }

        public void MinimizeWindow()
        {
            CurrentWindowState = WindowState.Minimized;
        }

        public void NormalizeWindow()
        {
            CurrentWindowState = WindowState.Normal;
        }

#endregion
        private async void Navigate(INavigation navigation)
        {
            try
            {
                ProgressBarVisibility = Visibility.Visible;
                if (await navigation.NavigateToAsync())
                    ActiveItem = navigation;            
            }
            finally
            {
                ProgressBarVisibility = Visibility.Collapsed;
            }
        }

        #region  PUB-SUB Handles
        public void Handle(UserLoggedInEvent message)
        {
            try
            {
                var userModel = message.UserModel;
                CurrentUserName = userModel.Name;
                UserImage = Utils.BytesToImage(userModel.Image);                
                Navigate(_boardViewModelCreator());
            }
            catch { }
        }

        public void Handle(UserLoggedOutEvent message)
        {
            try
            {
                // TODO: clear user image and user name.
                Navigate(_loginViewModelCreator());
            }
            catch { }
        }

#endregion
    }
}
