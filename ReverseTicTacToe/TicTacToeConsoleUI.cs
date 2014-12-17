using System;
using System.Drawing;
using System.Text;
using ReverseTicTacToeLogic;
using Ex02.ConsoleUtils;

namespace ReverseTicTacToe
{
    public class TicTacToeConsoleUI
    {
        private const int k_MinBoardSize = 3;
        private const int k_MaxBoardSize = 9;
        private const ConsoleKey k_QuitKey = ConsoleKey.Q;
        private const string k_PlayerOneName = "Player1";
        private const string k_PlayerTwoName = "Player2";


        private Player m_Player1;
        private Player m_Player2;
        private Player m_CurrentPlayer;
        private TicTacToe m_TicTacToe;

        public void Start()
        {
            int boardSize = getBoardSize();
            ePlayerType opponentType = getOpponentPlayerType();

            m_Player1 = new Player(ePlayerType.User, eSymbol.X, k_PlayerOneName);
            m_Player2 = new Player(opponentType, eSymbol.O, k_PlayerTwoName);
            m_TicTacToe = new TicTacToe(boardSize, m_Player1, m_Player2);
            while (true)
            {
                startSingleGame(boardSize);       
                displayScores();
                
                if (!isPlayAnotherGame())
                {
                    break;
                }
            }

            Console.WriteLine();
            Console.WriteLine("Bye Bye, press any key to exit");
            Console.ReadKey();
        }

        private void startSingleGame(int i_BoardSize)
        {
            m_CurrentPlayer = m_Player1;
            m_TicTacToe.Board.InitializeBoard();
            bool isUserSurrendered = false;
            displayBoard();

            while (true)
            {

                if (m_CurrentPlayer.PlayerType == ePlayerType.User)
                {
                    playUserTurn(1, i_BoardSize, out isUserSurrendered);
                    if (isUserSurrendered)
                    {
                        m_TicTacToe.Surrender(m_CurrentPlayer);
                        break;
                    }
                }
                else
                {
                    playPcTurn(m_CurrentPlayer);
                }

                displayBoard();
                eGameState gameState = getGameState(m_TicTacToe.Board);

                if (gameState == eGameState.BoardFull)
                {
                    Console.WriteLine("The board is full, it is tie :(");
                    break;
                }

                if (gameState == eGameState.HasWinner)
                {
                    string opponentPlayerName = getNextPlayer().PlayerName;
                    Console.WriteLine("The winner is {0} !!!!!", opponentPlayerName);
                    break;
                }

                togglePlayerTurn();
            }
        }

        private eGameState getGameState(Board i_Board)
        {
            eGameState gameState = eGameState.Active;
            if (i_Board.HasWinner())
            {
                gameState = eGameState.HasWinner;
            }
            else if (i_Board.IsBoardFull())
            {
                gameState = eGameState.BoardFull;
            }

            return gameState;
        }

        private void playUserTurn(int i_MinimumCellIndex, int i_MaximumCellIndex, out bool io_IsUserSurrendered)
        {
            eCellState cellState = eCellState.Empty;
            io_IsUserSurrendered = false;
            Point? coordinatesToPlay;
           
            do
            {
                coordinatesToPlay = getCoordianteFromUser(i_MinimumCellIndex, i_MaximumCellIndex);
                if (coordinatesToPlay == null) //null means the user surrendered
                {
                    io_IsUserSurrendered = true;
                    break;
                }

                Point boardPoint = convertUserPointToBoardPoint(coordinatesToPlay.Value);
                cellState = m_TicTacToe.TryPlayTurn(boardPoint, m_CurrentPlayer);
                if (cellState == eCellState.Used)
                {
                    Console.WriteLine("Cell is already Taken, please try again.");
                }
                else if (cellState == eCellState.OutOfRange)
                {
                    Console.WriteLine("Cell is out of range, please try again.");
                }

            } while (cellState != eCellState.Empty);
        }
        
        private Point convertUserPointToBoardPoint(Point i_Point)
        {
            i_Point.X--;
            i_Point.Y--;

            return i_Point;
        }

        private void togglePlayerTurn()
        {
            m_CurrentPlayer = getNextPlayer();
        }

        private Player getNextPlayer()
        {
            return m_CurrentPlayer == m_Player1 ? m_Player2 : m_Player1;
        }

