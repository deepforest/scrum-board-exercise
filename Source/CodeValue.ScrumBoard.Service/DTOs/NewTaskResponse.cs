using CodeValue.ScrumBoard.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeValue.ScrumBoard.Service.DTOs
{
    public class NewTaskResponse
    {
        public string Name { get; set; }
        public TaskStatus Status { get; set; }
    }
}
