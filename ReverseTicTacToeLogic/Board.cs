using System;
using System.Collections.Generic;
using System.Text;

namespace ReverseTicTacToeLogic
{
    public class Board
    {
        private Symbol[,] m_board;

        public Board(int size)
        {
            m_board = new Symbol[size, size];
        }

        public void initializeBoard()
        {
            for (int row = 0; row < m_board.GetLength(0); row++)
            {
                for (int column = 0; column < m_board.GetLength(1); column++)
                {
                    m_board[row, column] = Symbol.Blank;
                }
            }
        }


        public bool IsValidMove(int x, int y)
        {
            return true;
        }

        public void SetSymbol(Symbol i_symbol, int x, int y)
        {

        }
    }
}
