namespace ReverseTicTacToeLogic
{
    public class ScoreBoard
    {
        private Scores m_scores;

        public ScoreBoard(Player i_Player1, Player i_Player2)
        {
            m_scores = new Scores(i_Player1, i_Player2);
        }

        public void AddWinToPlayer1()
        {
            m_scores.Player1.AddWinToScore();
        }

        public void AddWinToPlayer2()
        {
            m_scores.Player2.AddWinToScore();
        }

        public Scores GetScores()
        {
            return m_scores;
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