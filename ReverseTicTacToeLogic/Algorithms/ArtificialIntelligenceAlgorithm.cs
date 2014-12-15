using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ReverseTicTacToeLogic.Algorithms
{
    static class ArtificialIntelligenceAlgorithm
    {
        public static Point GetMove(Board i_board, Symbol i_symbol)
        {
            Board dynamicProgrammingBoard = new Board(i_board.Size);
            Point bestMove;

            for (int row = 0; row < i_board.Size; row++)
            {
                for (int column = 0; column < i_board.Size; column++)
                {
                    bestMove = new Point(row, column);
                    if (i_board.IsValidMove(bestMove))
                    {
                        i_board.SetSymbol(i_symbol, bestMove);
                        if (i_board.HasWinner())
                        {
                            i_board.SetSymbol(Symbol.Blank, bestMove);
                        }
                        else 
                        {
                            goto EndOfLoop;
                        }
                    }


                }
            }
            EndOfLoop:
            return bestMove;
        }
    }
}

/*
 public static void ComputersMoveAI(GameBoard<eXMixDrixSymbol> i_GameBoard, out GameBoard<eXMixDrixSymbol>.Coordinate i_ComputerCoordinate, eXMixDrixSymbol i_ComputerSymbol, eXMixDrixSymbol i_PlayerSymbol)
        {
            GameBoard<eXMixDrixSymbol>.Coordinate[] goodCoordinatesArr = new GameBoard<eXMixDrixSymbol>.Coordinate[i_GameBoard.Size * i_GameBoard.Size];
            int goodCoordinateIndex = 0;
            for (int i = 0; i < i_GameBoard.Size; i++)
            {
                for (int j = 0; j < i_GameBoard.Size; j++)
                {
                    GameBoard<eXMixDrixSymbol>.Coordinate currentCoordinate = new GameBoard<eXMixDrixSymbol>.Coordinate();
                    currentCoordinate.Row = i;
                    currentCoordinate.Column = j;
                    if (i_GameBoard.IsAvailable(currentCoordinate) == GameBoard<eXMixDrixSymbol>.eAvailabiltyMsg.CellAvailable)
                    {
                        if (!IsFlushLine(i_GameBoard, currentCoordinate, i_ComputerSymbol) && !IsFlushLine(i_GameBoard, currentCoordinate, i_PlayerSymbol))
                        {
                            GameBoard<eXMixDrixSymbol>.Coordinate goodCoordinate;
                            goodCoordinate.Row = i;
                            goodCoordinate.Column = j;
                            goodCoordinatesArr[goodCoordinateIndex] = goodCoordinate;
                            goodCoordinateIndex++;
                        }
                    }
                }
            }

            if (goodCoordinateIndex == 0)
            {
                for (int i = 0; i < i_GameBoard.Size; i++)
                {
                    for (int j = 0; j < i_GameBoard.Size; j++)
                    {
                        GameBoard<eXMixDrixSymbol>.Coordinate currentCoordinate = new GameBoard<eXMixDrixSymbol>.Coordinate();
                        currentCoordinate.Row = i;
                        currentCoordinate.Column = j;
                        if (i_GameBoard.IsAvailable(currentCoordinate) == GameBoard<eXMixDrixSymbol>.eAvailabiltyMsg.CellAvailable)
                        {
                            GameBoard<eXMixDrixSymbol>.Coordinate availableCoordinate;
                            availableCoordinate.Row = i;
                            availableCoordinate.Column = j;
                            goodCoordinatesArr[goodCoordinateIndex] = availableCoordinate;
                            goodCoordinateIndex++;
                        }
                    }
                }
            }

            Random rnd = new Random();
            int oneOfGoodCoordinates = rnd.Next(0, goodCoordinateIndex - 1);
            i_ComputerCoordinate.Row = goodCoordinatesArr[oneOfGoodCoordinates].Row;
            i_ComputerCoordinate.Column = goodCoordinatesArr[oneOfGoodCoordinates].Column;
        }
*/