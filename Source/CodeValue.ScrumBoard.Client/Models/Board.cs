using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Client.Models
{
    public class Board
    {
        public string Description { get; set; }

        public string Name { get; set; }

        public string[] TeamMembers { get; set; }
    }
}
