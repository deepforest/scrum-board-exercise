using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Client.Events
{
    /// <summary>
    /// This event is fired when a new board is selected.
    /// </summary>
    public class BoardActivePayload
    {
        public string BoardId { get; set; }
    }
}