        private Point? getCoordianteFromUser(int i_MinimumValue, int i_MaximumValue)
        {
            Point? userCoordiante = null;

            Console.WriteLine("Enter row number to draw ({0}-{1}) , Press 'Q' to Surrender.", i_MinimumValue, i_MaximumValue);
            ConsoleKeyInfo rowNumber = getValidConsoleKeyInfo(i_MinimumValue, i_MaximumValue);
            if (rowNumber.Key != k_QuitKey)
            {
                Console.WriteLine("Enter column number to draw ({0}-{1}) , Press 'Q' to Surrender.", i_MinimumValue, i_MaximumValue);
                ConsoleKeyInfo columnNumber = getValidConsoleKeyInfo(i_MinimumValue, i_MaximumValue);

                if (columnNumber.Key != k_QuitKey)
                {
                    int row = ((int)Char.GetNumericValue(rowNumber.KeyChar));
                    int column = ((int)Char.GetNumericValue(columnNumber.KeyChar));
                    userCoordiante = new Point(row, column);
                }
            }

            return userCoordiante;
        }

        private ConsoleKeyInfo getValidConsoleKeyInfo(int i_MinimumValue, int i_MaximumValue)
        {
            ConsoleKeyInfo input = Console.ReadKey();
            Console.WriteLine();
            while (true)
            {
                if (input.Key == k_QuitKey || ((int)Char.GetNumericValue(input.KeyChar) >= i_MinimumValue && (int)Char.GetNumericValue(input.KeyChar) <= i_MaximumValue))
                {
                    return input;
                }

                Console.WriteLine("invalid Input. Try again");
                input = Console.ReadKey();
                Console.WriteLine();
            }
        }

        private void displayBoard() 
        {
            Screen.Clear();
            eSymbol[,] board = m_TicTacToe.Board.GetData();
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
                boardToDraw.Append(String.Format(" {0}|", row + 1));
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

        private void displayScores() 
        {
            Console.WriteLine("-- Scores --");
            Console.WriteLine("{0}: {1} Points, {2}{3}: {4} Points",
                m_Player1.PlayerName,
                m_TicTacToe.GetScores().Player1.Score,
                Environment.NewLine,
                m_Player2.PlayerName,
                m_TicTacToe.GetScores().Player2.Score);
        }

        private bool isPlayAnotherGame()
        {
            bool isPlayAnotherGame = false;

            Console.WriteLine("Do you want to keep playing? (1 = yes, 2 = no)");
            ConsoleKeyInfo userInput = Console.ReadKey();
            while (true)
            {
                if (userInput.Key == ConsoleKey.D1 || userInput.Key == ConsoleKey.NumPad1)
                {
                    isPlayAnotherGame = true;
                    break;
                }

                if (userInput.Key == ConsoleKey.D2 || userInput.Key == ConsoleKey.NumPad2)
                {
                    isPlayAnotherGame = false;
                    break;
                }

                Console.WriteLine("invalid input");
                userInput = Console.ReadKey();
            }

            return isPlayAnotherGame;
        }

        private void playPcTurn(Player i_Player)
        {
            m_TicTacToe.TryPlayTurn(i_Player);
        }

        private int getBoardSize()
        {
            Screen.Clear();
            Console.WriteLine("Enter board size ({0}-{1})", k_MinBoardSize, k_MaxBoardSize);
            char input = Console.ReadKey().KeyChar;
            while (!Char.IsNumber(input) || Char.GetNumericValue(input) < 3)
            {
                Screen.Clear();
                Console.WriteLine("Invalid input, Enter board size ({0}-{1})", k_MinBoardSize, k_MaxBoardSize);
                input = Console.ReadKey().KeyChar;
            }

            return (int)Char.GetNumericValue(input);
        }

        private ePlayerType getOpponentPlayerType() {
            Screen.Clear();
            Console.WriteLine("Enter player type (1 = human, 2 = PC)");
            char input = Console.ReadKey().KeyChar;
            while (!Char.IsNumber(input) || Char.GetNumericValue(input) < 1 || Char.GetNumericValue(input) > 2)
            {
                Screen.Clear();
                Console.WriteLine("Invalid input, Enter player type (1 = human, 2 = PC)");
                input = Console.ReadKey().KeyChar;
            }

            return (ePlayerType)Char.GetNumericValue(input);
        }
    }
}