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
            m_board.initializeBoard();
        }
                
        public bool PlayTurn()
        {
            return false;
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

            if (HasWinner())
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

        public bool HasWinner()
        {
            bool isStreightLineAchieved = false;
            
            for (int index = 0; index < Board.Size; index++)
            {
                if (isRowStreightLine(index) || isColumnStrightLine(index))
                {
                    isStreightLineAchieved = true;
                    break;
                }    
            }

            if (isAntiDiagonalStreightLine() || isDiagonalStreightLine())
            {
                isStreightLineAchieved = true;
            }

            return isStreightLineAchieved;
        }

        private bool isColumnStrightLine(int i_column)
        {
            bool isStreightLine = true;

            Symbol symbolToTest = Board.GetSymbol(0, i_column);
            for (int row = 0; row < Board.Size; row++)
            {
                if (Board.GetSymbol(row, i_column) != symbolToTest || Board.GetSymbol(row, i_column) == Symbol.Blank)
                {
                    isStreightLine = false;
                    break;
                }
            }
            
            return isStreightLine;

        }
        private bool isRowStreightLine(int i_row)
        {
            bool isStreightLine = true;

            Symbol symbolToTest = Board.GetSymbol(i_row, 0);
            for (int column = 0; column < Board.Size; column++)
            {
                if (Board.GetSymbol(i_row, column) != symbolToTest || Board.GetSymbol(i_row, column) == Symbol.Blank)
                {
                    isStreightLine = false;
                    break;
                }
            }
            
            return isStreightLine;
        }
        private bool isDiagonalStreightLine()
        {
            bool isStreightLine = true;

            Symbol symbolToTest = Board.GetSymbol(0, 0);
            for (int row = 0; row < Board.Size; row++)
            {
                if (Board.GetSymbol(row, row) != symbolToTest || Board.GetSymbol(row, row) == Symbol.Blank)
                {
                    isStreightLine = false;
                    break;
                }
            }

            return isStreightLine;
        }
        private bool isAntiDiagonalStreightLine()
        {
            bool isStreightLine = true;

            Symbol symbolToTest = Board.GetSymbol(0, 0);
            for (int row = 0; row < Board.Size; row++)
            {
                if (Board.GetSymbol(row, Board.Size - 1 - row) != symbolToTest || Board.GetSymbol(row, Board.Size - 1 - row) == Symbol.Blank)
                {
                    isStreightLine = false;
                    break;
                }
            }

            return isStreightLine;
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


        public bool isStreightLineAchieved { get; set; }
    }
}
