using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ReverseTicTacToeLogic
{
    public class TicTacToe
    {
        private Board m_board;
        private ScoreBoard m_scores;

        public Board Board { get { return m_board; }}

        public TicTacToe(int size)
        {
            m_board = new Board(size);
        }
                
        public bool PlayTurn()
        {
            return false;
        }

        public bool PlayTurn(int x, int y)
        {   
            return false;
        }

        public bool IsGameFinished()
        {
            return true;
        }


        public ScoreBoard.Scores GetScores()
        {
            return m_scores.GetScores();
        }

        public void Surrender()
        {

        }

    }
}
