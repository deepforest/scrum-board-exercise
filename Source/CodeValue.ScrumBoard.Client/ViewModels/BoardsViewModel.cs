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

namespace CodeValue.ScrumBoard.Client.ViewModels
{
    public class BoardsViewModel : Screen , IBoardsViewModel<object>
    {
        private readonly ObservableCollection<Board> _BoardsCollection;

        public BoardsViewModel()
        {
            
        }

       

        public async void AddNewBoard()
        {

        }

        public async Task<bool> NavigateToAsync(object args)
        {
            return await Task.Run(() => {  return true; });
        }
    }
}
