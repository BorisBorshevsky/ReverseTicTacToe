using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ReverseTicTacToeLogic
{
    public class Board
    {
        private Symbol[,] m_board;

        public int Size { get { return m_board.GetLength(0); } }

        public Board(int size)
        {
            m_board = new Symbol[size, size];
        }

        public Symbol[,] GetData()
        {
            return m_board;
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

        public bool IsBoardFull()
        {
            bool isBoardFull = true;
            
            for (int row = 0; row < m_board.GetLength(0); row++)
            {
                for (int column = 0; column < m_board.GetLength(1); column++)
                {
                    if (m_board[row, column] == Symbol.Blank)
                    {
                        isBoardFull = false;
                        break;
                    }
                }
            }
           
            return isBoardFull;
        }

        public bool HasWinner()
        {
            bool isStreightLineAchieved = false;

            for (int index = 0; index < Size; index++)
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


        public bool IsValidMove(Point i_coordinates)
        {
            bool isValidMove = false;
            
            if (m_board.GetLength(0) > i_coordinates.X || m_board.GetLength(1) > i_coordinates.Y)
            {
                isValidMove = m_board[i_coordinates.X, i_coordinates.Y] == Symbol.Blank;
            }

            return isValidMove;
        }

        public void SetSymbol(Symbol i_symbol, Point i_coordinates)
        {
            if (IsValidMove(i_coordinates))
            {
                m_board[i_coordinates.X, i_coordinates.Y] = i_symbol;
            }
        }
        public Symbol GetSymbol(Point i_coordinates)
        {
            return m_board[i_coordinates.X, i_coordinates.Y];
        }

        public Symbol GetSymbol(int i_row, int i_column)
        {
            return m_board[i_row, i_column];
        }


        private bool isColumnStrightLine(int i_column)
        {
            bool isStreightLine = true;

            Symbol symbolToTest = GetSymbol(0, i_column);
            for (int row = 0; row < Size; row++)
            {
                if (GetSymbol(row, i_column) != symbolToTest || GetSymbol(row, i_column) == Symbol.Blank)
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

            Symbol symbolToTest = GetSymbol(i_row, 0);
            for (int column = 0; column < Size; column++)
            {
                if (GetSymbol(i_row, column) != symbolToTest || GetSymbol(i_row, column) == Symbol.Blank)
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

            Symbol symbolToTest = GetSymbol(0, 0);
            for (int row = 0; row < Size; row++)
            {
                if (GetSymbol(row, row) != symbolToTest || GetSymbol(row, row) == Symbol.Blank)
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

            Symbol symbolToTest = GetSymbol(0, 0);
            for (int row = 0; row < Size; row++)
            {
                if (GetSymbol(row, Size - 1 - row) != symbolToTest || GetSymbol(row, Size - 1 - row) == Symbol.Blank)
                {
                    isStreightLine = false;
                    break;
                }
            }

            return isStreightLine;
        }



    }
}
