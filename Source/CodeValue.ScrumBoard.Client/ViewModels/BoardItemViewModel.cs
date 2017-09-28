using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using CodeValue.ScrumBoard.Client.Navigation;
using Refit;
using CodeValue.ScrumBoard.Client.Apis;
using CodeValue.ScrumBoard.Client.Models;
using CodeValue.ScrumBoard.Client.Messages;

namespace CodeValue.ScrumBoard.Client.ViewModels
{
    public class BoardItemViewModel : Screen, IBoardItemViewModel<BoardActiveMessage>
    {
        
        private bool _isEditable;
        private bool _isInTheDb;

        private string _description;
        private string _name;
        private readonly IBoardApi _boardApi;

        public string Id { get; set; }

        public BoardItemViewModel(IBoardApi boardApi)
        {
            IsInTheDb = false;
            IsEditable = true;
            _description = "Project Description";
            _name = "Project Name";
            _boardApi = boardApi;
        }

        public string Description
        {
            get => _description;

            set
            {
                _description = value;
                NotifyOfPropertyChange();
            }
        }

        public string Name
        {
            get => _name;

            set
            {
                _name = value;
                NotifyOfPropertyChange();
            }
        }

        public bool IsEditable
        {
            get => _isEditable;

            set
            {
                _isEditable = value;
                NotifyOfPropertyChange();
            }
        }

        public bool IsInTheDb
        {
            get => _isInTheDb;
            set
            {
                _isInTheDb = value;
                NotifyOfPropertyChange();
            }
        }

        public Task<bool> NavigateToAsync(BoardActiveMessage args)
        {
            throw new NotImplementedException();
        }

        public async void SaveBoardDetailsChangesAsync(string name, string descreption)
        {
            try
            {
                var board = new Board()
                {
                    Description = descreption,
                    Name = name
                };

                var recievedBoard = await _boardApi.CreateBoardAsync(board);
                Id = recievedBoard.Id.ToString();
                IsInTheDb = true;
                IsEditable = false;
            }
            catch (Exception ex)
            {
            }
        }

        public void EditBoard()
        {
            IsEditable = true;
        }

        public void DiscardBoardDetailsChanges()
        {
            if (!IsInTheDb)
            {
                //   _eventAggregator.PublishOnUIThread(new NewBoardForDelete());
            }
        }
    }
}
