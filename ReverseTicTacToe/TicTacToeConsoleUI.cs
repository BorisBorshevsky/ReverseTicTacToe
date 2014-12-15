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
            int boardSize = getBoardSize();
            m_game = new TicTacToe(boardSize);
            playerType opponentPlayerType = getOpponentType();
            bool player1ShouldPlay = true;

            while(true)
            {

                printBoard();

                if (opponentPlayerType == playerType.User || player1ShouldPlay)
                {
                    Point? pointToDraw = getPointToDrawFromUser(boardSize);
                    if (pointToDraw == null)
                    {
                        m_game.Restart();
                        continue;
                    }
                    bool stopGame;
                    if (player1ShouldPlay)
                    {
                        doUserTurn(boardSize, pointToDraw, Symbol.X, out stopGame);
                        CheckBoard("Player2", out stopGame);
                    }
                    else
                    {
                        doUserTurn(boardSize, pointToDraw, Symbol.O, out stopGame);
                        CheckBoard("Player1", out stopGame);
                    }

                    if (stopGame)
                    {
                        Console.WriteLine("Press 'Q' to exit or any other key to continue");
                        ConsoleKeyInfo userInput = Console.ReadKey();
                        if (userInput.Key == ConsoleKey.Q)
                        {
                            break;
                        }
                        m_game.Restart();
                        continue;
                    }
                }
                else
                {
                    m_game.PlayTurn(Symbol.O);
                }

                player1ShouldPlay = !player1ShouldPlay;
                
            }
            Console.Clear();
            Console.WriteLine("Bye Bye");
            printScores();
            Console.Read();
        }

        private static void CheckBoard(string opponentPlayerName, out bool stopGame)
        {
            stopGame = false;
            if (m_game.Board.HasWinner())
            {
                Console.WriteLine(String.Format("The winner is {0} !!!!!", opponentPlayerName));
                stopGame = true;
                printScores();
            }
            else
            {
                if (m_game.Board.IsBoardFull())
                {
                    Console.WriteLine("The board is full, No winner :(");
                    stopGame = true;
                    printScores();
                }
            }
        }

        private static void doUserTurn(int boardSize, Point? pointToDraw, Symbol symbol, out bool stopGame)
        {
            bool wasSuccess = m_game.PlayTurn(new Point(pointToDraw.Value.X - 1, pointToDraw.Value.Y - 1), symbol);
            printBoard();
            while (!wasSuccess)
            {
                Console.WriteLine("The location already chosen, choose another");
                pointToDraw = getPointToDrawFromUser(boardSize);
                if (pointToDraw == null)
                    stopGame = true;

                wasSuccess = m_game.PlayTurn(new Point(pointToDraw.Value.X - 1, pointToDraw.Value.Y - 1), symbol);
            }

            stopGame = false;
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

        private static Point? getPointToDrawFromUser(int boardSize)
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
