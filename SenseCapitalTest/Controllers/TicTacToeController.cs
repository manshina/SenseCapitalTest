using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SenseCapitalTest.Data;
using SenseCapitalTest.Models;

namespace SenseCapitalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicTacToeController : ControllerBase
    {
        private readonly Storage storage;
        public TicTacToeController(Storage storage)
        {
            this.storage = storage;
        }
        [HttpGet]
        public async Task<IActionResult> CreateNewGame(int player1Id)
        {
            var game = new Game() { Id= 1 };
            
            game.Player1 = player1Id;
            storage.Games.Add(game);

            return Ok(game.Id);
        }
        [HttpPost("connect")]
        public async Task<IActionResult> ConnectToGame(int gameid, int player2Id)
        {
            var game = storage.Games.FirstOrDefault(g => g.Id == gameid);
            game.Player2 = player2Id;
            return Ok(game.Id);
        }

        [HttpPost("move")]
        public async Task<IActionResult> Move(int playerId, int gameId, int move1)
        {
            var game = storage.Games.FirstOrDefault(g => g.Id == gameId);
            game.Field[move1] = playerId;
            game.last = playerId;
            while (game.last == playerId)
            {
                game = storage.Games.FirstOrDefault(g => g.Id == gameId);
                if (game.last != playerId)
                {
                    break;
                }
            }
            return Ok(game.Field);
        }
    }
}
