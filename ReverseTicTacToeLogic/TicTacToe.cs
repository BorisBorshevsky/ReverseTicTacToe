using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ReverseTicTacToeLogic
{
    public class TicTacToe
    {
        public Board m_board { get; private set; }
        private ScoreBoard m_scores;

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

        public bool IsBoardFull()
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
