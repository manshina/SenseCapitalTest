using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RPG7.Models;
using SenseCapitalTest.Data;
using SenseCapitalTest.Dtos.Game;


using System.Security.Claims;
using testtictoe.Services;

namespace SenseCapitalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    //контроллер игры 
    public class TicTacToeController : ControllerBase
    {
        
        
        
        private readonly ITickTacToeService _tickTacToeService;
        public TicTacToeController(ITickTacToeService tickTacToeService)
        {
      
            _tickTacToeService = tickTacToeService;
        }
        
        
        [HttpGet("CreateGame")]
        public async Task<ActionResult<ServiceResponce<string>>> CreateNewGame()
        {
            var playerid = GetPlayerId();
            var result = await _tickTacToeService.CreateNewGame(playerid);
            return Ok(result);

        }
        [HttpPost("ConnectToGame")]
        public async Task<ActionResult<ServiceResponce<string>>> ConnectToGame([FromBody]int gameid)
        {
            var playerid = GetPlayerId();
            var result = await _tickTacToeService.ConnectToGame(gameid, playerid);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetGameField")]
        public async Task<ActionResult<ServiceResponce<string>>> GetField()
        {
            var playerid = GetPlayerId();
            var result = await _tickTacToeService.GetField(playerid);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("MakeMove")]
        public async Task<ActionResult<ServiceResponce<string>>> Move([FromBody] MoveDto moveDto)
        {
            var playerid = GetPlayerId();
            
            var result = await _tickTacToeService.Move(moveDto, playerid);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("EndGame")]
        public async Task<ActionResult<ServiceResponce<string>>> EndGame()
        {
            var playerid = GetPlayerId();
            var result = await _tickTacToeService.EndGame(playerid);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        private int GetPlayerId()
        {
            return Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }
}
