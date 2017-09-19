using Caliburn.Micro;
using CodeValue.ScrumBoard.Client.Models;
using CodeValue.ScrumBoard.Client.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeValue.ScrumBoard.Client.Events;

namespace CodeValue.ScrumBoard.Client.ViewModels
{
    public class BoardsViewModel : Screen , IBoardsViewModel<object>
    {
        private readonly ObservableCollection<Board> _BoardsCollection;
        private readonly IEventAggregator _eventAggregator;

        public BoardsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

       

        public async void AddNewBoard()
        {

        }

        public async Task<bool> NavigateToAsync(object args)
        {
            return await Task.Run(() => { return true; });
        }

        public void OpenBoard(BoardItemViewModel boardItem)
        {
            _eventAggregator.PublishOnUIThread(new BoardActivePayload());
        }
    }
}
