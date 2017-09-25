using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using CodeValue.ScrumBoard.Client.Events;
using CodeValue.ScrumBoard.Client.Navigation;
using Refit;
using CodeValue.ScrumBoard.Client.Apis;
using CodeValue.ScrumBoard.Client.Models;

namespace CodeValue.ScrumBoard.Client.ViewModels
{
    public class BoardItemViewModel : Screen, IBoardItemViewModel<BoardActivePayload>
    {
        private readonly IEventAggregator _eventAggregator;
        private bool _isEditable;
        private bool _isInTheDB;

        private string _description;
        private string _name;
        public string Id { get; set; }

        public string Description
        {
            get { return _description; }

             set
            {
                _description = value;
                NotifyOfPropertyChange();
            }
        }

        public string Name
        {
            get { return _name; }

             set
            {
                _name = value;
                NotifyOfPropertyChange();
            }
        }

        public bool IsEditable
        {
            get { return _isEditable; }

            private set
            {
                _isEditable = value;
                NotifyOfPropertyChange();
            }
        }

        public BoardItemViewModel()
        {
            _isInTheDB = false;
            IsEditable = true;
            Description = "desc";
            Name = "aa";
        }

        public Task<bool> NavigateToAsync(BoardActivePayload args)
        {
            throw new NotImplementedException();
        }

        public async void SaveBoardDetailsChangesAsync(string name,string descreption)
        {
            try
            {
                var api = RestService.For<IBoardApi>("http://localhost:8080/api");

                var board = new Board()
                {
                    Description = descreption,
                    Name=name
                };

                var recievedBoard = await api.CreateBoardAsync(board);
                Id = recievedBoard.Id.ToString();
                _isInTheDB = true;
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
            if(!_isInTheDB)
            {
             //   _eventAggregator.PublishOnUIThread(new NewBoardForDelete());
            }
        }
    }
}
