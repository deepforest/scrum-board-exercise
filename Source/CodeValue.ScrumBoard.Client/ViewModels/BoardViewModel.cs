using Caliburn.Micro;
using CodeValue.ScrumBoard.Client.Apis;
using CodeValue.ScrumBoard.Client.Events;
using CodeValue.ScrumBoard.Client.Models;
using CodeValue.ScrumBoard.Client.Navigation;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Client.ViewModels
{
    public class BoardViewModel : IBoardViewModel<BoardActivePayload>
    {
        public BoardViewModel()
        {
            TodoTasks = new BindableCollection<TaskItemViewModel>();
            DoingTasks = new BindableCollection<TaskItemViewModel>();
            DoneTasks = new BindableCollection<TaskItemViewModel>();
        }
        
        public async Task CreateTask()
        {

        }

        public string BoardName
        {
            get;
            private set;
        }

        public IObservableCollection<TaskItemViewModel> TodoTasks { get; private set; }
        public IObservableCollection<TaskItemViewModel> DoingTasks { get; private set; }
        public IObservableCollection<TaskItemViewModel> DoneTasks { get; private set; }

        public async Task<bool> NavigateToAsync(BoardActivePayload payload)
        {
            var tasks = await GetAllBoardTasksAsync(payload.BoardId);
            BoardName = payload.BoardName;
            var taskVMs = tasks.Select(i => new TaskItemViewModel());
            TodoTasks.Clear();
            DoingTasks.Clear();
            DoneTasks.Clear();
            TodoTasks.AddRange(taskVMs.Where(x => x.Status == TaskModelStatus.Todo));
            DoingTasks.AddRange(taskVMs.Where(x => x.Status == TaskModelStatus.Doing));
            DoneTasks.AddRange(taskVMs.Where(x => x.Status == TaskModelStatus.Done));
            return true;
        }

        private async Task<IEnumerable<TaskModel>> GetAllBoardTasksAsync(string boardId)
        {
            try
            {
                var api = RestService.For<IBoardApi>("http://localhost:8080/api");
                var tasks = await api.GetBoardTasksAsync(boardId);
                return tasks;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<TaskModel>();
            }
        }

        public void DeleteTask(TaskItemViewModel taskToDelete)
        {
            TodoTasks.Remove(taskToDelete);
            DoingTasks.Remove(taskToDelete);
            DoneTasks.Remove(taskToDelete);
        }
    }
}
