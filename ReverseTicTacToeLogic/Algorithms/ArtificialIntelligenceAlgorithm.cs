using System;
using System.Collections.Generic;
using System.Drawing;

namespace ReverseTicTacToeLogic.Algorithms
{
    public static class ArtificialIntelligenceAlgorithm
    {
        public static Point GetMove(Board i_Board, eSymbol i_CurrentUserSymbol, eSymbol i_OpponentSymbol)
        {
            List<Point> availableMoves = new List<Point>();
            List<Point> goodMoves = new List<Point>();
            List<Point> bestMoves = new List<Point>();
            Point calculatedBestMove;

            for (int row = 0; row < i_Board.Size; row++)
            {
                for (int column = 0; column < i_Board.Size; column++)
                {
                    Point currentMove = new Point(row, column);
                    if (i_Board.GetCellState(currentMove) == eCellState.Empty)
                    {
                        availableMoves.Add(currentMove);
                        if (!checkIfMoveEndsTheGame(i_Board, currentMove, i_CurrentUserSymbol))
                        {
                            goodMoves.Add(currentMove);
                            if (!checkIfMoveEndsTheGame(i_Board, currentMove, i_OpponentSymbol))
                            {
                                bestMoves.Add(currentMove);
                            }
                        }
                    }
                }
            }

            Random random = new Random();
            if (bestMoves.Count > 0)
            {
                calculatedBestMove = bestMoves[random.Next(bestMoves.Count)];
            }
            else if (goodMoves.Count > 0)
            {
                calculatedBestMove = goodMoves[random.Next(goodMoves.Count)];
            }
            else
            {
                calculatedBestMove = availableMoves[random.Next(availableMoves.Count)];
            }

            return calculatedBestMove;
        }

        private static bool checkIfMoveEndsTheGame(Board i_Board, Point i_Coordinates, eSymbol i_SymbolToCheck)
        {
            bool isMoveEndsTheGame = false;
            
            if (i_Board.GetCellState(i_Coordinates) == eCellState.Empty)
            {
                i_Board.SetSymbol(i_SymbolToCheck, i_Coordinates);
                if (i_Board.HasWinner())
                {
                    isMoveEndsTheGame = true;
                }
                
                i_Board.SetSymbol(eSymbol.Blank, i_Coordinates);
            }

            return isMoveEndsTheGame;
        }
    }
}