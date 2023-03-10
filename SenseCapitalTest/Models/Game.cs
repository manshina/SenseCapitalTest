namespace SenseCapitalTest.Models
{
    public class Game
    {
        public int Id { get; set; }
        public int[] Field { get; set; } = { 0, 0, 0, 0, 0 };

        public int Player1 { get; set; }
        public int Player2 { get; set; }

        public int last { get; set; }

    }
}
