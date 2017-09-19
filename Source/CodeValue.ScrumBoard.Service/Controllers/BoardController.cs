using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CodeValue.ScrumBoard.Service.Entities;
using CodeValue.ScrumBoard.Service.Managers;
using MongoDB.Driver;
using CodeValue.ScrumBoard.Service.DTOs;
using MongoDB.Bson;

namespace CodeValue.ScrumBoard.Service.Controllers
{
    [Route("api/[controller]")]
    public class BoardController : Controller
    {
        private readonly IBoardManager _boardManager;

        public BoardController(IBoardManager boardManager)
        {
            _boardManager = boardManager;
        }

        [HttpGet]
        public IEnumerable<NewBoardDetails> GetBoards()
        {
            var boards = _boardManager.GetBoards();
            foreach (var board in boards)
            {
                yield return new NewBoardDetails()
                {
                    Description = board.Description,
                    Name = board.Name,
                    Id = board.Id.ToString()
                };
            }
        }

        [HttpPost]
        public async Task<NewBoardDetails> CreateBoard([FromBody] NewBoardDetails newBoard)
        {
            var board = await _boardManager.CreateBoardAsync(newBoard);
            newBoard.Id = board.Id.ToString();
            return newBoard;
        }

        [HttpPut]
        public async Task<bool> UpdateBoardDetails([FromBody] NewBoardDetails boardToUpdate)
        {
            var result = await _boardManager.UpdateBoardAsync(boardToUpdate);
            return result;
        }

        [HttpDelete("{boardId}")]
        public async Task<bool> DeleteBoard(string boardId)
        {
            var result = await _boardManager.RemoveBoardAsync(boardId);
            return result;
        }
    }
}
