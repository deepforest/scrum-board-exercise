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
    public class BoardsViewModel : Conductor<Screen>.Collection.AllActive, IBoardsViewModel<object>, IHandle<NewBoardForDeleteMessage>
    {
        private readonly IBoardApi _boardApi;
        private readonly IEventAggregator _eventAggregator;
        private readonly Func<IBoardItemViewModel<BoardActivePayload>> _boardItemViewModelCreator;

        public BoardsViewModel(IBoardApi boardApi, IEventAggregator eventAggregator)
        {
            _boardApi = boardApi;
            _eventAggregator = eventAggregator;
            //  _boardItemViewModelCreator = boardItemViewModelCreator;
        }

        public void AddNewBoard()
        {
            var newBoardItemVm = new BoardItemViewModel(_boardApi);
            Items.Add(newBoardItemVm);
        }

        public async Task<bool> NavigateToAsync(object args)
        {
            var boards = await GetAllBoardsAsync();

            foreach (var board in boards)
            {
                var newBoardItemVm = new BoardItemViewModel(_boardApi)
                {
                    Description = board.Description,
                    Name = board.Name,
                    Id = board.Id.ToString(),
                    IsEditable = false,
                    IsInTheDb = true

                };
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


        public void OpenBoard(BoardItemViewModel boardItem)
        {
            _eventAggregator.PublishOnUIThread(new BoardActiveMessage());
        }

        public void Handle(NewBoardForDeleteMessage message)
        {
            Items.Remove(Items.LastOrDefault());
        }
    }
}
