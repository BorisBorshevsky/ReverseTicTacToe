using System;
using System.Drawing;
using System.Text;
using ReverseTicTacToeLogic;
using Ex02.ConsoleUtils;

namespace ReverseTicTacToe
{
    public class TicTacToeConsoleUI
    {
        private const int v_MinBoardSize = 3;
        private const int v_MaxBoardSize = 9;

        private Player m_Player1;
        private Player m_Player2;
        private Player m_CurrentPlayer;
        private TicTacToe m_TicTacToe;

        public void Start()
        {
            int boardSize = getBoardSize();
            m_TicTacToe = new TicTacToe(boardSize);

            ePlayerType opponentType = getOpponentPlayerType();
            m_Player1 = new Player(ePlayerType.User, eSymbol.X);
            m_Player2 = new Player(opponentType, eSymbol.O);

            while (true)
            {
                StartSingleGame(boardSize);       
                displayScores();

                bool isAnotherGame = isPlayAnotherGame();
                if (!isAnotherGame)
                {
                    break;
                }
            }

            Console.WriteLine();
            Console.WriteLine("Bye Bye, press any key to exit");
            Console.ReadKey();
        }

        public void StartSingleGame(int i_BoardSize){
            
            m_CurrentPlayer = m_Player1;
            m_TicTacToe.Board.InitializeBoard();
            bool isUserSurrendered = false;
            displayBoard();

            while (true)
            {

                if (m_CurrentPlayer.PlayerType == ePlayerType.User)
                {
                    PlayUserTurn(1, i_BoardSize, out isUserSurrendered);
                    if (isUserSurrendered)
                    {
                        m_TicTacToe.Surrender(m_CurrentPlayer.Symbol);
                        break;
                    }
                }
                else
                {
                    playPcTurn(m_CurrentPlayer);
                }

                displayBoard();
                eGameState gameState = GetGameState(m_TicTacToe.Board);

                if (gameState == eGameState.BoardFull)
                {
                    Console.WriteLine("The board is full, it is tie :(");
                    break;
                }

                if (gameState == eGameState.HasWinner)
                {
                    string opponentPlayerSymbol = getNextPlayer().Symbol.ToString();
                    Console.WriteLine("The winner is {0} !!!!!", opponentPlayerSymbol);
                    break;
                }

                togglePlayerTurn();
            }
        }

        public eGameState GetGameState(Board i_Board)
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

        public void PlayUserTurn(int i_MinimumCellIndex, int i_MaximumCellIndex, out bool io_IsUserSurrendered)
        {
            bool isTurnValid = false;
            io_IsUserSurrendered = false;
            Point? coordinatesToPlay;
           
            do
            {
                coordinatesToPlay = getCoordianteFromUser(i_MinimumCellIndex, i_MaximumCellIndex);
                if (coordinatesToPlay == null)
                {
                    io_IsUserSurrendered = true;
                    break;
                }

                Point boardPoint = convertUserPointToBoardPoint(coordinatesToPlay.Value);
                isTurnValid = m_TicTacToe.TryPlayTurn(boardPoint, m_CurrentPlayer.Symbol);

                if (!isTurnValid)
                {
                    Console.WriteLine("invalid Input. Try again");
                }

            } while (!isTurnValid);

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
            if (rowNumber.Key != ConsoleKey.Q)
            {
                Console.WriteLine("Enter column number to draw ({0}-{1}) , Press 'Q' to Surrender.", i_MinimumValue, i_MaximumValue);
                ConsoleKeyInfo columnNumber = getValidConsoleKeyInfo(i_MinimumValue, i_MaximumValue);

                if (columnNumber.Key != ConsoleKey.Q)
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
                if (input.Key == ConsoleKey.Q || ((int)Char.GetNumericValue(input.KeyChar) >= i_MinimumValue && (int)Char.GetNumericValue(input.KeyChar) <= i_MaximumValue))
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
            Console.WriteLine("{0}: {1}, {2}{3}: {4}",
                m_Player1.Symbol.ToString(),
                m_TicTacToe.GetScores().Player1,
                Environment.NewLine,
                m_Player2.Symbol.ToString(),
                m_TicTacToe.GetScores().Player2);
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
                else if (userInput.Key == ConsoleKey.D2 || userInput.Key == ConsoleKey.NumPad2)
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
            m_TicTacToe.TryPlayTurn(i_Player.Symbol);
        }

        private int getBoardSize()
        {
            Screen.Clear();
            Console.WriteLine("Enter board size ({0}-{1})", v_MinBoardSize, v_MaxBoardSize);
            char input = Console.ReadKey().KeyChar;
            while (!Char.IsNumber(input) || Char.GetNumericValue(input) < 3)
            {
                Screen.Clear();
                Console.WriteLine("Invalid input, Enter board size ({0}-{1})", v_MinBoardSize, v_MaxBoardSize);
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
