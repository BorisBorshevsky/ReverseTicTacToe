namespace ReverseTicTacToeLogic
{
    public class ScoreBoard
    {
        private readonly Scores r_scores;

        public ScoreBoard(Player i_Player1, Player i_Player2)
        {
            r_scores = new Scores(i_Player1, i_Player2);
        }

        public void AddWinToPlayer1()
        {
            r_scores.Player1.AddWinToScore();
        }

        public void AddWinToPlayer2()
        {
            r_scores.Player2.AddWinToScore();
        }

        public Scores GetScores()
        {
            return r_scores;
        }

        public class Scores
        {
            public Scores(Player i_Player1, Player i_Player2)
            {
                Player1 = i_Player1;
                Player2 = i_Player2;
            }

            public Player Player1 { get; private set; }

            public Player Player2 { get; private set; }
        }
    }
}