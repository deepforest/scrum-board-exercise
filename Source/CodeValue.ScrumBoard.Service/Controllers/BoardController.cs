using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CodeValue.ScrumBoard.Service.Entities;
using CodeValue.ScrumBoard.Service.Managers;
using MongoDB.Driver;
using CodeValue.ScrumBoard.Service.DTOs;

namespace CodeValue.ScrumBoard.Service.Controllers
{
    [Route("api/[controller]")]
    public class BoardController : Controller
    {
        private IBoardManager _boardManager;

        public BoardController(IBoardManager boardManager)
        {
            _boardManager = boardManager;
        }

        [HttpGet]
        public IEnumerable<Board> GetBoards()
        {
            var boards = _boardManager.GetBoards();
            return boards;
        }

        [HttpPost]
        public async Task<Board> CreateBoard([FromBody] NewBoardDetails board)
        {
            return await _boardManager.CreateBoardAsync(board);
        }

        [HttpPut]
        public async Task<bool> UpdateBoardDetails([FromBody] Board boardToUpdate)
        {
            var result = await _boardManager.UpdateBoardAsync(boardToUpdate);
            return result.IsAcknowledged;
        }
    }
}
