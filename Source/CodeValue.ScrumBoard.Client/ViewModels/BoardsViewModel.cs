using Caliburn.Micro;
using CodeValue.ScrumBoard.Client.Models;
using CodeValue.ScrumBoard.Client.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CodeValue.ScrumBoard.Client.Events;
using Refit;
using CodeValue.ScrumBoard.Client.Apis;

namespace CodeValue.ScrumBoard.Client.ViewModels
{
    public class BoardsViewModel : Conductor<Screen>.Collection.AllActive, IBoardsViewModel<object>, IHandle<NewBoardForDelete>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly Func<IBoardItemViewModel<BoardActivePayload>> _boardItemViewModelCreator;

        public BoardsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            //  _boardItemViewModelCreator = boardItemViewModelCreator;
        }

        public async Task AddNewBoard()
        {
            var newBoardItemVm = new BoardItemViewModel();
            await Task.Run(() => Items.Add(newBoardItemVm));
        }

        public async Task<bool> NavigateToAsync(object args)
        {
            var boards = await GetAllBoardsAsync();

            foreach (var board in boards)
            {
                var newBoardItemVm = new BoardItemViewModel()
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
                var api = RestService.For<IBoardApi>("http://localhost:8080/api");
                var boards = await api.GetBoardsAsync();
                return boards;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Board>();
            }
        }


        public void OpenBoard(BoardItemViewModel boardItem)
        {
            _eventAggregator.PublishOnUIThread(new BoardActivePayload());
        }

        public void Handle(NewBoardForDelete message)
        {
            Items.Remove(Items.LastOrDefault());
        }
    }
}
