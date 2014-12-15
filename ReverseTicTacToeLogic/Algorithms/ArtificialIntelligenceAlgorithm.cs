using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ReverseTicTacToeLogic.Algorithms
{
    static class ArtificialIntelligenceAlgorithm
    {
        public static Point GetMove(Board i_board, eSymbol i_currentUserSymbol, eSymbol i_opponnentSymbol)
        {
            Board dynamicProgrammingBoard = new Board(i_board.Size);
            List<Point> availableMoves = new List<Point>();
            List<Point> goodMoves = new List<Point>();
            List<Point> bestMoves = new List<Point>();
            Point calculatedBestMove;

            for (int row = 0; row < i_board.Size; row++)
            {
                for (int column = 0; column < i_board.Size; column++)
                {
                    Point currentMove = new Point(row, column);
                    if (i_board.IsValidMove(currentMove))
                    {
                        availableMoves.Add(currentMove);
                        if (!checkIfMoveEndsTheGame(i_board, currentMove, i_currentUserSymbol))
                        {
                            goodMoves.Add(currentMove);
                            if (!checkIfMoveEndsTheGame(i_board, currentMove, i_opponnentSymbol))
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

        private static bool checkIfMoveEndsTheGame(Board i_board, Point i_coordinates, eSymbol i_symbolToCheck)
        {
            bool isMoveEndsTheGame = false;
            
            if (i_board.IsValidMove(i_coordinates))
            {
                i_board.SetSymbol(i_symbolToCheck, i_coordinates);
                if (i_board.HasWinner())
                {
                    isMoveEndsTheGame = true;
                }
                
                i_board.SetSymbol(eSymbol.Blank, i_coordinates);
            }

            return isMoveEndsTheGame;
        }
    }
}
