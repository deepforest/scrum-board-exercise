using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Client.ViewModels
{
    class BoardViewModel
    {
        public BoardViewModel()
        {
            TodoTasks = new ObservableCollection<TaskModel>();
            DoingTasks = new ObservableCollection<TaskModel>();
            DoneTasks = new ObservableCollection<TaskModel>();
        }

        public ObservableCollection<TaskModel> TodoTasks { get; private set; }
        public ObservableCollection<TaskModel> DoingTasks { get; private set; }
        public ObservableCollection<TaskModel> DoneTasks { get; private set; }

        //public async Task GetAllStudentsAsync()
        //{
        //    try
        //    {
        //        var api = RestService.For<IBoardApi>("http://localhost:61151/api");
        //        var tasks = await api.GetBoardTasksAsync();
        //        if (tasks == null)
        //            return;

        //        //foreach (var student in students)
        //        //    Students.Add(student);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
    }
}
