using CodeValue.ScrumBoard.Client.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Client.ViewModels
{
    public class TaskViewModel  : ITaskViewModel
    {

        public async Task<bool> NavigateToAsync<T>(T args)
        {
            return await Task.Run(() => { return true; });
        }
    }
}
