using System.Drawing;

namespace ReverseTicTacToeLogic
{
    public class Board
    {
        private readonly eSymbol[,] m_board;

        public int Size {
            get { return m_board.GetLength(0); } 
        }

        public Board(int size)
        {
            m_board = new eSymbol[size, size];
        }

        public eSymbol[,] GetData()
        {
            return m_board;
        }

        public void InitializeBoard()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int column = 0; column < Size; column++)
                {
                    SetSymbol(eSymbol.Blank, row, column);
                }
            }
        }

        public bool IsBoardFull()
        {
            bool isBoardFull = true;
            
            for (int row = 0; row < Size; row++)
            {
                for (int column = 0; column < Size; column++)
                {
                    if (m_board[row, column] == eSymbol.Blank)
                    {
                        isBoardFull = false;
                        goto EndOfLoop;
                    }
                }
            }
            
            EndOfLoop:
           
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

        public bool IsValidMove(Point i_Coordinates)
        {
            bool isValidMove = false;
            
            if (Size > i_Coordinates.X || Size > i_Coordinates.Y)
            {
                isValidMove = m_board[i_Coordinates.X, i_Coordinates.Y] == eSymbol.Blank;
            }

            return isValidMove;
        }

        public void SetSymbol(eSymbol i_SymbolToPlace, Point i_Coordinates)
        {
            SetSymbol(i_SymbolToPlace, i_Coordinates.X, i_Coordinates.Y);
        }
        public void SetSymbol(eSymbol i_SymbolToPlace, int i_Row, int i_Column)
        {
            m_board[i_Row, i_Column] = i_SymbolToPlace;
        }

        public eSymbol GetSymbol(Point i_Coordinates)
        {
            return GetSymbol(i_Coordinates.X, i_Coordinates.Y);
        }

        public eSymbol GetSymbol(int i_Row, int i_Column)
        {
            return m_board[i_Row, i_Column];
        }

        private bool isColumnStrightLine(int i_Column)
        {
            bool isStreightLine = true;

            eSymbol symbolToTest = GetSymbol(0, i_Column);
            for (int row = 0; row < Size; row++)
            {
                if (GetSymbol(row, i_Column) != symbolToTest || GetSymbol(row, i_Column) == eSymbol.Blank)
                {
                    isStreightLine = false;
                    break;
                }
            }

            return isStreightLine;

        }
       
        private bool isRowStreightLine(int i_Row)
        {
            bool isStreightLine = true;
            eSymbol symbolToTest = GetSymbol(i_Row, 0);
            
            for (int column = 0; column < Size; column++)
            {
                if (GetSymbol(i_Row, column) != symbolToTest || GetSymbol(i_Row, column) == eSymbol.Blank)
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
            eSymbol symbolToTest = GetSymbol(0, 0);
            
            for (int row = 0; row < Size; row++)
            {
                if (GetSymbol(row, row) != symbolToTest || GetSymbol(row, row) == eSymbol.Blank)
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

            eSymbol symbolToTest = GetSymbol(0, 0);
            for (int row = 0; row < Size; row++)
            {
                if (GetSymbol(row, Size - 1 - row) != symbolToTest || GetSymbol(row, Size - 1 - row) == eSymbol.Blank)
                {
                    isStreightLine = false;
                    break;
                }
            }

            return isStreightLine;
        }
    }
}
