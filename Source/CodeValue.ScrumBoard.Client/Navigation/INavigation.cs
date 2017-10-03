using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Client.Navigation
{
    public interface INavigation<in T>  
    {        
        Task<bool> NavigateToAsync(T args);
    }
}
