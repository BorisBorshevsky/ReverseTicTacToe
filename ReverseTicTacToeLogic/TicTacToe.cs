using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ReverseTicTacToeLogic
{
    public class TicTacToe
    {
        private readonly Board m_board;
        private readonly ScoreBoard m_scoreBoard;

        public Board Board { get { return m_board; }}

        public TicTacToe(int size)
        {
            m_scoreBoard = new ScoreBoard();
            m_board = new Board(size);
            m_board.InitializeBoard();
        }

        public bool TryPlayTurn(eSymbol i_PlayersSymbol, eSymbol i_OpponentSymbol)
        {
            Point computerMove = Algorithms.ArtificialIntelligenceAlgorithm.GetMove(Board, i_PlayersSymbol, i_OpponentSymbol);
            
            return TryPlayTurn(computerMove, i_PlayersSymbol);
        }

        public bool TryPlayTurn(Point i_coordinates, eSymbol i_PlayersSymbol)
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
                if (i_PlayersSymbol == eSymbol.X)
                {
                    m_scoreBoard.AddWinToPlayer2();
                }
                else
                {
                    m_scoreBoard.AddWinToPlayer1();
                }
            }

            return isPlayedSucceded;
        }


        public ScoreBoard.Scores GetScores()
        {
            return m_scoreBoard.GetScores();
        }

        public void Surrender(eSymbol i_PlayersSymbol)
        {
            if (i_PlayersSymbol == eSymbol.X)
            {
                m_scoreBoard.AddWinToPlayer2();
            }
            else
            {
                m_scoreBoard.AddWinToPlayer1();
            }
        }

        public void Restart()
        {
            Board.InitializeBoard();
        }

    }
}
