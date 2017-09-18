using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Client.Models
{
    public class TaskModel
    {
        public string CreatedBy { get; set; }

        public string AssignedTo { get; set; }

        public TaskStatus Status { get; set; }

        public int Priority { get; set; }

        public int RemainingWork { get; set; }

        public CommentModel[] Comments { get; set; }

    }
}
