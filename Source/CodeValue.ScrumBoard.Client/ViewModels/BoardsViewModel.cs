using Caliburn.Micro;
using CodeValue.ScrumBoard.Client.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Client.ViewModels
{
    public class BoardsViewModel : Screen
    {
        private readonly ObservableCollection<Board> _BoardsCollection;

        public BoardsViewModel()
        {
            DisplayName = "Virtual Scrum Board";
        }

        public async void AddNewBoard()
        {

        }
    }
}
