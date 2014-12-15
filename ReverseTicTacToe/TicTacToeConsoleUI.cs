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
                    m_game.Restart();
                    continue;
                }
                
                //player 1
                bool continuePlay = PlayPlayer(size, playerType.User, Symbol.X, "Player 1", pointToDraw);
                if (continuePlay == false)
                {
                    Console.WriteLine("Do you want another game?");
                    m_game.Restart();
                    continue;
                }

                //player 2
                continuePlay = PlayPlayer(size, playerType, Symbol.O, "Player 2", pointToDraw);
                if (continuePlay == false)
                {
                    m_game.Restart();
                    continue;
                }
            }

            Console.Read();
        }

        private static bool PlayPlayer(int boardSize, playerType playerType, Symbol symbol, string playerName, Point? pointToDraw)
        {
            bool continuePlay = false;
            if (playerType == ReverseTicTacToe.playerType.User)
            {
                continuePlay = playTurn(boardSize, pointToDraw, symbol);
            }
            else
            {
                m_game.PlayTurn(symbol);
            }

            if (m_game.HasWinner())
            {
                Console.WriteLine(String.Format("The winner is {0} !!!!!", playerName));
                printScores();
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                continuePlay = false;
            }
            else
            {
                if (m_game.Board.IsBoardFull())
                {
                    Console.WriteLine("The board is full, No winner :(");
                    printScores();
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    continuePlay = false;
                }
            }

            return continuePlay;
        }

        private static bool playTurn(int size, Point? pointToDraw, Symbol symbol)
        {
            bool wasSuccess = m_game.PlayTurn(new Point(pointToDraw.Value.X - 1, pointToDraw.Value.Y - 1), symbol);
            printBoard();
            while (!wasSuccess)
            {
                Console.WriteLine("The location already chosen, choose another");
                pointToDraw = getToDrawFromUser(size);
                if (pointToDraw == null)
                    return false;

                wasSuccess = m_game.PlayTurn(new Point(pointToDraw.Value.X - 1, pointToDraw.Value.Y - 1), symbol);
            }

            return true;
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
            StringBuilder boardToDraw = new StringBuilder();

            boardToDraw.Append(" ");
            for (int col = 1; col <= rowLength; col++)
            {
                boardToDraw.Append(String.Format("   {0}", col));   
            }

            boardToDraw.AppendLine();
            for (int row = 0; row < rowLength; row++)
            {
                boardToDraw.Append(String.Format(" {0}|", row+1));
                for (int col = 0; col < colLength; col++)
                {
                    switch (board[row, col])
                    {
                        case Symbol.Blank:
                            boardToDraw.Append("   ");
                            break;
                        case Symbol.X:
                            boardToDraw.Append(" X ");
                            break;
                        case Symbol.O:
                            boardToDraw.Append(" O ");
                            break;
                        default:
                            break;
                    }
                    
                    boardToDraw.Append("|");
                }

                boardToDraw.AppendLine();
                boardToDraw.Append("  ");
                for (int index = 0; index < colLength; index++)
                {
                    boardToDraw.Append("====");
                }

                boardToDraw.AppendLine();
            }

            Console.WriteLine(boardToDraw);
        }

        private static void printScores()
        {
            Console.WriteLine(String.Format("Player1: {0}, {1}Player2: {2}", 
                m_game.GetScores().Player1, 
                Environment.NewLine,
                m_game.GetScores().Player2));
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
            StringBuilder scoresBoardToDraw = new StringBuilder();
            scoresBoardToDraw.AppendLine();
            scoresBoardToDraw.Append("****Score Board****");
            scoresBoardToDraw.AppendLine();
            scoresBoardToDraw.AppendLine(String.Format("   Player1: {0}", m_game.GetScores().Player1));
            scoresBoardToDraw.AppendLine(String.Format("   Player2: {0}", m_game.GetScores().Player2));
            scoresBoardToDraw.AppendLine();

            Console.WriteLine(scoresBoardToDraw);
        }
    }
}
