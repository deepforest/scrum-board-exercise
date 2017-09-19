using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeValue.ScrumBoard.Client.Models;
using CodeValue.ScrumBoard.Client.Navigation;

namespace CodeValue.ScrumBoard.Client.ViewModels
{
    public class BoardsViewModel : Screen , IBoardsViewModel
    {
        private readonly ObservableCollection<Board> _BoardsCollection;

        public BoardsViewModel()
        {
            DisplayName = "Virtual Scrum Board";
        }

        public async Task<bool> NavigateToAsync()
        {
            return await Task.Run(() => { return true; });
        }

        public async void AddNewBoard()
        {

        }
    }
}
