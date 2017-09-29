﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Client.Messages
{
    public class BoardForDeleteMessage
    {
        public string BoardId{ get; set; }
       

        public BoardForDeleteMessage(string boardId)
        {
            BoardId = boardId;
        }
    }
}
