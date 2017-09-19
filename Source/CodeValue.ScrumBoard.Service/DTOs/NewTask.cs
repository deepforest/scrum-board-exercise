using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Service.DTOs
{
    public class NewTask
    {
        public string CreatedBy { get; set; }
        public string Description { get; set; }
        public string AssignedTo { get; set; }
        public int Priority { get; set; }
        public int RemainingWork { get; set; }
    }
}
