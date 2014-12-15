namespace ReverseTicTacToeLogic
{

    public class ScoreBoard
    {
        private Scores m_scores;

        public void AddWinToPlayer1()
        {
            m_scores.Player1++;
        }

        public void AddWinToPlayer2()
        {
            m_scores.Player2++;
        }

        public Scores GetScores()
        {
            return m_scores;
        }

        public struct Scores
        {
            public int Player1 { get; set; }
            public int Player2 { get; set; }
        }
    }
}