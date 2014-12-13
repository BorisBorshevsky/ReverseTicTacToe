using ReverseTicTacToeLogic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ReverseTicTacToe
{
    static class TicTacToeConsoleUI
    {
        private static TicTacToe m_game;
        
        public static void Start()
        {
            int size = getBoardSize();
            m_game = new TicTacToe(size);
            playerType playerType = getOpponentType();
            

            
            while(true)
            {
                printBoard();
                Point? pointToDraw = getToDrawFromUser(size);
                if (pointToDraw == null)
                {
                    size = getBoardSize();
                    m_game = new TicTacToe(size);
                    playerType = getOpponentType();
                    continue;
                }

                m_game.PlayTurn(pointToDraw.Value);
                
            }

            Console.Read();

        }

        private static int getBoardSize()
        {
            Console.Clear();
            Console.WriteLine("Enter board size (3-9)");
            char input = Console.ReadKey().KeyChar;
            while (!Char.IsNumber(input) || Char.GetNumericValue(input) < 3)
            {
                Console.Clear();
                Console.WriteLine("Invalid input, Enter board size (3-9)");
                input = Console.ReadKey().KeyChar;
            }

            return (int)Char.GetNumericValue(input);
        }

        private static playerType getOpponentType()
        {
            Console.Clear();
            Console.WriteLine("Enter player type (1 = human, 2 = PC)");
            char input = Console.ReadKey().KeyChar;
            while (!Char.IsNumber(input) || Char.GetNumericValue(input) < 1 || Char.GetNumericValue(input) > 2)
            {
                Console.Clear();
                Console.WriteLine("Invalid input, Enter player type (1 = human, 2 = PC)");
                input = Console.ReadKey().KeyChar;
            }

            return (playerType)Char.GetNumericValue(input);
        }

        private static void printBoard()
        {
            Console.Clear();
            Symbol[,] board = m_game.Board.GetData();

            int rowLength = board.GetLength(0);
            int colLength = board.GetLength(1);

            Console.Write(" ");
            for (int col = 1; col <= rowLength; col++)
            {
                Console.Write(String.Format("   {0}", col));   
            }
            Console.WriteLine();
            for (int row = 0; row < rowLength; row++)
            {
                Console.Write(String.Format(" {0}|", row+1));
                for (int col = 0; col < colLength; col++)
                {
                    switch (board[row, col])
                    {
                        case Symbol.Blank:
                            Console.Write("   ");
                            break;
                        case Symbol.X:
                            Console.Write(" X ");
                            break;
                        case Symbol.O:
                            Console.Write(" O ");
                            break;
                        default:
                            break;
                    }
                    Console.Write("|");
                }
                
                Console.WriteLine();
                Console.WriteLine("  ========================");
            }
        }

        private static void printScores()
        {

        }

        private static Point? getToDrawFromUser(int boardSize)
        {
            Console.WriteLine(String.Format("Enter row number to draw (1-{0})", boardSize));
            ConsoleKeyInfo rowInput = Console.ReadKey();
            while (!Char.IsNumber(rowInput.KeyChar) ||
                ((int)Char.GetNumericValue(rowInput.KeyChar) < 1 || (int)Char.GetNumericValue(rowInput.KeyChar) > boardSize))
            {
                if (rowInput.Key == ConsoleKey.Q)
                {
                    break;
                }

                printBoard();
                Console.WriteLine(String.Format("Invalid input, Enter row number to draw (1-{0})", boardSize));
                rowInput = Console.ReadKey();
            }

            Point? pointToDraw = null;
            Console.WriteLine(String.Format("Enter column number to draw (1-{0})", boardSize));
            if (rowInput.Key != ConsoleKey.Q)
            {
                Console.WriteLine(String.Format("Enter column to draw (1-{0})", boardSize));
                ConsoleKeyInfo colInput = Console.ReadKey();
                while (!Char.IsNumber(colInput.KeyChar) ||
                    ((int)Char.GetNumericValue(colInput.KeyChar) < 1 || (int)Char.GetNumericValue(colInput.KeyChar) > boardSize))
                {
                    if (colInput.Key == ConsoleKey.Q)
                    {
                        break;
                    }

                    printBoard();
                    Console.WriteLine(String.Format("Invalid input, Enter column number to draw (1-{0})", boardSize));
                    colInput = Console.ReadKey();
                }

                if (rowInput.Key != ConsoleKey.Q)
                {
                    pointToDraw = new Point((int)Char.GetNumericValue(rowInput.KeyChar), (int)Char.GetNumericValue(colInput.KeyChar));
                }
            }

            return pointToDraw;
        }

        private static void printWinner()
        {

        }

        private static void isToStartAnotherGame()
        {
        }

    }
}
