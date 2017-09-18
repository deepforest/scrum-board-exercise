using Caliburn.Micro;
using CodeValue.ScrumBoard.Client.Common.Types;
using CodeValue.ScrumBoard.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CodeValue.ScrumBoard.Client.ViewModels
{
    public class TitleBarViewModel: Screen
    {
        private readonly IEventAggregator _eventAggregator;

        private readonly string _title;

        private string _currentUserName;

        private ImageSource _userImage;

        public string Title => _title;

        public string CurrentUserName
        {
            get { return _currentUserName; }
            set
            {
                if (Equals(_userImage, value))
                    return;
                _currentUserName = value;
                NotifyOfPropertyChange(() => CurrentUserName);
            }
        }

        public ImageSource UserImage
        {
            get { return _userImage; }
            set
            {
                if (Equals(_userImage, value))
                    return;
                _userImage = value;
                NotifyOfPropertyChange(() => UserImage);
            }
        }
      
        public TitleBarViewModel(string title,IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;            
            _title = title;
            // TODO: add uri to constants.
            UserImage =  new BitmapImage(new Uri(@"/Images/unknown_user.png", UriKind.Relative));
            CurrentUserName = "guest";
        }

        public void Close()
        {
            try
            {
                PublishWindowStateChanged(WindowStateType.Closed);
            }
            catch (Exception ex)
            { }            
        }

        public void Minimize()
        {
            try
            {
                PublishWindowStateChanged(WindowStateType.Minimized);
            }
            catch (Exception ex)
            { }
        }

        public void Maximize()
        {
            try
            {
                PublishWindowStateChanged(WindowStateType.Maximized);
            }
            catch (Exception ex)
            { }
        }

        public void Normal()
        {
            try
            {
                PublishWindowStateChanged(WindowStateType.Normal);
            }
            catch (Exception ex)
            { }
        }

        private void PublishWindowStateChanged(WindowStateType windowStateType)
        {
            try
            {
                _eventAggregator.PublishOnUIThread(new WindowStateChangedEvent(windowStateType));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
