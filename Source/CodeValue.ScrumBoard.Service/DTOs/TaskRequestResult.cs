using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task = CodeValue.ScrumBoard.Service.Entities.Task;

namespace CodeValue.ScrumBoard.Service.DTOs
{
    public class TaskRequestResult
    {
        public IEnumerable<Task> Tasks { get; set; }
    }
}
