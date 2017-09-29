using CodeValue.ScrumBoard.Client.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Client.Apis
{
    [Headers("Authorization: Bearer")]
    public interface IBoardApi
    {
        [Get("/task/{boardId}}")]
        Task<IEnumerable<TaskModel>> GetBoardTasksAsync(string boardId);

        [Get("/Board")]
        Task<IEnumerable<Board>> GetBoardsAsync();

        [Post("/Board")]
        Task<Board> CreateBoardAsync(Board board);
    }
}
