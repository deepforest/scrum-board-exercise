using Caliburn.Micro;
using CodeValue.ScrumBoard.Client.Common.Types;
using CodeValue.ScrumBoard.Client.Events;
using CodeValue.ScrumBoard.Client.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CodeValue.ScrumBoard.Client.ViewModels
{
    public sealed class MainViewModel : Conductor<Screen>.Collection.AllActive,
                                        IHandle<WindowStateChangedEvent>
    {

        public Screen TitleBarScreen => Items[0];  
        // We would like to have an instance of the main window so we can control its state (minimized,maximized...).
        private Window _mainWindow;

        
        public MainViewModel(IEventAggregator eventAggregator,                               
                            TitleBarViewModel titleBarViewModel,
                            LoginViewModel loginViewModel)
        {
            eventAggregator.Subscribe(this);
            DisplayName = "Virtual Scrumn Board";            
            Items.AddRange(new Screen[] 
            {
               titleBarViewModel,
               loginViewModel
            });
          
        }

        protected override void OnViewAttached(object view, object context)
        {
            // TODO: handle null case.
            _mainWindow = (Window)GetView();
            base.OnViewAttached(view, context);
        }

        /// <summary>
        /// Handle the changes of the windows state that comes from the TitleBarViewModel clicks.
        /// </summary>
        /// <param name="message"></param>
        public void Handle(WindowStateChangedEvent message)
        {
            // TODO: make sure that _mainWindow is not null.
            switch (message.WindowStateType)
            {
                case WindowStateType.Closed:                   
                    Console.WriteLine("Closed");
                    _mainWindow.Close();
                    break;
                case WindowStateType.Maximized:                   
                    _mainWindow.WindowState = WindowState.Maximized;
                    Console.WriteLine("Maximized");
                    break;
                case WindowStateType.Minimized:
                    _mainWindow.WindowState = WindowState.Minimized;
                    //  Console.WriteLine("Minimized");
                    break;
                case WindowStateType.Normal:                    
                    _mainWindow.WindowState = WindowState.Normal;
                    break;
            }            
        }

        private void Navigate()
        {
            Items[0] = Items[1] ;            
        }
    }
}
