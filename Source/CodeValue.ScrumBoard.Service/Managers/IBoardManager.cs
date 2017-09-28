using CodeValue.ScrumBoard.Service.DTOs;
using CodeValue.ScrumBoard.Service.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Service.Managers
{
    public interface IBoardManager
    {
        IEnumerable<Board> GetBoards();
        Task<Board> CreateBoardAsync(NewBoardDetails board);
        Task<bool> UpdateBoardAsync(NewBoardDetails board);
        Task<bool> RemoveBoardAsync(string boardId);
    }
}