using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CodeValue.ScrumBoard.Service.Entities;
using CodeValue.ScrumBoard.Service.Managers;
using MongoDB.Driver;

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

        //[HttpGet]
        //public async Task<IEnumerable<Board>> GetBoards()
        //{

        //}

        //[HttpPost]
        //public async Task<Board> CreateBoard([FromBody] Board student)
        //{

        //}

        [HttpPut]
        public async Task<bool> UpdateBoardDetails([FromBody] Board boardToUpdate)
        {
            var manager = new BoardManager();
            var result = await manager.UpdateBoard(boardToUpdate);

            return result.IsAcknowledged;
        }
    }
}
