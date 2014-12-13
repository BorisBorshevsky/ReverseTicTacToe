namespace ReverseTicTacToeLogic
{

    public class ScoreBoard
    {
        private Scores scores;

        public void AddWinToPlayer1()
        {
            scores.Player1++;
        }

        public void AddWinToPlayer2()
        {
            scores.Player2++;
        }

        public Scores GetScores()
        {
            return scores;
        }

        public struct Scores
        {
            public int Player1 { get; set; }
            public int Player2 { get; set; }
        }
    }
}