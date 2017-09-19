using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using CodeValue.ScrumBoard.Client.Events;
using CodeValue.ScrumBoard.Client.Navigation;

namespace CodeValue.ScrumBoard.Client.ViewModels
{
    public class BoardItemViewModel : Screen, IBoardItemViewModel<BoardActivePayload>
    {
        public Task<bool> NavigateToAsync(BoardActivePayload args)
        {
            throw new NotImplementedException();
        }
    }
}
