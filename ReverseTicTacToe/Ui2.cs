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
        public Ui2()
        {
            
            player1 = new Player();
            player2 = new Player();

            currentPlayer = player1;
        }

        Player player1;
        Player player2;
        Player currentPlayer;
        TicTacToe ticTacToe = new TicTacToe(5);
        

        public class Player
        {
            public ePlayerType PlayerType { get; set; }

            public bool IsPc { get; set; }

            public eSymbol Symbol { get; set; }

        }
        enum CellState
        {
            Empty,
            Used,
            Invalid
        }



        public  void Start()
        {
            
            //start game proceess
            while (true)
            {
                DisplayBoard();
                if (currentPlayer.PlayerType == ePlayerType.User)
                {
                    PlayUserTurn(currentPlayer);
                }
                else
                {
                    playPcTurn();
                }
                eGameState gameState = GetGameState(ticTacToe.Board);
                switch (gameState)
                {
                    case eGameState.Active:
                        break;
                    case eGameState.BoardFull:
                        break;
                    case eGameState.HasWinner:
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


        public void PlayUserTurn(Player p)
        {
            bool isTurnValid = false;
            Point? a;
           
            do
            {
                a = GetInputFromUser();
                
                if (a == null)
                {
                    ticTacToe.Surrender(currentPlayer.Symbol);
                    ticTacToe.Restart();
                    RestartUi();
                    break;
                }
                else
                {
                     isTurnValid = ticTacToe.TryPlayTurn(a.Value, currentPlayer.Symbol);
                }
            } while (!isTurnValid);

        }
        public void PlayPcTurn(Player p)
        {
            ticTacToe.TryPlayTurn(currentPlayer.Symbol,getNextPlayer().Symbol);
        }




        void RestartUi()
        {
            
        }

        void togglePlayerTurn()
        {
            currentPlayer = getNextPlayer();
        }

        private Player getNextPlayer()
        {
            return currentPlayer == player1 ? player2 : player1;
        }

        private  void StartGameProcess() { }

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

        public  void playUserTurn() { }

        public  void playPcTurn()
        {
        }

        public  int GetBoardSize()
        {
            return 0;
        }

        public  Player GetPlayer() { return null; }
       
    }
}
