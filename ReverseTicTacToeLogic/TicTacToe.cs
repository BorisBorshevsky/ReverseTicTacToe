using System.Drawing;
using ReverseTicTacToeLogic.Algorithms;

namespace ReverseTicTacToeLogic
{
    public class TicTacToe
    {
        private readonly ScoreBoard r_scoreBoard;

        public Board Board
        {
            get;
            private set;
        }

        public TicTacToe(int size)
        {
            r_scoreBoard = new ScoreBoard();
            Board = new Board(size);
            Board.InitializeBoard();
        }

        public bool TryPlayTurn(eSymbol i_PlayersSymbol)
        {
            eSymbol opponentSymbol = eSymbol.O;
            if (i_PlayersSymbol == eSymbol.O)
            {
                opponentSymbol = eSymbol.X;
            }

            Point computerMove = ArtificialIntelligenceAlgorithm.GetMove(Board, i_PlayersSymbol, opponentSymbol);
            
            return TryPlayTurn(computerMove, i_PlayersSymbol);
        }

        public bool TryPlayTurn(Point i_coordinates, eSymbol i_PlayersSymbol)
        {
            bool isPlayedSucceded = true;
            
            if (!Board.IsValidMove(i_coordinates))
            {
                isPlayedSucceded = false;
            }
            else
            {
                Board.SetSymbol(i_PlayersSymbol, i_coordinates);
            }

            if (Board.HasWinner())
            {
                if (i_PlayersSymbol == eSymbol.X)
                {
                    r_scoreBoard.AddWinToPlayer2();
                }
                else
                {
                    r_scoreBoard.AddWinToPlayer1();
                }
            }

            return isPlayedSucceded;
        }

        public ScoreBoard.Scores GetScores()
        {
            return r_scoreBoard.GetScores();
        }

        public void Surrender(eSymbol i_PlayersSymbol)
        {
            if (i_PlayersSymbol == eSymbol.X)
            {
                r_scoreBoard.AddWinToPlayer2();
            }
            else
            {
                r_scoreBoard.AddWinToPlayer1();
            }
        }

        public void Restart()
        {
            Board.InitializeBoard();
        }
    }
}