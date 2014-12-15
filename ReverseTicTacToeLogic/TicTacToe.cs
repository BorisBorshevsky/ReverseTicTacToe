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
            m_scores = new ScoreBoard();
            m_board = new Board(size);
            m_board.initializeBoard();
        }

        public bool PlayTurn(Symbol i_PlayersSymbol)
        {
            bool isPlayedSucceded = true;
            Point computerMove = Algorithms.ArtificialIntelligenceAlgorithm.GetMove(Board, i_PlayersSymbol);

            return PlayTurn(computerMove, i_PlayersSymbol);
        }

        public bool PlayTurn(Point i_coordinates, Symbol i_PlayersSymbol)
        {
            bool isPlayedSucceded = true;
            
            if (!Board.IsValidMove(i_coordinates))
            {
                isPlayedSucceded = false;
            }
            else
            {
                Board.SetSymbol(i_PlayersSymbol, i_coordinates);
            }

            if (Board.HasWinner())
            {
                if (i_PlayersSymbol == Symbol.X)
                {
                    m_scores.AddWinToPlayer2();
                }
                else
                {
                    m_scores.AddWinToPlayer1();
                }
            }

            return isPlayedSucceded;
        }





        public ScoreBoard.Scores GetScores()
        {
            return m_scores.GetScores();
        }

        public void Surrender(Symbol i_PlayersSymbol)
        {
            if (i_PlayersSymbol == Symbol.X)
            {
                m_scores.AddWinToPlayer2();
            }
            else
            {
                m_scores.AddWinToPlayer1();
            }
            
        }


        public void Restart()
        {
            Board.initializeBoard();
        }

        public bool isStreightLineAchieved { get; set; }
    }
}
