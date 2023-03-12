using Microsoft.Identity.Client.Extensions.Msal;
using SenseCapitalTest.Dtos.Game;

namespace SenseCapitalTest.Data
{
    public class Storage : IStorage
    {
        public List<Game> Games { get; set; } = new List<Game>();

        public Game GetGameByID(int id)
        {
            return Games.FirstOrDefault(g => g.Id == id);
        }

        public Game GetGameByPlayer(int id)
        {
            return Games.FirstOrDefault(g => g.Player1id == id || g.Player2id == id);
        }
        public void AddGame(Game game)
        {
            Games.Add(game);
        }
        public void RemoveGame(Game game)
        {
            Games.Remove(game);
        }

        public int GetLastId()
        {
            if (Games.Count == 0)
            {
                return 1;
            }
            
            return Games.Count() + 1;
            
        }
    }
}
