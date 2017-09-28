using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CodeValue.ScrumBoard.Service.Managers;
using CodeValue.ScrumBoard.Service.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CodeValue.ScrumBoard.Service.Controllers
{
    /// <summary>
    /// Virtual scrum board APIs.
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BoardController : Controller
    {
        private readonly IBoardManager _boardManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="boardManager"></param>
        public BoardController(IBoardManager boardManager)
        {
            _boardManager = boardManager;
        }

        /// <summary>
        /// Get all virtual scrum boards.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Create a new virtual scrum board.
        /// </summary>
        /// <param name="newBoard">New scurm board properties.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<NewBoardDetails> CreateBoard([FromBody] NewBoardDetails newBoard)
        {
            var board = await _boardManager.CreateBoardAsync(newBoard);
            newBoard.Id = board.Id.ToString();
            return newBoard;
        }

        /// <summary>
        /// Update virtual scrum board details.
        /// </summary>
        /// <param name="boardToUpdate">Properties of the virtual scrum board should be updated.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> UpdateBoardDetails([FromBody] NewBoardDetails boardToUpdate)
        {
            var result = await _boardManager.UpdateBoardAsync(boardToUpdate);
            return result;
        }

        /// <summary>
        /// Delete virtual scrum board.
        /// </summary>
        /// <param name="boardId">The unique ID of the virtual scrum board.</param>
        /// <returns></returns>
        [HttpDelete("{boardId}")]
        public async Task<bool> DeleteBoard(string boardId)
        {
            var result = await _boardManager.RemoveBoardAsync(boardId);
            return result;
        }
    }
}
