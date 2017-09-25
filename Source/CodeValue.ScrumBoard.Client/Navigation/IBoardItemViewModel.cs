using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeValue.ScrumBoard.Client.Events;

namespace CodeValue.ScrumBoard.Client.Navigation
{
    public interface IBoardItemViewModel <T>: INavigation<T>
    {
    }
}
