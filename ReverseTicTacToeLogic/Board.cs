﻿using System;
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


        public bool IsValidMove(Point i_coordinates)
        {
            bool isValidMove = false;
            
            if (m_board.GetLength(0) > i_coordinates.X || m_board.GetLength(1) > i_coordinates.Y)
            {
                isValidMove = m_board[i_coordinates.X, i_coordinates.Y] != Symbol.Blank;
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


    }
}
