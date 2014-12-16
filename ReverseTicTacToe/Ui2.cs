using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using ReverseTicTacToeLogic;

namespace ReverseTicTacToe
{
    public class Ui2
    {
        private static bool isUserSurrendered = false;

        Player player1;
        Player player2;
        Player currentPlayer;
        TicTacToe ticTacToe;

        enum CellState
        {
            Empty,
            Used,
            Invalid
        }

        public void Start()
        {
            int boardSize = GetBoardSize();
            TicTacToe game = new TicTacToe(boardSize);
            
            ePlayerType opponentType = GetOpponentPlayertype();
            player1 = new Player(ePlayerType.User, eSymbol.X);
            player2 = new Player(opponentType, eSymbol.O);

            while (true)
            {
                StartSingleGame();

                
                DisplayScores();



                bool isAnotherGame = isPlayAnotherGame();
                if (!isAnotherGame)
                {
                    break;
                }
            }
            
            //bye bye string

            
        }

        public void StartSingleGame(){
            
            currentPlayer = player1;
            ticTacToe.Board.InitializeBoard();
            bool isUserSurrendered = false;


            while (true)
            {
                DisplayBoard();
                if (currentPlayer.PlayerType == ePlayerType.User)
                {
                    PlayUserTurn(currentPlayer, isUserSurrendered);
                    if (isUserSurrendered)
                    {
                        ticTacToe.Surrender(currentPlayer.Symbol);
                        break;
                    }
                }
                else
                {
                    playPcTurn();
                }
                
                eGameState gameState = GetGameState(ticTacToe.Board);
                switch (gameState)
                {
                    case eGameState.Active:
                        //all cool - contniure playing
                        
                        break;
                    case eGameState.BoardFull:
                        //print Tie and exit

                        break;
                    case eGameState.HasWinner:
                        //print winner and exit
                        break;
                }

                togglePlayerTurn();
            }
        }


        public enum eGameState
        {
            BoardFull,
            HasWinner,
            Active
        }

        public eGameState GetGameState(Board b)
        {
            return eGameState.Active;
        }


        public void PlayUserTurn(Player p, bool io_isUserSurrendered)
        {
            bool isTurnValid = false;
            io_isUserSurrendered = false;


            Point? coordinatesToPlay;
           
            do
            {
                coordinatesToPlay = GetInputFromUser();
                
                if (coordinatesToPlay == null)
                {
                    io_isUserSurrendered = true;
                    break;
                }
                else
                {
                     isTurnValid = ticTacToe.TryPlayTurn(coordinatesToPlay.Value, currentPlayer.Symbol);
                }
            } while (!isTurnValid);

        }
        
        
        public void PlayPcTurn(Player p)
        {
            ticTacToe.TryPlayTurn(currentPlayer.Symbol,getNextPlayer().Symbol);
        }

        void togglePlayerTurn()
        {
            currentPlayer = getNextPlayer();
        }

        private Player getNextPlayer()
        {
            return currentPlayer == player1 ? player2 : player1;
        }

        public  Point? GetInputFromUser()
        {
            return new Point();
        }

        public  void DisplayBoard() { }

        public  void DisplayScores() { }

        public  bool isPlayAnotherGame()
        {
            return true;
        }


        public  void playPcTurn()
        {
        }

        public int GetBoardSize()
        {
            return 5;
        }

        public ePlayerType GetOpponentPlayertype() {
            return ePlayerType.Computer;
        }
       
    }
}
