using ReverseTicTacToeLogic;
using System;
using System.Drawing;
using System.Text;

namespace ReverseTicTacToe
{
    static class TicTacToeConsoleUi
    {
        private static TicTacToe s_gameLogic;
        
        public static void Start()
        {
            int boardSize = getBoardSize();
            s_gameLogic = new TicTacToe(boardSize);
            ePlayerType opponentPlayerType = getOpponentType();
            bool player1ShouldPlay = true;

            while(true)
            {
                printBoard();
                
                //player one or human opponent
                if (opponentPlayerType == ePlayerType.User || player1ShouldPlay)
                {
                    bool stopGame = false;
                    Point? pointToDraw;
                    
                /*    if (getCoordianteFromUser(boardSize, out pointToDraw) == false)
                    {
                        s_gameLogic.Surrender(player1ShouldPlay? eSymbol.X : eSymbol.O);
                        stopGame = true;
                    }*/

                    if (!stopGame && player1ShouldPlay) //player1
                    {
                        doUserTurn(boardSize, pointToDraw, eSymbol.X, out stopGame);
                        stopGame = isGameEnded(eSymbol.X);
                        PrintWinner(eSymbol.X);
                    }
                    else if (!stopGame && !player1ShouldPlay) //player2
                    {
                        doUserTurn(boardSize, pointToDraw, eSymbol.O, out stopGame);
                        stopGame = isGameEnded(eSymbol.O);
                        PrintWinner(eSymbol.O);
                    } 
                    
                    if (stopGame)
                    {
                        displayEndGame();
                    }
                }
                else //if computer plays (player2)
                {
                    s_gameLogic.TryPlayTurn(eSymbol.O, eSymbol.X);
                }
                
                player1ShouldPlay = !player1ShouldPlay;
            }

            
        }

        private static void displayEndGame()
        {
            
            printScores();

            Console.WriteLine("Do you want to keep playing? (1 = yes, 2 = no)");
            ConsoleKeyInfo userInput = Console.ReadKey();
            while (true) { 
                if (userInput.Key == ConsoleKey.D1)
                {
                    s_gameLogic.Restart();
                    break;
                }
                else if (userInput.Key == ConsoleKey.D2)
                {
                    Console.WriteLine("Bye Bye");
                    Console.Read();
                    break;
                }
                Console.WriteLine("invalid input");
                userInput = Console.ReadKey();
            }
            
        }

        private static bool isGameEnded(eSymbol i_PlayingSymbol)
        {
            bool isGameEnded = false;
            
            if (s_gameLogic.Board.HasWinner() || s_gameLogic.Board.IsBoardFull())
            {
                isGameEnded = true;
            }
            
            return isGameEnded;
        }

        private static void PrintWinner(eSymbol i_PlayingSymbol)
        {
            if (s_gameLogic.Board.HasWinner())
            {
                Console.WriteLine("The winner is {0} !!!!!", i_PlayingSymbol == eSymbol.X ? "Player1" : "Player2");
            }
            else if (s_gameLogic.Board.IsBoardFull())
            {
                Console.WriteLine("The board is full, No winner :(");
            }
            printScores();

        }



        private static void doUserTurn(int i_BoardSize, eSymbol i_Symbol, out bool io_StopGame)
        {

            Point? cordinatesToPlay;
            getCoordianteFromUser(i_BoardSize, out cordinatesToPlay,out io_StopGame);

            /*if (getCoordianteFromUser(boardSize, out pointToDraw, io_StopGame)
            {
                s_gameLogic.Surrender(player1ShouldPlay ? eSymbol.X : eSymbol.O);
                stopGame = true;
            }*/
            
            
         //   Point cordinatesToPlay = new Point(i_PointToDraw.Value.X - 1, i_PointToDraw.Value.Y - 1);
            
            bool isSuccess = s_gameLogic.TryPlayTurn(cordinatesToPlay, i_Symbol);
            
            while (!isSuccess)
            {
                Console.WriteLine("The location already chosen, choose another");
                if (getCoordianteFromUser(i_BoardSize, out i_PointToDraw) == false)
                {
                    io_StopGame = true;
                    return;
                }

                isSuccess = s_gameLogic.TryPlayTurn(new Point(i_PointToDraw.Value.X - 1, i_PointToDraw.Value.Y - 1), i_Symbol);
            }

            io_StopGame = false;
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

        private static ePlayerType getOpponentType()
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

            return (ePlayerType)Char.GetNumericValue(input);
        }

