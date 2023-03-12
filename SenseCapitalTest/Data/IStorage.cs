using SenseCapitalTest.Dtos.Game;

namespace SenseCapitalTest.Data
{
    public interface IStorage
    {
        public List<Game> Games { get; set; }
        public Game GetGameByID(int id);

        public Game GetGameByPlayer(int id);
        public void AddGame(Game game);
        public void RemoveGame(Game game);
        public int GetLastId();
    }
}
