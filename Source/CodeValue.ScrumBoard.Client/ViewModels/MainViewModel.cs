using Caliburn.Micro;
using CodeValue.ScrumBoard.Client.Common;
using CodeValue.ScrumBoard.Client.Messages;
using CodeValue.ScrumBoard.Client.Navigation;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CodeValue.ScrumBoard.Client.ViewModels
{
    public sealed class MainViewModel : Conductor<object>.Collection.OneActive,
                                        IHandle<UserLoggedInMessage>,
                                        IHandle<UserLoggedOutMessage>,
                                        IHandle<BoardActiveMessage>
    {

        private WindowState _currentWindowState;

        private Visibility _progressBarVisibility;

        private Func<INavigation<object>> _loginViewModelCreator;
        private Func<INavigation<object>> _boardsViewModelCreator;
        private Func<INavigation<object>> _taskViewModelCreator;
        private Func<INavigation<object>> _boardItemViewModelCreator;
        private Func<INavigation<BoardActiveMessage>> _boardViewModelCreator;


        private string _currentUserName;
        private ImageSource _userImage;

        public MainViewModel(IEventAggregator eventAggregator,
                            Func<ILoginViewModel<object>> loginViewModelCreator,
                            Func<IBoardsViewModel<object>> boardsViewModelCreator,
                            Func<IBoardViewModel<BoardActiveMessage>> boardViewModelCreator,
                            Func<ITaskViewModel<object>> taskViewModelCreator,
                            Func<IBoardItemViewModel<object>> boardItemViewModelCreator)
        {
            _progressBarVisibility = Visibility.Collapsed;
            eventAggregator.Subscribe(this);
            _loginViewModelCreator = loginViewModelCreator;
            _boardsViewModelCreator = boardsViewModelCreator;
            _taskViewModelCreator = taskViewModelCreator;
            _boardItemViewModelCreator = boardItemViewModelCreator;
            _boardViewModelCreator = boardViewModelCreator;

            Items.AddRange(new INavigation<object>[]
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


        private async void Navigate<T>(INavigation<T> navigation, T args)
        {
            try
            {
                ProgressBarVisibility = Visibility.Visible;
                if (await navigation.NavigateToAsync(args))
                    ActiveItem = navigation;
            }
            finally
            {
                ProgressBarVisibility = Visibility.Collapsed;
            }
        }

        #region  PUB-SUB Handles
        public void Handle(UserLoggedInMessage message)
        {
            try
            {
                var userModel = message.UserModel;
                CurrentUserName = userModel.Name;
                UserImage = Utils.BytesToImage(userModel.Image);
                Navigate(_boardsViewModelCreator(), null);
            }
            catch { }
        }

        public void Handle(UserLoggedOutMessage message)
        {
            try
            {

                // TODO: uri link in conatants
                UserImage = new BitmapImage(new Uri(@"/Images/unknown_user.png", UriKind.Relative));
                CurrentUserName = "guest";
                Navigate(_loginViewModelCreator(), null);
            }
            catch { }
        }

        public void Handle(BoardActiveMessage message)
        {

            Navigate(_boardViewModelCreator(), message);
            //// message.BoardId
            //Navigate<object>(_boardViewModelCreator(), null);
        }

        #endregion
    }
}