        private static void printBoard()
        {
            Console.Clear();
            eSymbol[,] board = s_gameLogic.Board.GetData();
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
                        case eSymbol.Blank:
                            boardToDraw.Append("   ");
                            break;
                        case eSymbol.X:
                            boardToDraw.Append(" X ");
                            break;
                        case eSymbol.O:
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
                s_gameLogic.GetScores().Player1, 
                Environment.NewLine,
                s_gameLogic.GetScores().Player2));
        }




        private static ConsoleKeyInfo getValidConsoleKeyInfo(int i_BoardSize)
        {
            ConsoleKeyInfo input = Console.ReadKey();
            Console.WriteLine();
            while (true)
            {
                if (input.Key == ConsoleKey.Q ||((int)Char.GetNumericValue(input.KeyChar) >= 1 || (int)Char.GetNumericValue(input.KeyChar) <= i_BoardSize))
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("invalid Input. Try again");
                }
                
                input = Console.ReadKey();
                Console.WriteLine();
            }
        }
        private static void getCoordianteFromUser(int i_BoardSize, out Point io_PointToDraw, out bool isGameEnded)
        {
            isGameEnded = true;
            io_PointToDraw = default(Point);

            Console.WriteLine(String.Format("Enter row number to draw (1-{0}) , Press 'Q' to Surrender.", i_BoardSize));
            ConsoleKeyInfo rowInfo = getValidConsoleKeyInfo(i_BoardSize);
            if (!(rowInfo.Key == ConsoleKey.Q))
            {
                Console.WriteLine(String.Format("Enter column number to draw (1-{0}) , Press 'Q' to Surrender.", i_BoardSize));
                ConsoleKeyInfo columnInfo = getValidConsoleKeyInfo(i_BoardSize);


                if (!(columnInfo.Key == ConsoleKey.Q))
                {
                    isGameEnded = false;
                    int row = ((int) Char.GetNumericValue(rowInfo.KeyChar));
                    int column = ((int)Char.GetNumericValue(columnInfo.KeyChar));
                    io_PointToDraw = new Point(row, column);
                }
            }

        }
        /*
        private static bool tryGetPointToDrawFromUser(int i_BoardSize, out Point io_PointToDraw)
        {
            Console.WriteLine(String.Format("Enter row number to draw (1-{0}) , Press 'Q' to Surrender.", i_BoardSize));
            ConsoleKeyInfo rowInput = Console.ReadKey();
            bool shouldQuit = false;

            while (!Char.IsNumber(rowInput.KeyChar) ||
                ((int)Char.GetNumericValue(rowInput.KeyChar) < 1 || (int)Char.GetNumericValue(rowInput.KeyChar) > i_BoardSize))
            {
                if (rowInput.Key == ConsoleKey.Q)
                {
                    shouldQuit = true;
                    break;
                }

                printBoard();
                Console.WriteLine(String.Format("Invalid input, Enter row number to draw (1-{0})", i_BoardSize));
                rowInput = Console.ReadKey();
            }

            Console.WriteLine(String.Format("Enter column number to draw (1-{0})", i_BoardSize));
            if (rowInput.Key != ConsoleKey.Q)
            {
                Console.WriteLine(String.Format("Enter column to draw (1-{0})", i_BoardSize));
                ConsoleKeyInfo colInput = Console.ReadKey();
                while (!Char.IsNumber(colInput.KeyChar) ||
                    ((int)Char.GetNumericValue(colInput.KeyChar) < 1 || (int)Char.GetNumericValue(colInput.KeyChar) > i_BoardSize))
                {
                    if (colInput.Key == ConsoleKey.Q)
                    {
                        break;
                    }

                    printBoard();
                    Console.WriteLine(String.Format("Invalid input, Enter column number to draw (1-{0})", i_BoardSize));
                    colInput = Console.ReadKey();
                }

                if (rowInput.Key != ConsoleKey.Q)
                {
                    io_PointToDraw = new Point((int)Char.GetNumericValue(rowInput.KeyChar), (int)Char.GetNumericValue(colInput.KeyChar));
                }
            }
        }*/

        private static void printWinner()
        {
            StringBuilder scoresBoardToDraw = new StringBuilder();
            scoresBoardToDraw.AppendLine();
            scoresBoardToDraw.Append("****Score Board****");
            scoresBoardToDraw.AppendLine();
            scoresBoardToDraw.AppendLine(String.Format("   Player1: {0}", s_gameLogic.GetScores().Player1));
            scoresBoardToDraw.AppendLine(String.Format("   Player2: {0}", s_gameLogic.GetScores().Player2));
            scoresBoardToDraw.AppendLine();

            Console.WriteLine(scoresBoardToDraw);
        }
    }
}