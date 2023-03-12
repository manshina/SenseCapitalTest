using Microsoft.AspNetCore.Mvc;
using RPG7.Models;
using SenseCapitalTest.Data;
using SenseCapitalTest.Dtos.Game;

namespace testtictoe.Services
{
    public class TickTacToeService : ITickTacToeService
    {

        private readonly IStorage _storage;
        public TickTacToeService(IStorage storage)
        {
            _storage = storage;
        }
        //создание новой игры
        public async Task<ServiceResponce<string>> CreateNewGame(int playerid)
        {

            int gameid = _storage.GetLastId();
          
            Game game = new Game() { Id = gameid };

            game.Player1id = playerid;

            _storage.AddGame(game);



            ServiceResponce<string> responce = new ServiceResponce<string>()
            {
                Data = $"Game id is: {game.Id}"
            };
            return responce;
        }
        //присоедениться к игре
        public async Task<ServiceResponce<string>> ConnectToGame(int gameid, int playerid)
        {
            ServiceResponce<string> responce = new ServiceResponce<string>();

            var game = _storage.GetGameByID(gameid);
            
            if (game == null)
            {
                responce.Message = "Game not found";
                responce.IsSuccess = false;
                return responce;
            }
            if (game.Player2id != null)
            {
                responce.Message = "Room is full";
                responce.IsSuccess = false;
                return responce;
            }
            game.Player2id = playerid;
            responce.Data = "Connected";
            return responce;
        }
        //получить поле игры на текущий момент
        public async Task<ServiceResponce<MatrixDto>> GetField(int playerid)
        {
            ServiceResponce<MatrixDto> responce = new ServiceResponce<MatrixDto>();
            var game = _storage.GetGameByPlayer(playerid);
            if (game == null)
            {
                responce.Message = "You Have no games";
                responce.IsSuccess = false;
                return responce;
               
            }
            responce.Data = game.FieldString();
            return responce;
            
        }
        
        //сделать ход
        public async Task<ServiceResponce<MatrixDto>> Move(MoveDto moveDto, int playerid)
        {
            ServiceResponce<MatrixDto> responce = new ServiceResponce<MatrixDto>();
            var game = _storage.GetGameByPlayer(playerid);

            int x = moveDto.X;
            int y = moveDto.Y;
            if (game == null)
            {
                responce.Message = "Game Not Found";
                responce.IsSuccess = false;
                return responce;
            }
            //mark - X/O
            char mark = ' ';
            if (game.Player1id == playerid)
            {
                mark = 'X';
            }
            if (game.Player2id == playerid)
            {
                mark = 'O';
            }
            if (mark == ' ')
            {
                responce.Message = "Unexpected error";
                responce.IsSuccess = false;
                return responce;
                
            }
            var result = game.Move(y, x, mark);

            responce.Message = result;
            responce.Data = game.FieldString();
            //удаление игры по ее завершению
            if (game.Winner != null)
            {
                game.IsFinished = true;
                try
                {
                    _storage.RemoveGame(game);
                }
                catch
                {

                }
                
            }
            
            return responce;
            
        }
        //досрочно закончить игру
        public async Task<ServiceResponce<string>> EndGame(int playerid)
        {
            ServiceResponce<string> responce = new ServiceResponce<string>();
            var game = _storage.GetGameByPlayer(playerid);
            if (game == null)
            {
                
                responce.Message = "You have no games";
                responce.IsSuccess = false;
                return responce;
            }
            game.IsFinished = true;
            _storage.RemoveGame(game);

            responce.Message = "Game ended";
            
            return responce;
        }
    }
}
