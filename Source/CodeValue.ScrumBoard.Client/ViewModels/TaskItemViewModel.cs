using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeValue.ScrumBoard.Client.Models;
using Refit;
using CodeValue.ScrumBoard.Client.Apis;
using CodeValue.ScrumBoard.Client.Common;

namespace CodeValue.ScrumBoard.Client.ViewModels
{
    public class TaskItemViewModel
    {
        public string Id { get; set; }

        public DateTime CreationTimeUtc { get; set; }

        public int Version { get; set; }

        public string CreatedBy { get; set; }

        public string AssignedTo { get; set; }

        public string Description { get; set; }

        public TaskModelStatus Status { get; internal set; }

        public int Priority { get; set; }

        public int RemainingWork { get; set; }

        public CommentModel[] Comments { get; set; }

        public IEnumerable<string> RelevantUsersList { set; get; }

        public BoardViewModel BoardContains { set; get; }

        public async void DeleteTaskAsync()
        {

            var api = RestService.For<ITaskApi>(Constants.ServerUri);
            await api.DeleteTaskAsync(Id);

            BoardContains.DeleteTask(this);
        }
    }
}
