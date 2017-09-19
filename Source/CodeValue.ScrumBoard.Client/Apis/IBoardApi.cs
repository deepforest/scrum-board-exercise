﻿using CodeValue.ScrumBoard.Client.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Client.Apis
{
    interface IBoardApi
    {
        [Get("/tasks}")]
        Task<TaskModel> GetBoardTasksAsync(int board_id);
    }
}
