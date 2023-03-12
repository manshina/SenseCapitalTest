namespace SenseCapitalTest.Dtos.Game
{
    public class Game
    {
        public int Id { get; set; }
        public char[,] Field { get; set; } = new char[3, 3];

        public int? Player1id { get; set; }
        public int? Player2id { get; set; }
        public char? Winner { get; set; }
        public bool FirstMove = true;
        public int Last { get; set; }

        public bool IsFinished = false;

        public int Moves { get; set; } = 0;
        //инициальзируем поле
        public Game()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Field[i, j] = '.';
                }
            }
        }
        //странный способ представления поля в виде 3 строк
        public MatrixDto FieldString()
        {
            var mat = new MatrixDto();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i == 0)
                    {
                        mat.line1 += Field[i, j];
                    }
                    if (i == 1)
                    {
                        mat.line2 += Field[i, j];
                    }
                    if (i == 2)
                    {
                        mat.line3 += Field[i, j];
                    }

                }

            }
            return mat;
        }
        //ход
        //mark - X/O
        public string Move(int x, int y, char mark)
        {
            if (FirstMove && mark != 'X')
            {
                return "Not your turn";
            }
            if (Player1id == null || Player2id == null)
            {
                return "Wait for other player";
            }
            if (x > 2 || y > 2)
            {
                return "Position is out of range!";

            }
            if (IsFinished)
            {
                return "Game is finished!";

            }
            if (Field[x, y] != '.')
            {
                return "Cell is busy!";

            }
            
            Field[x, y] = mark;
            FirstMove = false;
            if (checkWinner(mark))
            {
                Winner = mark;
                IsFinished = true;

                return "winnner" + Winner;
            }
            Last = mark;
            //ответ не приходит, пока второй игрок не сделает ход
            while (Last == mark)
            {
                if (Moves == 7)
                {
                    IsFinished = true;
                    return "Draw";
                }
                if (IsFinished)
                {

                    return "winnner" + Winner;
                }

            }
            Moves++;
            return " ";

        }
        private bool checkWinner(char mark)
        {

            // * _ _
            // _ * _
            // _ _ *
            bool trigger = true;
            for (int i = 0; i < 3; i++)
            {
                if (Field[i, i] != mark)
                    trigger = false;
            }
            if (trigger)
            {

                return true;
            }
            // _ _ *
            // _ * _
            // * _ _
            trigger = true;
            for (int i = 0; i < 3; i++)
            {
                if (Field[i, 2 - i] != mark)
                    trigger = false;
            }
            if (trigger)
            {

                return true;
            }
            // _ * _
            // _ * _
            // _ * _
            bool triggerI = true;
            bool triggerJ = true;
            for (int i = 0; i < 3; i++)
            {
                triggerJ = true;
                for (int j = 0; j < 3; j++)
                {
                    if (Field[i, j] != mark)
                        triggerJ = false;
                }
                if (triggerJ)
                {

                    return true;
                }
            }
            // _ _ _
            // * * *
            // _ _ _
            for (int j = 0; j < 3; j++)
            {
                triggerI = true;
                for (int i = 0; i < 3; i++)
                {
                    if (Field[i, j] != mark)
                        triggerI = false;
                }
                if (triggerI)
                {

                    return true;
                }
            }
            return false;
        }


    }
}
