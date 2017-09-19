using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Service.DTOs
{
    public class NewTaskDetailsRequest
    {
        public string Description { get; set; }
        public string AssignedTo { get; set; }
        public int Priority { get; set; }
        public int RemainingWork { get; set; }
    }
}
