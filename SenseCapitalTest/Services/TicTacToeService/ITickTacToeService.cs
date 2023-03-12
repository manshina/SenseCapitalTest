using RPG7.Models;
using SenseCapitalTest.Dtos.Game;

namespace testtictoe.Services
{
    public interface ITickTacToeService
    {
        public  Task<ServiceResponce<string>> CreateNewGame(int playerid);
        public  Task<ServiceResponce<string>> ConnectToGame(int gameid, int playerid);
        public Task<ServiceResponce<MatrixDto>> GetField(int playerid);
        public  Task<ServiceResponce<MatrixDto>> Move(MoveDto moveDto, int playerid);
        public Task<ServiceResponce<string>> EndGame(int playerid);

    }
}
