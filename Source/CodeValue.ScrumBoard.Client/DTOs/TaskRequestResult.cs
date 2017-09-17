using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeValue.ScrumBoard.Client.Models;

namespace CodeValue.ScrumBoard.Client.DTOs
{
    public class TaskResponse
    {
        public IEnumerable<TaskModel> Tasks;
    }
}
