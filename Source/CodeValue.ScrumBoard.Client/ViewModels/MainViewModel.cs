using System;
using System.Linq;
using Caliburn.Micro;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CodeValue.ScrumBoard.Client.ViewModels
{
    public sealed class MainViewModel : Conductor<Screen>.Collection.OneActive
    {
        private string _currentUserName;
        private ImageSource _userImage;

        public MainViewModel(IEventAggregator eventAggregator,
                            LoginViewModel loginViewModel)
        {
            eventAggregator.Subscribe(this);
            DisplayName = "Virtual Scrumn Board";            
            Items.AddRange(new Screen[] 
            {
               loginViewModel
            });

            UserImage = new BitmapImage(new Uri(@"/Images/unknown_user.png", UriKind.Relative));
            CurrentUserName = "guest";
        }

        public Screen TitleBarScreen => Items[0];

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
    }
}
