using Caliburn.Micro;
using CodeValue.ScrumBoard.Client.Models;
using CodeValue.ScrumBoard.Client.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Refit;
using CodeValue.ScrumBoard.Client.Apis;
using CodeValue.ScrumBoard.Client.Messages;

namespace CodeValue.ScrumBoard.Client.ViewModels
{
    public class BoardsViewModel : Conductor<Screen>.Collection.AllActive, IBoardsViewModel<object>, IHandle<BoardForDeleteMessage>
    {
        private readonly IBoardApi _boardApi;
        private readonly IEventAggregator _eventAggregator;
        private Func<IBoardItemViewModel<object>> _boardItemViewModelCreator;

        public BoardsViewModel(IBoardApi boardApi, IEventAggregator eventAggregator
            , Func<IBoardItemViewModel<object>> boardItemViewModelCreator)
        {
            _boardItemViewModelCreator = boardItemViewModelCreator;
            _boardApi = boardApi;
            //_eventAggregator = eventAggregator;
            eventAggregator.Subscribe(this);
            //  _boardItemViewModelCreator = boardItemViewModelCreator;
        }

        public void AddNewBoard()
        {
            var newBoardItemVm = (BoardItemViewModel)_boardItemViewModelCreator();
            Items.Add(newBoardItemVm);
        }

        public async Task<bool> NavigateToAsync(object args)
        {
            var boards = await GetAllBoardsAsync();

            foreach (var board in boards)
            {
                var newBoardItemVm = (BoardItemViewModel)_boardItemViewModelCreator();

                newBoardItemVm.Description = board.Description;
                newBoardItemVm.Name = board.Name;
                newBoardItemVm.Id = board.Id.ToString();
                newBoardItemVm.IsEditable = false;
                newBoardItemVm.IsInTheDb = true;

                Items.Add(newBoardItemVm);
            }

            return true;
        }

        private async Task<IEnumerable<Board>> GetAllBoardsAsync()
        {
            try
            {
                var boards = await _boardApi.GetBoardsAsync();
                return boards;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Board>();
            }
        }


      

        public async void Handle(BoardForDeleteMessage message)
        {
            if (!String.IsNullOrEmpty(message.BoardId))
            {
               await _boardApi.DeleteBoard(message.BoardId);
            }
            int indexToDelete = GetBoardIndex(message.BoardId);
            if(indexToDelete>-1) Items.RemoveAt(indexToDelete);

        }

        private int GetBoardIndex(string boardId)
        {
            int index = -1;
            if (String.IsNullOrEmpty(boardId))
            {
                index = (Items.Count - 1);
            }
            else
            {
                for (int i=0;i<Items.Count;i++)
                {
                   if( ((BoardItemViewModel)Items[i]).Id == boardId ) return i;
                }
            }
            return index;

        }
    }
}
