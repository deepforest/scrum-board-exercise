using CodeValue.ScrumBoard.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Service.DTOs
{
    public class UpdateTask
    {
        public string Id { get; set; }
        public string AssignedTo { get; set; }
        public string Description { get; set; }
        public Entities.TaskStatus? Status { get; set; }
        public int? Priority { get; set; }
        public int? RemainingWork { get; set; }
        public Comment[] Comments { get; set; }
    }
}
